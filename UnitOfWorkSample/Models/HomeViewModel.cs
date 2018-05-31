using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using UnitOfWorkSample.Helpers;
using System.IO;

namespace UnitOfWorkSample.Models
{
    public class HomeRestaurantViewModel
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
    }

    public class HomeViewModel
    {
        #region Properties
        public List<HomeRestaurantViewModel> RestaurantsList { get; set; }
        #endregion

        #region Constructors
        public HomeViewModel()
        {
            // initialize the list with Images; it should not be null
            RestaurantsList = new List<HomeRestaurantViewModel>();
        }

        public HomeViewModel(List<Restaurant> allRestaurants)
            : this()
        {
            foreach (Restaurant restaurant in allRestaurants)
            {
                if (string.IsNullOrEmpty(restaurant.ImageName) == false)
                {
                    string imageFullPath = Path.Combine(Constants.ImagesDirectory, restaurant.ImageName);
                    string physicalPath = HttpContext.Current.Server.MapPath(imageFullPath);
                    bool fileExists = File.Exists(physicalPath);
                    if (fileExists == false)
                    {
                        imageFullPath = Path.Combine(Constants.ImagesDirectory, "no_image.png");
                    }

                    HomeRestaurantViewModel item = new HomeRestaurantViewModel();
                    item.ID = restaurant.ID;
                    item.ImagePath = imageFullPath;
                    RestaurantsList.Add(item);
                }
            }
        }
        #endregion
    }
}