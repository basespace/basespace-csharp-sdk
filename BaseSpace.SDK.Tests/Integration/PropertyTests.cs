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
            setPropRequest.SetProperty("mytestapp.metrics.magicnumber").SetSingleValueContent("42");
            setPropRequest.SetProperty("mytestapp.inputs.appresults").SetMultiValueReferences(new[] { "appresults/3006", "appresults/3005" });

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
            setPropRequest.SetProperty(name).SetSingleValueContent("Foo");

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
            setPropRequest.SetProperty(name).SetSingleValueReference(_project);

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
            setPropRequest.SetProperty("unittest.multiitem.rainbow").SetMultiValueContent(RAINBOW);
            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var prop = propResponse.Items.FirstOrDefault(p => p.Name == "unittest.multiitem.rainbow");
            Assert.NotNull(prop);
            Assert.NotNull(prop.Items);
            Assert.Equal(RAINBOW.Count(), prop.Items.Count());
            Assert.Equal(RAINBOW.Count(), prop.ItemsDisplayedCount.Value);
            Assert.Equal(RAINBOW.Count(), prop.ItemsTotalCount.Value);

            string[] contents = prop.ToStringArray();
            Assert.Equal(RAINBOW.Count(), contents.Count());
            Assert.True(RAINBOW.All(i => contents.Contains(i)));
        }

        [Fact]
        public void CreateMultiItemReferenceProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.multiitem.projects").SetMultiValueReferences(new[] { _project });
            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var itemsResponse = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.projects")).Response;

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
            setPropRequest.SetProperty("unittest.deletetest").SetSingleValueContent("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;

            var response = Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest"));
        }

        [Fact]
        public void GetPropertyVerbose()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.verbosepropertytest").SetMultiValueContent(RAINBOW);
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

            setPropRequest.SetProperty("unittest.multiitem.manyitems").SetMultiValueContent(values);
            var property = Client.SetPropertiesForResource(setPropRequest).Response.Items.FirstOrDefault();
            Assert.NotNull(property);
            Assert.True(property.ItemsDisplayedCount < property.ItemsTotalCount);
            Assert.True(property.ItemsDisplayedCount < 100);
            Assert.Equal(100, property.ItemsTotalCount);

            var propertyItems = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.manyitems")).Response;
            Assert.NotNull(propertyItems);
            Assert.Equal(100, propertyItems.TotalCount);
            Assert.True(propertyItems.DisplayedCount < property.ItemsTotalCount);

            propertyItems = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.manyitems") { Limit = 3, Offset = 75 }).Response;
            Assert.Equal(3, propertyItems.Limit);
            Assert.Equal(75, propertyItems.Offset);
            Assert.Equal(3, propertyItems.DisplayedCount);
            Assert.True(propertyItems.Items.All(x => x != null));
            Assert.True(propertyItems.Items.All(x => new[] { "75", "76", "77" }.Contains(x.ToString())));
        }

        [Fact]
        public void InvalidNameError()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("a").SetMultiValueContent(RAINBOW);
            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateDuplicateProperty()
        {
            string name = "unittest.duplicateproperty";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetSingleValueContent("Foo");
            setPropRequest.SetProperty(name).SetSingleValueContent("Foo2");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.DUPLICATE_NAMES", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateEmptyPropertyName()
        {
            string name = string.Empty;
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetSingleValueContent("Foo");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteNonExistingProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.deletetest").SetSingleValueContent("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;
            AssertErrorResponse(() => Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest.notexisting")), "BASESPACE.PROPERTIES.NOT_FOUND", HttpStatusCode.NotFound);
        }

        [Fact]
        public void CreatePropertyWithNameGreaterThan64()
        {
            var name = "unittest.singlevalue.propertynamegreaterthan64.01234567891234567890";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetSingleValueContent("Foo");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateMultipleProperties()
        {
            var name = "unittest.multipleproperties.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 65; intCtr++)
                setPropRequest.SetProperty(name + intCtr.ToString()).SetSingleValueContent("Foo" + intCtr.ToString());

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
                setPropRequest.SetProperty(name + intCtr.ToString()).SetSingleValueContent("Foo" + intCtr.ToString());

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

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Offset = 30 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(35, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(35, prjProperties.Items.Count());

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Limit = 10, Offset = 30 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(10, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(10, prjProperties.Items.Count());

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Limit = 10, Offset = 60 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(5, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(5, prjProperties.Items.Count());

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Offset = 65 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(0, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(0, prjProperties.Items.Count());

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Offset = 65, Limit = 1 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(0, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(0, prjProperties.Items.Count());

            prjProperties = Client.ListPropertiesForResource(new ListPropertiesRequest(_project) { Offset = -1, Limit = -1 }).Response;
            Assert.NotNull(prjProperties);
            Assert.Equal(0, prjProperties.DisplayedCount);
            Assert.Equal(65, prjProperties.TotalCount);
            Assert.Equal(0, prjProperties.Items.Count());
        }

        [Fact]
        public void GetAPropertyForResource()
        {
            var name = "unittest.getpropertyforaresource.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 5; intCtr++)
                setPropRequest.SetProperty(name + intCtr.ToString()).SetSingleValueContent("Foo" + intCtr.ToString());

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
            setPropRequest.SetProperty(name).SetSingleValueContent("Foo");

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.NotNull(propResponse);

            var prjRqst = new GetPropertyRequest(_project, "unittest.getinvalidproperty.notexisting");

            AssertErrorResponse(() => Client.GetPropertyForResource(prjRqst), "BASESPACE.PROPERTIES.NOT_FOUND", HttpStatusCode.NotFound);
        }

        [Fact]
        public void CreateDuplicateItemsForProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var values = new string[5];

            for (int i = 0; i < 5; i++)
                values[i] = "duplicate";

            setPropRequest.SetProperty("unittest.multiitem.duplicateitems").SetMultiValueContent(values);
            var property = Client.SetPropertiesForResource(setPropRequest).Response.Items.FirstOrDefault();
            Assert.NotNull(property);
            Assert.Equal(property.ItemsDisplayedCount, property.ItemsTotalCount);
            Assert.Equal(5, property.ItemsTotalCount);

            var propertyItems = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.duplicateitems")).Response;
            Assert.NotNull(propertyItems);
            Assert.Equal(5, propertyItems.TotalCount);
            Assert.Equal(propertyItems.DisplayedCount, property.ItemsTotalCount);
        }

        [Fact]
        public void CreateEmptyItemProperty()
        {
            var name = "unittest.singlevalue.emptyvalue";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetSingleValueContent("");

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);
            Assert.NotNull(prop);
            Assert.Equal(string.Empty, prop.Content.ToString());
        }

        [Fact]
        public void CreateEmptyMultiItemProperty()
        {
            var name = "unittest.multivalue.emptyvalue";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetMultiValueContent(new string[1]);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);
            Assert.NotNull(prop);
            Assert.Equal(0, prop.Items.Count());
        }

        [Fact]
        public void UpdatePropertyForResource()
        {
            var name = "unittest.singlevalue.updatepropertyvalue";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name + "1").SetSingleValueContent("Foo1");
            setPropRequest.SetProperty(name + "2").SetSingleValueContent("Foo2");
            setPropRequest.SetProperty(name + "3").SetSingleValueContent("Foo");

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.NotNull(propResponse);
            Assert.Equal(3, propResponse.Items.Count());

            setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name + "3").SetSingleValueContent("Foo3");
            setPropRequest.SetProperty(name + "4").SetSingleValueContent("Foo4");
            setPropRequest.SetProperty(name + "5").SetSingleValueContent("Foo5");

            propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var propertyItems = Client.ListPropertiesForResource(new ListPropertiesRequest(_project)).Response;
            Assert.NotNull(propertyItems);
            Assert.Equal(5, propertyItems.Items.Count());

            var prop = propResponse.Items.Where(p => p.Name == name + "3").FirstOrDefault();
            Assert.NotNull(prop);
            Assert.Equal("Foo3", prop.Content.ToString());
        }

        [Fact]
        public void UpdateValuesOnMultiItemProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.singlevalue.updatepropertyvalue").SetSingleValueContent("Foo1");
            setPropRequest.SetProperty("unittest.multiitem.updatepropertyvalue").SetMultiValueContent(new string[2] { "one", "two" });

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.NotNull(propResponse);
            Assert.Equal(2, propResponse.Items.Count());

            setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.multiitem.updatepropertyvalue").SetMultiValueContent(new string[3] { "one", "two", "three" });
            propResponse = Client.SetPropertiesForResource(setPropRequest).Response;

            var property = Client.ListPropertiesForResource(new ListPropertiesRequest(_project)).Response;

            Assert.NotNull(property);
            Assert.NotNull(property.Items);
            Assert.Equal(2, property.Items.Count());

            Assert.Equal("Foo1", property.Items.Where(p => p.Name == "unittest.singlevalue.updatepropertyvalue").FirstOrDefault().Content.ToString());
            Assert.Equal(null, property.Items.Where(p => p.Name == "unittest.singlevalue.updatepropertyvalue").FirstOrDefault().Items);

            Assert.Equal(3, property.Items.Where(p => p.Name == "unittest.multiitem.updatepropertyvalue").FirstOrDefault().Items.Count());
            var prop = property.Items.Where(p => p.Name == "unittest.multiitem.updatepropertyvalue").FirstOrDefault();
            string[] contents = prop.ToStringArray();

            Assert.Equal(3, prop.ItemsDisplayedCount.Value);
            Assert.Equal(3, prop.ItemsTotalCount.Value);
            Assert.Equal(3, contents.Count());
            Assert.True((new string[3] { "one", "two", "three" }).All(i => contents.Contains(i)));
        }

        [Fact]
        public void CreatePropertyWithJsonContent()
        {
            var name = "unittest.singlevalue.contentJson";
            string jsonContent = @"{ ""verification_uri"":""https://basespace.illumina.com/oauth/device"", " + System.Environment.NewLine + "\t" +
            @"""verification_with_code_uri"":""https://basespace.illumina.com/oauth/device?code=b9bac"",    " + "\t" +
            @"""user_code"":""b9bac"", ""expires_in"":1800, " + "\r\n \t" + @"""device_code"":""~!@#$%^&*()_+<>?,"", ""interval"":1      }";

            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetSingleValueContent(jsonContent);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);

            Assert.NotNull(prop);
            Assert.Equal(jsonContent, prop.Content.ToString());
        }

        [Fact]
        public void CreatePropertyWithMultiJsonItems()
        {
            var name = "unittest.singlevalue.contentJson";
            string[] jsonContent = new string[2] {@"{   ""verification_uri"":""https://basespace.illumina.com/oauth/device"", " + System.Environment.NewLine + @"""verification_with_code_uri"":""https://basespace.illumina.com/oauth/device?code=b9bac"" }",
            @"{ ""user_code"":""b9bac"", " + "\r\n \t" + @"""expires_in"":1800, ""device_code"":""~!@#$%^&*()_+<>?,"", ""interval"":1 }"};

            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetMultiValueContent(jsonContent);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);

            Assert.NotNull(prop);
            Assert.Equal(2, prop.Items.Count());
            Assert.True(jsonContent.All(i => prop.ToStringArray().Contains(i)));
        }

        [Fact]
        public void CreatePropertyWithXMLContent()
        {
            var name = "unittest.singlevalue.contentXML";
            string XMLContent = "<Environment><Name>cloud-test</Name>" + System.Environment.NewLine + "\t" +
                @"<BaseUrl>https://cloud-test.illumina.com</BaseUrl>" +
                @"<DefaultToken>~!@#$%^&*()_+<>?</DefaultToken>" + "\r\n \t" +
                @"<BaseSpaceAPIUrl>https://cloud-test-api.illumina.com/@version/</BaseSpaceAPIUrl>  " +
                @"<StoreUrl>https://test-store.basespace.illumina.com</StoreUrl>" +
                @"<VendorUserEmail>basespaceic@gmail.com</VendorUserEmail></Environment>";

            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetSingleValueContent(XMLContent);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);

            Assert.NotNull(prop);
            Assert.Equal(XMLContent, prop.Content.ToString());
        }

        [Fact]
        public void CreatePropertyWithMultiXMLItems()
        {
            var name = "unittest.singlevalue.contentJson";
            string[] XMLContent = new string[2] {"<Environment><Name>cloud-test</Name>" + System.Environment.NewLine + "\t" +
                @"<BaseUrl>https://cloud-test.illumina.com</BaseUrl>" +
                @"<DefaultToken>9b555f57d9e94c9eaee0f77cfe968099</DefaultToken>" + "\r\n \t" +
                @"<BaseSpaceAPIUrl>https://cloud-test-api.illumina.com/@version/</BaseSpaceAPIUrl>" +
                @"<StoreUrl>https://test-store.basespace.illumina.com</StoreUrl>" +
                @"<VendorUserEmail>basespaceic@gmail.com</VendorUserEmail></Environment>",
                "<Environment><Name>cloud-hoth</Name>       " +
                @"<BaseUrl>https://cloud-hoth.illumina.com</BaseUrl>" +
                @"<DefaultToken>~!@#$%^&*()_+<>?</DefaultToken>" + "\r\n \t" +
                @"<BaseSpaceAPIUrl>https://api.cloud-hoth.illumina.com/@version/</BaseSpaceAPIUrl>" + "\r\n \t" +
                @"<StoreUrl>https://hoth-store.basespace.illumina.com</StoreUrl>" +
                @"<VendorUserEmail>basespaceic@gmail.com</VendorUserEmail></Environment>"};

            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetMultiValueContent(XMLContent);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);

            Assert.NotNull(prop);
            Assert.Equal(2, prop.Items.Count());
            Assert.True(XMLContent.All(i => prop.ToStringArray().Contains(i)));
        }

        [Fact]
        public void AddDuplicateResourceItemReferenceToProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.multiitem.duplicateprojects").SetMultiValueReferences(new[] { _project, _project });

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var itemsResponse = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.duplicateprojects")).Response;

            Assert.NotNull(itemsResponse);
            Assert.Equal(1, itemsResponse.Items.Count());

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
        public void AddMultipleHrefItemToProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.multiitem.hrefappresults").SetMultiValueReferences(new[] { "appresults/710710", "appresults/710711" });

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var itemsResponse = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.hrefappresults")).Response;

            Assert.NotNull(itemsResponse);
            Assert.Equal(2, itemsResponse.Items.Count());
            Assert.NotNull(itemsResponse.Items.Where(i => i.Content.ToAppResult().Id == "710710").FirstOrDefault());
            Assert.NotNull(itemsResponse.Items.Where(i => i.Content.ToAppResult().Id == "710711").FirstOrDefault());
        }

        [Fact]
        public void AddMultipleHrefItemNoAccessToResource()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.multiitem.hrefappresults.noaccess").SetMultiValueReferences(new[] { "appresults/447464", "appresults/447465" });

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var itemsResponse = Client.ListPropertyItems(new ListPropertyItemsRequest(_project, "unittest.multiitem.hrefappresults.noaccess")).Response;

            Assert.NotNull(itemsResponse);
            Assert.Equal(0, itemsResponse.Items.Count());
        }
    }
}