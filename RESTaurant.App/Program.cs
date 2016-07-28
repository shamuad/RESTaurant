using MongoDB.Bson;
using RESTaurant.App.Domain;
using RESTaurant.App.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTaurant.App
{
    class Program
    {
        private static RestaurantService restaurantService = new RestaurantService();

        static void Main(string[] args)
        {
            CheckGetById();
            CheckGetByName();
            CheckInsert();
            CheckUpdate();
            CheckDelete();
        }

        private static void CheckDelete()
        {
            InsertSampleData();
            restaurantService.DeleteByName("aridinar");
            var restaurants = restaurantService.GetByName("aridinar");
        } //Break Point here

        private static void CheckInsert()
        {
            InsertSampleData();
            var restaurants = restaurantService.GetByName("aridinar");
            restaurantService.DeleteByName("aridinar"); //Break Point here
        }

        private static void CheckGetByName()
        {
            var restaurants = restaurantService.GetByName("Vella");
        } //Break Point here

        private static void CheckGetById()
        {
            var restaurant = restaurantService.GetById(new ObjectId("5790fca9b9dc400f608e4eb9"));
        } //Break Point here

        private static void CheckUpdate()
        {
            InsertSampleData();

            var restaurant = restaurantService.GetByName("aridinar").First();
            restaurant.ChangeCuisine("French");
            restaurantService.Update(restaurant);

            var updatedRestaurant = restaurantService.GetByName("aridinar").First();
            restaurantService.DeleteByName("aridinar"); //Break Point here
        }

        private static void InsertSampleData()
        {
            var address = new Address()
            {
                Building = "Building",
                Coord = new List<double>() { 12D, 15D },
                Street = "street",
                Zipcode = "zipcode"
            };

            var restaurantToInsert =
                new Restaurant("aridinar",
                "reaafds",
                address,
                "Borough",
                "turkish",
                new List<Gradee>() {
                   new Gradee()
                   {
                       Date = DateTime.Now,
                       Grade = "A",
                       Score = 90
                   }
                });

            restaurantService.Insert(restaurantToInsert);
        }
    }
}
