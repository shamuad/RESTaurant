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
    public class When_updating_a_restaurant
    {
        private RestaurantDocument restaurantDocument;
        private readonly RestaurantRepository restaurantRepository = new RestaurantRepository();
        private RestaurantDocument fetchedData;
        private string updatedName = "Updated_Name";

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            CreateSampleData();
            ManipulateData();
            restaurantRepository.FindAndReplace(restaurantDocument);
            fetchedData = restaurantRepository.GetById(restaurantDocument.Id);
        }

        [Test]
        public void name_should_be_updated()
        {
            fetchedData.Name.Should().Be(updatedName);
        }

        [Test]
        public void updated_grade_list_should_be_empty()
        {
            fetchedData.Grades.Should().HaveCount(0);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            restaurantRepository.DeleteByName(restaurantDocument.Name);
        }

        private void ManipulateData()
        {
            restaurantDocument.Name = updatedName;
            restaurantDocument.Grades = new List<Gradee>();
        }

        private void CreateSampleData()
        {
            restaurantDocument = new RestaurantDocument()
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

            restaurantRepository.Insert(restaurantDocument);
        }
    }
}
