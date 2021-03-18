using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {

        public SqlRestaurantData(OdeToFoodDBContext db)
        {
            Db = db;
        }

        public OdeToFoodDBContext Db { get; }

        public Restaurant Add(Restaurant newRestaurant)
        {
            Db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return Db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurantToDelete = Db.Restaurants.FirstOrDefault(r => r.ID == id);

            if (restaurantToDelete != null)
            {
                Db.Restaurants.Remove(restaurantToDelete);
            }

            return restaurantToDelete;
        }


        public Restaurant GetById(int id)
        {
            return Db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in Db.Restaurants
                where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                orderby r.Name
                select r;

            return query;
        }

        public Restaurant Remove(Restaurant updatedRestaurant)
        {
            var entity = Db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
