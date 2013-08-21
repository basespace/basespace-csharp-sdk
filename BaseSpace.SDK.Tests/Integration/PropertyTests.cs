using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Illumina.BaseSpace.SDK.ServiceModels;
using Illumina.BaseSpace.SDK.Tests.Helpers;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK.Tests.Integration
{
    public class PropertyTests : BaseIntegrationTest
    {
        private Project _project = null;

        private static readonly string[] RAINBOW = { "red", "orange", "yellow", "green", "blue", "indigo", "violet" };

        public PropertyTests()
        {
            var projectName = string.Format("C# SDK Unit Test Project " + DateTime.UtcNow.ToFileTime());
            var response = Client.CreateProject(new CreateProjectRequest(projectName));
            _project = response.Response;
        }

        [Fact]
        public void UsageExample()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("mytestapp.metrics.magicnumber").SetSingleValueContent("42");
            setPropRequest.AddPropertyToSet("mytestapp.inputs.appresults").SetMultiValueReferences(new[] { "appresults/3006", "appresults/3005" });

            // Create some properties
            // POST: resource/{id}/properties 
            var properties = Client.SetPropertiesForResource(setPropRequest).Response.Items;

            // list those properties
            // GET: resource/{id}/properties
            properties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project)).Response.Items;

            // they also show up here
            // GET: resource/{id}
            properties = Client.GetProject(new GetProjectRequest(_project.Id)).Response.Properties.Items;

            // take a deeper dive into the items list
            // GET: resource/{id}/properties/{name}/items           
            var propertyItems = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "mytestapp.inputs.appresults")).Response.Items;

            // delete a property
            // DELETE: resource/{id}/properties/{name}
            Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "mytestapp.inputs.appresults"));
        }

        [Fact]
        public void CreateSingleItemProperty()
        {
            var name = "unittest.singlevalue.contentfoo";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet(name).SetSingleValueContent("Foo");

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);
            Assert.NotNull(prop);
            Assert.Equal("Foo", prop.Content.ToString());
        }

        [Fact]
        public void CreateSingleItemReferenceProperty()
        {
            var name = "unittest.singlevalue.referencefoo";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet(name).SetSingleValueReference(_project);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);
            Assert.NotNull(prop);
            var projectContent = prop.Content.ToResource<ProjectCompact>();
            Assert.NotNull(projectContent);
            Assert.Equal(_project.Id, projectContent.Id);
        }

        [Fact]
        public void CreateMultiItemProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("unittest.multiitem.rainbow").SetMultiValueContent(RAINBOW);
            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var prop = propResponse.Items.FirstOrDefault(p => p.Name == "unittest.multiitem.rainbow");
            Assert.NotNull(prop);
            Assert.NotNull(prop.Items);
            Assert.Equal(RAINBOW.Count(), prop.Items.Count());
            Assert.Equal(RAINBOW.Count(), prop.ItemsDisplayedCount.Value);
            Assert.Equal(RAINBOW.Count(), prop.ItemsTotalCount.Value);

            string[] contents = prop.ToStringArray();
            Assert.Equal(RAINBOW.Count(), contents.Count());
            Assert.True(RAINBOW.All(i=>contents.Contains(i)));
        }

        [Fact]
        public void CreateMultiItemReferenceProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("unittest.multiitem.projects").SetMultiValueReferences(new []{_project});
            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var itemsResponse  = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.projects")).Response;
 
            var returnedProject = itemsResponse.Items.First().Content.ToProject();
            Assert.NotNull(returnedProject);
            Assert.Equal(_project.Id, returnedProject.Id);
            Assert.Equal(_project.Name, returnedProject.Name);

            returnedProject = itemsResponse.ToProjectArray().First();
            Assert.NotNull(returnedProject);
            Assert.Equal(_project.Id, returnedProject.Id);
            Assert.Equal(_project.Name, returnedProject.Name);
        }

        [Fact]
        public void DeleteProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("unittest.deletetest").SetSingleValueContent("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;

            var response = Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest"));
        }

        [Fact]
        public void GetPropertyVerbose()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("unittest.verbosepropertytest").SetMultiValueContent(RAINBOW);
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;
            
            // TODO (maxm): finish this by including app/user/dates in PropertyFull
        }

        [Fact]
        public void MultiItemPaging()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var values = new string[100];

            for (int i = 0; i < 100; i++)
                values[i] = i.ToString();

            setPropRequest.AddPropertyToSet("unittest.multiitem.manyitems").SetMultiValueContent(values);
            var property = Client.SetPropertiesForResource(setPropRequest).Response.Items.FirstOrDefault();
            Assert.NotNull(property);
            Assert.True(property.ItemsDisplayedCount < property.ItemsTotalCount);
            Assert.True(property.ItemsDisplayedCount < 100);
            Assert.Equal(100, property.ItemsTotalCount);

            var propertyItems = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.manyitems")).Response;
            Assert.NotNull(propertyItems);
            Assert.Equal(100, propertyItems.TotalCount);
            Assert.True(propertyItems.DisplayedCount < property.ItemsTotalCount);

            propertyItems = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.manyitems"){ Limit = 3, Offset = 75}).Response;
            Assert.Equal(3, propertyItems.Limit);
            Assert.Equal(75, propertyItems.Offset);
            Assert.Equal(3, propertyItems.DisplayedCount);
            Assert.True(propertyItems.Items.All(x => x != null));
            Assert.True(propertyItems.Items.All(x => new[] {"75", "76", "77"}.Contains(x.ToString())));
        }

        [Fact]
        public void InvalidNameError()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("a").SetMultiValueContent(RAINBOW);
            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateDuplicateProperty()
        {
            string name = "unittest.duplicateproperty";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet(name).SetSingleValueContent("Foo");
            setPropRequest.AddPropertyToSet(name).SetSingleValueContent("Foo2");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.DUPLICATE_NAMES", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateEmptyPropertyName()
        {
            string name = string.Empty;
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet(name).SetSingleValueContent("Foo");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteNonExistingProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet("unittest.deletetest").SetSingleValueContent("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;
            AssertErrorResponse(() => Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest.notexisting")), "BASESPACE.PROPERTIES.NOT_FOUND", HttpStatusCode.NotFound);
        }

        [Fact]
        public void CreatePropertyWithNameGreaterThan64()
        {
            var name = "unittest.singlevalue.propertynamegreaterthan64.01234567891234567890";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet(name).SetSingleValueContent("Foo");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateMultipleProperties()
        {
            var name = "unittest.multipleproperties.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 65; intCtr++)
                setPropRequest.AddPropertyToSet(name + intCtr.ToString()).SetSingleValueContent("Foo" + intCtr.ToString());

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            Assert.NotNull(propResponse);
            Assert.Equal(propResponse.DisplayedCount, propResponse.TotalCount); 
            Assert.Equal(65, propResponse.TotalCount);
        }

        [Fact]
        public void GetAllPropertiesForResource()
        {
            var name = "unittest.getmultipleproperties.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 65; intCtr++)
                setPropRequest.AddPropertyToSet(name + intCtr.ToString()).SetSingleValueContent("Foo" + intCtr.ToString());

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            Assert.NotNull(propResponse);
            Assert.Equal(propResponse.DisplayedCount, propResponse.TotalCount);
            Assert.Equal(65, propResponse.TotalCount);

            var prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project)).Response;

            Assert.NotNull(prjProperties);
            Assert.True(prjProperties.DisplayedCount < prjProperties.TotalCount, string.Format("Displayed count: {0} should be less than Total Count: {1}", propResponse.DisplayedCount, propResponse.TotalCount));
            Assert.Equal(50, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Limit = 65 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(65, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(65, prjProperties.Items.Count());
        }

        [Fact]
        public void GetAPropertyForResource()
        {
            var name = "unittest.getpropertyforaresource.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 5; intCtr++)
                setPropRequest.AddPropertyToSet(name + intCtr.ToString()).SetSingleValueContent("Foo" + intCtr.ToString());

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            Assert.NotNull(propResponse);
            Assert.Equal(propResponse.DisplayedCount, propResponse.TotalCount);
            Assert.Equal(5, propResponse.TotalCount);

            var prjRqst = new GetPropertyRequest(_project, "unittest.getpropertyforaresource.property2");
            var prjProperties = Client.GetPropertyForResource(prjRqst);

            Assert.NotNull(prjProperties);
            Assert.Equal("unittest.getpropertyforaresource.property2", prjProperties.Response.Name);
            Assert.Equal("Foo2", prjProperties.Response.Content.ToString());
            Assert.NotNull(prjProperties.Response.ApplicationModifiedBy);
            Assert.NotNull(prjProperties.Response.UserModifiedBy);            
        }

        [Fact]
        public void GetNonExistingPropertyForResource()
        {
            var name = "unittest.getinvalidproperty";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddPropertyToSet(name).SetSingleValueContent("Foo");

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.NotNull(propResponse);

            var prjRqst = new GetPropertyRequest(_project, "unittest.getinvalidproperty.notexisting");

            AssertErrorResponse(() => Client.GetPropertyForResource(prjRqst), "BASESPACE.PROPERTIES.NOT_FOUND", HttpStatusCode.NotFound);
        }
    }
}
