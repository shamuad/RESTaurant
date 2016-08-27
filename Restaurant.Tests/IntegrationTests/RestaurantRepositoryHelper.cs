using System.Collections.Generic;
using MongoDB.Bson;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;

namespace Restaurant.Tests.IntegrationTests
{
    public class RestaurantRepositoryHelper
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantRepositoryHelper(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }

        public void AddSampleRestaurant(ObjectId id, string name = null)
        {
            var address = new Address
            {
                Building = "Building",
                Coord = new Coordinate(12D, 15D),
                Street = "street",
                Zipcode = "zipcode"
            };

            var restaurantDocument = new RestaurantDocument()
            {
                Id = id,
                Address = address,
                Borough = "Borough",
                Cuisine = "Cuisine",
                Grades = new List<Gradee>(),
                Name = name ?? "IntegrationTest",
                RestaurantId = "RestId"
            };

            restaurantRepository.Insert(restaurantDocument);
        }
    }
}
