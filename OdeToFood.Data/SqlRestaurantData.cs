using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Restaurant Add(Restaurant createdRestaurant)
        {
            dbContext.Restaurants.Add(createdRestaurant);
            return createdRestaurant;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = GetById(id);
            if (restaurant != null) dbContext.Restaurants.Remove(restaurant);
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return dbContext.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            IOrderedQueryable<Restaurant> query = from r in dbContext.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;

        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = dbContext.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;

        }
        public int GetCountOfRestaurants()
        {
            return dbContext.Restaurants.Count();
        }
    }
}
