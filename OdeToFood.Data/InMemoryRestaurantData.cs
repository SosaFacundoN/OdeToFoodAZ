using System;
using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { ID = 1, Name = "Scott's Pizza", Location = "CABA", Cuisine = Restaurant.CuisineType.Italian},
                new Restaurant { ID = 2, Name = "Marie's Fstake", Location = "Inaba", Cuisine = Restaurant.CuisineType.None},
                new Restaurant { ID = 3, Name = "Alf's Cats", Location = "Rosario", Cuisine = Restaurant.CuisineType.Indian},
                new Restaurant { ID = 4, Name = "Kiwi's", Location = "Villa Luro", Cuisine = Restaurant.CuisineType.Mexican}
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.ID = restaurants.Max(r => r.ID) + 1;
            return newRestaurant;
        }

        public Restaurant Remove(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.ID == updatedRestaurant.ID);
            if (restaurant != null)    
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
        public int Commit()
        {
            return 0;
        }
        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.ID == id);
        }


        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)                   
                   orderby r.Name
                   select r;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.ID == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public void Delete(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
