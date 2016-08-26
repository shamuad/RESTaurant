using System.Collections.Generic;
using Castle.Core.Internal;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests.When_getting_a_restaurant
{
    [TestFixture]
    public class by_name
    {
        private RestaurantRepository repository;
        private string restaurantName = "AriDinar";
        private IEnumerable<RestaurantDocument> fetchedRestaurantDocuments;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new RestaurantRepository();

            AddSampleRestaurant(restaurantName);
            AddSampleRestaurant(restaurantName);
            AddSampleRestaurant("DinarAli");

            fetchedRestaurantDocuments = repository.GetByName(restaurantName);
        }

        [Test]
        public void it_should_return_two_restaurants()
        {
            fetchedRestaurantDocuments.Should().HaveCount(2);
        }

        [Test]
        public void returned_restaurants_should_have_correct_name()
        {
            foreach (var fetchedRestaurantDocument in fetchedRestaurantDocuments)
            {
                fetchedRestaurantDocument.Name.Should().Be(restaurantName);
            }

            /* With LINQ */
            //fetchedRestaurantDocuments.ForEach(restaurant =>
            //{
            //    restaurant.Name.Should().Be(restaurantName);
            //});
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            repository.DeleteByName(restaurantName);
            repository.DeleteByName("DinarAli");
        }

        private void AddSampleRestaurant(string name)
        {
            var address = new Address
            {
                Building = "Building",
                Coord = new List<double> { 12D, 15D },
                Street = "street",
                Zipcode = "zipcode"
            };

            repository.Insert(new RestaurantDocument()
            {
                Id = ObjectId.GenerateNewId(),
                Address = address,
                Borough = "Borough",
                Cuisine = "Cuisine",
                Grades = new List<Gradee>(),
                Name = name,
                RestaurantId = "RestId"
            });
        }

    }
}
