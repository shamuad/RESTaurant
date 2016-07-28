using MongoDB.Bson;
using RESTaurant.App.Domain;
using System.Collections.Generic;
using System;

namespace RESTaurant.App.Repository
{
    public class RestaurantDocument
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string RestaurantId { get; set; }
        public Address Address { get; set; }
        public string Borough { get; set; }
        public string Cuisine { get; set; }
        public IEnumerable<Gradee> Grades { get; set; }
    }
}
