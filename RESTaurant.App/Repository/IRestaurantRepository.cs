using MongoDB.Bson;
using System.Collections.Generic;
using RESTaurant.App.Domain;

namespace RESTaurant.App.Repository
{
    public interface IRestaurantRepository
    {
        RestaurantDocument GetById(ObjectId id);
        IEnumerable<RestaurantDocument> GetByName(string name);
        IEnumerable<RestaurantDocument> GetByCuisine(string cuisine);
        IEnumerable<RestaurantDocument> GetByStreetName(string streetName);
        RestaurantDocument GetByCoordinate(Coordinate coordinate);
        void Insert(RestaurantDocument restaurantDocument);
        void FindAndReplace(RestaurantDocument restaurantDocument);
        void DeleteByName(string restaurantName);
        void DeleteByStreetName(string streetName);
    }
}
