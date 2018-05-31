using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using UnitOfWorkSample.Helpers;
using System.IO;

namespace UnitOfWorkSample.Models
{
    public class SearchViewModel
    {
        #region Properties
        public int RestaurantID { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantImageUrl { get; set; }
        public string CityName { get; set; }
        public string CategoryName { get; set; }

        public bool HasImage { get; set; }
        #endregion

        #region Constructors
        public SearchViewModel(Restaurant dbRestaurant)
        {
            this.RestaurantID = dbRestaurant.ID;
            this.RestaurantName = dbRestaurant.Name;
            this.RestaurantDescription = dbRestaurant.Description;

            // ! Important - notice how we get the City Name (lazy loading)
            this.CityName = dbRestaurant.City.Name;

            // ! Important - notice how we get the Category Name (lazy loading)
            this.CategoryName = dbRestaurant.Category.Name;

            // we have to check if the image exists, otherwise the UI crashes if there is no valid image 
            if (string.IsNullOrEmpty(dbRestaurant.ImageName) == false)
            {
                string imageFullPath = Path.Combine(Constants.ImagesDirectory, dbRestaurant.ImageName);
                string physicalPath = HttpContext.Current.Server.MapPath(imageFullPath);
                bool fileExists = File.Exists(physicalPath);
                if (fileExists == false)
                {
                    imageFullPath = Path.Combine(Constants.ImagesDirectory, "no_image.png");
                }
                // string imageFullPath = Path.Combine(Constants.ImagesDirectory, dbRestaurant.ImageName);

                this.HasImage = true;
                this.RestaurantImageUrl = imageFullPath;
            }
        }
        #endregion
    }
}