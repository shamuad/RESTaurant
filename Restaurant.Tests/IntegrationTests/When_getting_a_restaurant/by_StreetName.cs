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
    public class by_StreetName
    {
        public RestaurantRepository repository = new RestaurantRepository();
        private string streetName = "Ulus Vadi";
        private IEnumerable<RestaurantDocument> fetchedRestaurants;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AddSampleRestaurant();
            fetchedRestaurants = repository.GetByStreetName(streetName);
        }

        [Test]
        public void returned_restaurant_should_have_correct_streetname()
        {
            foreach (var fecthedRestaurant in fetchedRestaurants)
            {
                fecthedRestaurant.Address.Street.Should().Be(streetName);
            }
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            repository.DeleteByStreetName(streetName);
        }

        private void AddSampleRestaurant()
        {
            var restaurantToInsert = new RestaurantDocument()
            {
                Id = ObjectId.GenerateNewId(),
                Address = new Address()
                {
                    Building = "Building",
                    Coord = new Coordinate(12D, 15D),
                    Street = streetName,
                    Zipcode = "34350"
                },
                Name = "Aslan Ari Dinar",
                Grades = new List<Gradee>()
                {
                    new Gradee() { Date = DateTime.Today, Grade = "A", Score = 89}
                },
                RestaurantId = "123456",
                Borough = "Borough",
                Cuisine = "Japanies Cuisine",
            };
            repository.Insert(restaurantToInsert);
        }
    }
}
