using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests.When_getting_a_restaurant
{
    [TestFixture]
    public class by_Cuisine
    {
        private RestaurantRepository repository;
        private IEnumerable<RestaurantDocument> fetchedRestaurants;
        private string cuisineName = "Korean Cuisine";
        private RestaurantDocument restaurantToInsert;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new RestaurantRepository();
            AddSampleRestaurant();
            fetchedRestaurants = repository.GetByCuisine(restaurantToInsert.Cuisine);
        }

        [Test]
        public void it_should_return_a_restaurant()
        {
            fetchedRestaurants.Should().HaveCount(1);
        }

        [Test]
        public void returned_restaurant_should_have_correct_cuisine()
        {
            foreach (var fetchedRestaurant in fetchedRestaurants)
            {
                fetchedRestaurant.Cuisine.Should().Be(cuisineName);
            }
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            repository.DeleteByName(restaurantToInsert.Name);
        }

        private void AddSampleRestaurant()
        {
            restaurantToInsert = new RestaurantDocument()
            {
                Id = ObjectId.GenerateNewId(),
                Address = new Address()
                {
                    Building = "Building",
                    Coord = new Coordinate(12D, 15D),
                    Street = "Ulus Vadi",
                    Zipcode = "34350"
                },
                Name = "Aslan Ari Dinar",
                Grades = new List<Gradee>(),
                RestaurantId = "123456",
                Borough = "Borough",
                Cuisine = cuisineName,

            };
            repository.Insert(restaurantToInsert);
        }
    }
}
