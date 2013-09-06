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

        [Fact(Skip = "Just demonstrating SDK usage")]
        public void UsageExample()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("mytestapp.metrics.magicnumber").SetContentString("42");
            //setPropRequest.SetProperty("mytestapp.inputs.appresults").SetContentReferencesArray(new[] { "appresults/3006", "appresults/3005" });
            var map = new PropertyContentMap();
            map.Add("label.x-axis", "energydrinks");
            map.Add("label.y-axis", "productivity");
            map.Add("series.x-axis", "0", "1", "2", "3", "4");
            map.Add("series.y-axis", "5", "7", "8", "4", "1");

            setPropRequest.SetProperty("mytestapp.inputs.metrics").SetContentMap(map);

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
            setPropRequest.SetProperty(name).SetContentString("Foo");

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
            setPropRequest.SetProperty(name).SetContentReference(_project);

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
            setPropRequest.SetProperty("unittest.multiitem.rainbow").SetContentStringArray(RAINBOW);
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
            setPropRequest.SetProperty("unittest.multiitem.projects").SetContentReferencesArray(new[] { _project });
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
            setPropRequest.SetProperty("unittest.deletetest").SetContentString("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;

            var response = Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest"));
        }

        [Fact]
        public void GetPropertyVerbose()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.verbosepropertytest").SetContentStringArray(RAINBOW);
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

            setPropRequest.SetProperty("unittest.multiitem.manyitems").SetContentStringArray(values);
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
            setPropRequest.SetProperty("a").SetContentStringArray(RAINBOW);
            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateDuplicateProperty()
        {
            string name = "unittest.duplicateproperty";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetContentString("Foo");
            setPropRequest.SetProperty(name).SetContentString("Foo2");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.DUPLICATE_NAMES", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateEmptyPropertyName()
        {
            string name = string.Empty;
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetContentString("Foo");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteNonExistingProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.deletetest").SetContentString("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;
            AssertErrorResponse(() => Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest.notexisting")), "BASESPACE.PROPERTIES.NOT_FOUND", HttpStatusCode.NotFound);
        }

        [Fact]
        public void CreatePropertyWithNameGreaterThan64()
        {
            var name = "unittest.singlevalue.propertynamegreaterthan64.01234567891234567890";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetContentString("Foo");

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.NAME_LENGTH", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void CreateMultipleProperties()
        {
            var name = "unittest.multipleproperties.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 65; intCtr++)
                setPropRequest.SetProperty(name + intCtr.ToString()).SetContentString("Foo" + intCtr.ToString());

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
                setPropRequest.SetProperty(name + intCtr.ToString()).SetContentString("Foo" + intCtr.ToString());

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
        }

        [Fact]
        public void GetAPropertyForResource()
        {
            var name = "unittest.getpropertyforaresource.property";
            var setPropRequest = new SetPropertiesRequest(_project);

            for (int intCtr = 1; intCtr <= 5; intCtr++)
                setPropRequest.SetProperty(name + intCtr.ToString()).SetContentString("Foo" + intCtr.ToString());

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
            setPropRequest.SetProperty(name).SetContentString("Foo");

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

            setPropRequest.SetProperty("unittest.multiitem.duplicateitems").SetContentStringArray(values);
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
            setPropRequest.SetProperty(name).SetContentString("");

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
            setPropRequest.SetProperty(name).SetContentStringArray(new string[] {});

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);
            Assert.NotNull(prop);
            Assert.Equal("string[]", prop.Type);
            Assert.Equal(0, prop.Items.Count());
        }

        [Fact]
        public void CreateEmptyMultiItemReferenceProperty()
        {
            var name = "unittest.multivalue.emptyvaluereference";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetContentReferencesArray(new IPropertyContent[] {});

            AssertErrorResponse(
                () => Client.SetPropertiesForResource(setPropRequest), 
                "BASESPACE.PROPERTIES.TYPE_INVALID",
                HttpStatusCode.BadRequest);

            setPropRequest.Properties.First().Type = PropertyTypes.PROJECT + PropertyTypes.LIST_SUFFIX;

            var prop = Client.SetPropertiesForResource(setPropRequest).Response.Items.FirstOrDefault();
            Assert.NotNull(prop);
            Assert.Equal(PropertyTypes.PROJECT + PropertyTypes.LIST_SUFFIX, prop.Type);
            Assert.NotNull(prop.Items);
            Assert.Equal(0, prop.Items.Count());
        }

        [Fact]
        public void UpdatePropertyForResource()
        {
            var name = "unittest.singlevalue.updatepropertyvalue";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name + "1").SetContentString("Foo1");
            setPropRequest.SetProperty(name + "2").SetContentString("Foo2");
            setPropRequest.SetProperty(name + "3").SetContentString("Foo");

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.NotNull(propResponse);
            Assert.Equal(3, propResponse.Items.Count());

            setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name + "3").SetContentString("Foo3");
            setPropRequest.SetProperty(name + "4").SetContentString("Foo4");
            setPropRequest.SetProperty(name + "5").SetContentString("Foo5");

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
            setPropRequest.SetProperty("unittest.singlevalue.updatepropertyvalue").SetContentString("Foo1");
            setPropRequest.SetProperty("unittest.multiitem.updatepropertyvalue").SetContentStringArray(new string[2] { "one", "two" });

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.NotNull(propResponse);
            Assert.Equal(2, propResponse.Items.Count());

            setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.multiitem.updatepropertyvalue").SetContentStringArray(new string[3] { "one", "two", "three" });
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
            setPropRequest.SetProperty(name).SetContentString(jsonContent);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);

            Assert.NotNull(prop);
            Assert.Equal(jsonContent, prop.Content.ToString());
        }

        [Fact]
        public void CreatePropertyWithMultiJsonItems()
        {
            var name = "unittest.multivalue.contentJson";
            string[] jsonContent = new string[2] {@"{   ""verification_uri"":""https://basespace.illumina.com/oauth/device"", " + System.Environment.NewLine + @"""verification_with_code_uri"":""https://basespace.illumina.com/oauth/device?code=b9bac"" }",
            @"{ ""user_code"":""b9bac"", " + "\r\n \t" + @"""expires_in"":1800, ""device_code"":""~!@#$%^&*()_+<>?,"", ""interval"":1 }"};

            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty(name).SetContentStringArray(jsonContent);

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
            setPropRequest.SetProperty(name).SetContentString(XMLContent);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == name);

            Assert.NotNull(prop);
            Assert.Equal(XMLContent, prop.Content.ToString());
        }

        [Fact]
        public void CreatePropertyWithMultiXMLItems()
        {
            var name = "unittest.multivalue.contentXML";
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
            setPropRequest.SetProperty(name).SetContentStringArray(XMLContent);

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
            setPropRequest.SetProperty("unittest.multiitem.duplicateprojects").SetContentReferencesArray(new[] { _project, _project });

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
        public void InvalidContentReference()
        {
            var setPropRequest = new SetPropertiesRequest(_project);

            setPropRequest.SetProperty("unittest.multiitem.invalid.uri").SetContentReferencesArray(new[] { _project.Href.ToString(), "somethinginvalid/1234" });
            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.CONTENT_REFERENCE_INVALID", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void UnresolvedContentReference()
        {
            var setPropRequest = new SetPropertiesRequest(_project);

            setPropRequest.SetProperty("unittest.multiitem.invalid.uri").SetContentReferencesArray(new[] { _project.Href.ToString(), "projects/notexistingproject" });
            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.CONTENT_REFERENCE_NOT_RESOLVED", HttpStatusCode.Conflict);            
        }

        [Fact]
        public void AddEmptyMapList()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var hash = new PropertyContentMap();
            setPropRequest.SetProperty("unittest.hash.empty").SetContentMap(hash);

            var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            var prop = propResponse.Items.FirstOrDefault(p => p.Name == "unittest.hash.empty");
            Assert.NotNull(prop);
            Assert.Equal("map", prop.Type);
            Assert.Equal(null, prop.ToMapArray());
        }

        [Fact]
        public void AddEmptyKeyNameMap()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var hash = new PropertyContentMap();
            hash.Add(string.Empty, "value");
            setPropRequest.SetProperty("unittest.hash.empty").SetContentMap(hash);

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.CONTENT_MAP_KEY_INVALID", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void AddInvalidKeyNameMap()
        {
            var setPropRequest = new SetPropertiesRequest(_project);

            var hash = new PropertyContentMap();
            hash.Add("ab$c", "value");
            setPropRequest.SetProperty("unittest.hash.invalidkey").SetContentMap(hash);

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.CONTENT_MAP_KEY_INVALID", HttpStatusCode.BadRequest);

            hash = new PropertyContentMap();
            hash.Add("unittest.singlevalue.propertynamegreaterthan64.01234567891234567890", "value");
            setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.SetProperty("unittest.hash.invalidkey").SetContentMap(hash);

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.CONTENT_MAP_KEY_INVALID", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void SingleValueMap()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var map = new PropertyContentMap();
            map.Add("rainbow", RAINBOW);
            map.Add("key2", "value2");
            map.Add("rainbow2", RAINBOW);

            setPropRequest.SetProperty("unittest.hash").SetContentMap(map);
            var prop = Client.SetPropertiesForResource(setPropRequest).Response.Items.FirstOrDefault();
            Assert.NotNull(prop);
            Assert.Equal(PropertyTypes.MAP, prop.Type);
            Assert.NotNull(prop.Content);
            map = prop.Content.ToMap();
            Assert.NotNull(map);
            Assert.NotNull(map.FirstOrDefault(h=>h.Key == "rainbow"));
            Assert.True(map.FirstOrDefault(h => h.Key == "rainbow").Values.All(v => RAINBOW.Contains(v)));
            Assert.Equal(3, map.Count());
        }

        [Fact]
        public void SingleValueMapDupes()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var hash = new PropertyContentMap();
            hash.Add("rainbow", RAINBOW);
            hash.Add("rainbow", "value2");
            setPropRequest.SetProperty("unittest.hash.dupe").SetContentMap(hash);

            AssertErrorResponse(() => Client.SetPropertiesForResource(setPropRequest), "BASESPACE.PROPERTIES.CONTENT_MAP_DUPES", HttpStatusCode.BadRequest);
        }

        [Fact]
        public void MapArray()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            var h1 = new PropertyContentMap();
            h1.Add("a1", "r1", "r1a", "r1a");
            h1.Add("a2", "r2");

            var h2 = new PropertyContentMap();
            h2.Add("b1", "b1a", "b1b", "b1c");
            h2.Add("b2", "b2a");


            setPropRequest.SetProperty("unittest.hash.multivalue").SetContentMapArray(new[] {h1, h2});
            var props = Client.SetPropertiesForResource(setPropRequest).Response;
            Assert.Equal(1, props.Items.Count());

            var prop = props.Items.First();

            Assert.Equal(PropertyTypes.MAP + PropertyTypes.LIST_SUFFIX, prop.Type);
            Assert.Equal(2, prop.Items.Count());
            Assert.Equal(2, prop.ItemsDisplayedCount);
            Assert.Equal(2, prop.ItemsTotalCount);

            Assert.Equal(new[] { "r1", "r1a", "r1a" }, prop.Items[0].ToMap()["a1"].Values);
            Assert.Equal(new[] { "r2" }, prop.Items[0].ToMap()["a2"].Values);

            Assert.Equal(new[] { "b1a", "b1b", "b1c" }, prop.Items[1].ToMap()["b1"].Values);
            Assert.Equal(new[] { "b2a" }, prop.Items[1].ToMap()["b2"].Values);
        }
    }
}