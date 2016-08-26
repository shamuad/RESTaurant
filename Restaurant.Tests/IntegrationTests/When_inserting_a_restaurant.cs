using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FluentAssertions;
using MongoDB.Bson;
using NUnit.Framework;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests
{
    [TestFixture]
    public class When_inserting_a_restaurant
    {
        private readonly RestaurantRepository restaurantRepository = new RestaurantRepository();
        private RestaurantDocument fetchedRestaurant;
        private RestaurantDocument restaurantToInsert;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AddSampleRestaurant();
            fetchedRestaurant = restaurantRepository.GetById(restaurantToInsert.Id);
        }

        [Test]
        public void it_should_not_be_null()
        {
            fetchedRestaurant.Should().NotBeNull();
        }

        [Test]
        public void it_should_have_correct_id()
        {
            fetchedRestaurant.Id.Should().Be(restaurantToInsert.Id);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            restaurantRepository.DeleteByName(restaurantToInsert.Name);
        }


        public void AddSampleRestaurant()
        {
            restaurantToInsert = new RestaurantDocument()
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
                    Coord = new List<double>() { 12D,15D},
                    Street = "LEYLAK",
                    Zipcode = "34050"
                }
            };
            restaurantRepository.Insert(restaurantToInsert);
        }
    }
}
