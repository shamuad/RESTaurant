using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests.When_getting_a_restaurant
{
    [TestFixture]
    public class by_id
    {
        private RestaurantRepository repository;
        private RestaurantDocument restaurantToInsert;
        private RestaurantDocument fetchedRestaurant;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new RestaurantRepository();
            AddSampleRestaurant();
            fetchedRestaurant = repository.GetById(restaurantToInsert.Id);
        }

        [Test]
        public void fethced_restaurant_should_not_be_null()
        {
            fetchedRestaurant.Should().NotBeNull();
        }

        [Test]
        public void fetched_restaurant_should_have_correct_id()
        {
            fetchedRestaurant.Id.Should().Be(restaurantToInsert.Id);
        }

        [Test]
        public void fetched_restaurant_should_have_correct_name()
        {
            fetchedRestaurant.Name.Should().Be(restaurantToInsert.Name);
        }

        [Test]
        public void fetched_restaurant_should_have_correct_cuisine()
        {
            fetchedRestaurant.Cuisine.Should().Be(restaurantToInsert.Cuisine);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            repository.DeleteByName(restaurantToInsert.Name);
        }

        private void AddSampleRestaurant()
        {
            var address = new Address
            {
                Building = "Building",
                Coord = new Coordinate(12D,15D),
                Street = "street",
                Zipcode = "zipcode"
            };

            restaurantToInsert = new RestaurantDocument()
            {
                Id = ObjectId.GenerateNewId(),
                Address = address,
                Borough = "Borough",
                Cuisine = "Cuisine",
                Grades = new List<Gradee>(),
                Name = "IntegrationTest",
                RestaurantId = "RestId"
            };
            
           repository.Insert(restaurantToInsert);
        }
    }
}
