using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests.When_getting_a_restaurant
{
    [TestFixture]
    public class by_Coordinate
    {
        private IRestaurantRepository repository = new RestaurantRepository();
        private RestaurantDocument fetchedRestaurant;
        private RestaurantDocument restaurantToInsert;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AddSampleRestaurant();
            fetchedRestaurant = repository.GetByCoordinate(restaurantToInsert.Address.Coord);
        }

        [Test]
        public void coordinates_should_be_correct()
        {
            fetchedRestaurant.Address.Coord.XCoord.Should().Be(restaurantToInsert.Address.Coord.XCoord);
            fetchedRestaurant.Address.Coord.YCoord.Should().Be(restaurantToInsert.Address.Coord.YCoord);
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
                Coord = new Coordinate(12D, 16D),
                Street = "31st ST",
                Zipcode = "34050"
            };

            restaurantToInsert = new RestaurantDocument()
            {
                Id = ObjectId.GenerateNewId(),
                Address = address,
                Borough = "Besiktas",
                Cuisine = "German Cuisine",
                Grades = new List<Gradee>(),
                Name = "Integration Test",
                RestaurantId = "RestId003"
            };

            repository.Insert(restaurantToInsert);
        }
    }
}
