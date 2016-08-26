using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RESTaurant.App.Domain;
using System.Linq;
using System.Collections.Generic;
using System.Configuration.Abstractions;

namespace RESTaurant.App.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IMongoCollection<RestaurantDocument> collection;

        public RestaurantRepository()
        {

            var mongoConnectionString = ConfigurationManager.Instance.AppSettings["MongoConnectionString"];
            var restaurantDbName = ConfigurationManager.Instance.AppSettings["RestaurantDBName"];
            var restaurantCollectionName = ConfigurationManager.Instance.AppSettings["RestaurantCollectionName"];

            RegisterDocuments();

            var client = new MongoClient(mongoConnectionString);
            var database = client.GetDatabase(restaurantDbName);
            collection = database.GetCollection<RestaurantDocument>(restaurantCollectionName);
        }

        public RestaurantDocument GetById(ObjectId id)
        {
            return collection.Find(r => r.Id == id).ToList().First(); // => isareti oyle ki anlamindadir. 
        }

        public IEnumerable<RestaurantDocument> GetByName(string name)
        {
            return collection.Find(r => r.Name == name).ToList();
        }

        public IEnumerable<RestaurantDocument> GetByCuisine(string cuisine)
        {
            return collection.Find(r => r.Cuisine == cuisine).ToList();
        }

        public IEnumerable<RestaurantDocument> GetByStreetName(string streetName)
        {
            return collection.Find(r => r.Address.Street == streetName).ToList();
        }

        public RestaurantDocument GetByCoordinate(Coordinate coordinate)
        {
            return 
                collection.Find(
                    r => r.Address.Coord.XCoord == coordinate.XCoord 
                    && r.Address.Coord.YCoord == coordinate.YCoord)
                    .First();
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

        public void DeleteByStreetName(string streetName)
        {
            var filter = Builders<RestaurantDocument>.Filter.Eq(rd => rd.Address.Street, streetName);
            collection.DeleteMany(filter);
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
