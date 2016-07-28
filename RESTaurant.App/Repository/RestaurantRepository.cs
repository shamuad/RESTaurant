using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RESTaurant.App.Domain;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RESTaurant.App.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IMongoCollection<RestaurantDocument> collection;

        public RestaurantRepository()
        {
            RegisterDocuments();
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("testdb");
            collection = database.GetCollection<RestaurantDocument>("primer-dataset");
        }

        public RestaurantDocument GetById(ObjectId id)
        {
            return collection.Find(r => r.Id == id).ToList().First(); // => isareti oyle ki anlamindadir. 
        }

        public IEnumerable<RestaurantDocument> GetByName(string name)
        {
            return collection.Find(r => r.Name == name).ToList();
        }

        public void Insert(RestaurantDocument restaurantDocument)
        {
            collection.InsertOne(restaurantDocument);
        }

        public void FindAndReplace(RestaurantDocument restaurantDocument)
        {
            var filter = Builders<RestaurantDocument>.Filter.Eq(rd => rd.Id, restaurantDocument.Id);
            collection.ReplaceOne(filter, restaurantDocument);
        }

        public void DeleteByName(string restaurantName)
        {
            var filter = Builders<RestaurantDocument>.Filter.Eq(rd => rd.Name, restaurantName);
            collection.DeleteOne(filter);
        }

        private void RegisterDocuments()
        {
            BsonClassMap.RegisterClassMap<Gradee>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.Date).SetElementName("date");
                cm.GetMemberMap(c => c.Grade).SetElementName("grade");
                cm.GetMemberMap(c => c.Score).SetElementName("score");
            });

            BsonClassMap.RegisterClassMap<Address>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.Building).SetElementName("building");
                cm.GetMemberMap(c => c.Zipcode).SetElementName("zipcode");
                cm.GetMemberMap(c => c.Street).SetElementName("street");
                cm.GetMemberMap(c => c.Coord).SetElementName("coord");
            });

            BsonClassMap.RegisterClassMap<RestaurantDocument>(cm =>
            {
                cm.AutoMap();
                cm.GetMemberMap(c => c.Name).SetElementName("name");
                cm.GetMemberMap(c => c.Address).SetElementName("address");
                cm.GetMemberMap(c => c.Borough).SetElementName("borough");
                cm.GetMemberMap(c => c.Cuisine).SetElementName("cuisine");
                cm.GetMemberMap(c => c.Id).SetElementName("_id");
                cm.GetMemberMap(c => c.RestaurantId).SetElementName("restaurant_id");
                cm.GetMemberMap(c => c.Grades).SetElementName("grades");
            });
        }


    }
}
