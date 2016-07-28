using System;
using System.Collections.Generic;
using MongoDB.Bson;
using RESTaurant.App.Domain;
using RESTaurant.App.Repository;
using System.Linq;
using RESTaurant.App.Domain.Exceptions;

namespace RESTaurant.App.Service
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository repository;

        public RestaurantService()
        {
            repository = new RestaurantRepository();
        }

        public Restaurant GetById(ObjectId id)
        {
            return new Restaurant().Load(repository.GetById(id));
        }

        public IEnumerable<Restaurant> GetByName(string name)
        {
            var restaurantList = new List<Restaurant>();

            foreach (var item in repository.GetByName(name))
            {
                var restaurant = new Restaurant();
                restaurantList.Add(restaurant.Load(item));
            }
            return restaurantList;
        }

        public void Insert(Restaurant restaurant)
        {
            EnsureRestaurantNameIsNotUsed(restaurant);
            repository.Insert(restaurant.ToDocument());
        }

        private void EnsureRestaurantNameIsNotUsed(Restaurant restaurant)
        {
            var restaurantsFetched = repository.GetByName(restaurant.Name);

            if (restaurantsFetched.Any())
            {
                throw new RestaurantNameAlreadyExistsException();
            }
        }

        public void Update(Restaurant restaurant)
        {
            repository.FindAndReplace(restaurant.ToDocument());
        }

        public void DeleteByName(string restaurantName)
        {
            repository.DeleteByName(restaurantName);
        }
    }
}
