using MongoDB.Bson;
using RESTaurant.App.Domain;
using System.Collections.Generic;

namespace RESTaurant.App.Service
{
    public interface IRestaurantService
    {
        Restaurant GetById(ObjectId id);
        IEnumerable<Restaurant> GetByName(string name);
        IEnumerable<Restaurant> GetByCuisine(string cuisine);
        IEnumerable<Restaurant> GeyByStreetName(string streetName);
        Restaurant GetByCoordinate(double xCoord, double yCoord);
        void Insert(Restaurant restaurant);
        void Update(Restaurant restaurant);
        void DeleteByName(string restaurantName);
        void DeleteByStreetName(string streetName);
    }
}
