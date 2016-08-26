using System;
using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests
{
    [TestFixture]
    public class When_deleting_a_restaurant
    {
        private readonly IRestaurantRepository restaurantRepository = new RestaurantRepository();
        private RestaurantDocument sampleRestaurant;
        private IEnumerable<RestaurantDocument> fetchedRestaurants;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            CreateSampleData();
            restaurantRepository.DeleteByName(sampleRestaurant.Name);
            fetchedRestaurants = restaurantRepository.GetByName(sampleRestaurant.Name);
        }

        [Test]
        public void created_data_should_be_deleted()
        {
            fetchedRestaurants.Should().HaveCount(0);
        }

        private void CreateSampleData()
        {
            sampleRestaurant = new RestaurantDocument()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Anil",
                Cuisine = "German",
                Borough = "Borough",
                Grades = new List<Gradee>()
                {
                    new Gradee()
                    {
                        Date = DateTime.Today,
                        Score = 85,
                        Grade = "B"
                    }
                },
                RestaurantId = "Rest1234",
                Address = new Address()
                {
                    Building = "A BLOK",
                    Coord = new List<double>() { 12D, 15D },
                    Street = "LEYLAK",
                    Zipcode = "34050"
                }
            };

            restaurantRepository.Insert(sampleRestaurant);
        }
    }
}
