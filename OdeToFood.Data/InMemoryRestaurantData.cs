using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id=1,
                    Name="Scott's Pizza",
                    Location = "Maryland",
                    Cuisine = CuisineType.Italian
                },
                new Restaurant()
                {
                    Id=2,
                    Name="Cinnamon Club",
                    Location = "London",
                    Cuisine = CuisineType.Indian
                },
                new Restaurant()
                {
                    Id=3,
                    Name="La Costa",
                    Location = "California",
                    Cuisine = CuisineType.Mexican
                }

            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = "")
        {
            //return restaurants.OrderBy(r => r.Name).Where(r => r.Name.StartsWith(name) || string.IsNullOrEmpty(name)).ToList();
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            Restaurant restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public Restaurant Add(Restaurant createdRestaurant)
        {
            restaurants.Add(createdRestaurant);
            createdRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return createdRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null) restaurants.Remove(restaurant);
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
