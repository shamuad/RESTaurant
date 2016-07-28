using MongoDB.Bson;
using RESTaurant.App.Repository;
using System.Collections.Generic;
using System;

namespace RESTaurant.App.Domain
{
    public class Restaurant
    {
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string RestaurantId { get; private set; }
        public Address Address { get; private set; }
        public string Borough { get; private set; }
        public string Cuisine { get; private set; }
        public IEnumerable<Gradee> Grades { get; private set; }

        public Restaurant()
        {
        }

        public Restaurant(string name, string restaurantId, Address address, string borough, string cuisine, IEnumerable<Gradee> gradeList)
        {
            Id = ObjectId.GenerateNewId();
            Name = name;
            RestaurantId = restaurantId;
            Address = address;
            Borough = borough;
            Cuisine = cuisine;
            Grades = gradeList;
        }

        public void ChangeCuisine(string cuisine) {
            this.Cuisine = cuisine;
        }

        public Restaurant Load(RestaurantDocument document)
        {
            Id = document.Id;
            Name = document.Name;
            RestaurantId = document.RestaurantId;
            Borough = document.Borough;
            Address = document.Address;
            Cuisine = document.Cuisine;
            Grades = document.Grades;
            return this;
        }

        public RestaurantDocument ToDocument()
        {
            var restaurantDocument = new RestaurantDocument();

            restaurantDocument.Id = this.Id;
            restaurantDocument.Name = this.Name;
            restaurantDocument.RestaurantId = this.RestaurantId;
            restaurantDocument.Borough = this.Borough;
            restaurantDocument.Address = this.Address;
            restaurantDocument.Cuisine = this.Cuisine;
            restaurantDocument.Grades = this.Grades;

            return restaurantDocument;
        }
    }
}
