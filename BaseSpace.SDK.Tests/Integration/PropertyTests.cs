using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CreateSingleItemProperty()
        {
            var name = "unittest.singlevalue.contentfoo";
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddProperty(name).SetSingleValueContent("Foo");

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
            setPropRequest.AddProperty(name).SetSingleValueContent(_project);

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
            setPropRequest.AddProperty("unittest.multiitem.rainbow").SetMultiValueContent(RAINBOW);
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
        public void DeleteProperty()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddProperty("unittest.deletetest").SetSingleValueContent("Property to delete");
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;

            var response = Client.DeletePropertyForResource(new DeletePropertyRequest(_project, "unittest.deletetest"));
        }

        [Fact]
        public void GetPropertyVerbose()
        {
            var setPropRequest = new SetPropertiesRequest(_project);
            setPropRequest.AddProperty("unittest.verbosepropertytest").SetMultiValueContent(RAINBOW);
            var prop = Client.SetPropertiesForResource(setPropRequest).Response;
            
            // TODO (maxm): finish this by including app/user/dates in PropertyFull
        }

        [Fact]
        public void MultiItemPaging()
        {

            var setPropRequest = new SetPropertiesRequest(_project);
            var values = new string[100];
            for (int i = 0; i < 100; i++)
            {
                values[i] = i.ToString();
            }

            setPropRequest.AddProperty("unittest.multiitem.manyitems").SetMultiValueContent(values);
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
        public void CreateDuplicateProperty()
        {
            bool IsPassed = false;

            try
            {
                string name = "unittest.duplicateproperty";
                var setPropRequest = new SetPropertiesRequest(_project);
                setPropRequest.AddProperty(name).SetSingleValueContent("Foo");
                setPropRequest.AddProperty(name).SetSingleValueContent("Foo2");

                var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            }
            catch
            {
                //TODO: Add checking on error type/message thrown
                IsPassed = true;
            }

            Assert.True(IsPassed, "User should not be able to add duplicate property names");
        }

        [Fact]
        public void CreateEmptyPropertyName()
        {
            bool IsPassed = false;

            try
            {
                string name = string.Empty;
                var setPropRequest = new SetPropertiesRequest(_project);
                setPropRequest.AddProperty(name).SetSingleValueContent("Foo");

                var propResponse = Client.SetPropertiesForResource(setPropRequest).Response;
            }
            catch
            {
                //TODO: Add checking on error type/message thrown
                IsPassed = true;
            }

            Assert.True(IsPassed, "User should not be able to add an empty property name");
        }
    }


}
