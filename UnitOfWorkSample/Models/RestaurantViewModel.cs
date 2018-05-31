using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.IO;
using UnitOfWorkSample.Helpers;
using System.ComponentModel.DataAnnotations;

namespace UnitOfWorkSample.Models
{
    /// <summary>
    /// The ViewModel class for the Index.cshtml, Details.cshtml and Edit.cshtml
    /// Usually we have to use always 1 ViewModel class for 1 view, but the information for the three views quite similar
    /// </summary>
    public class RestaurantViewModel
    {
        #region Properties
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int CityID { get; set; }

        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
        public string ImageName { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Email { get; set; }


        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public bool HasImage { get; set; }
        public string ImagePath { get; set; }
        #endregion

        #region Constructors

        public RestaurantViewModel()
        {
        }
        public RestaurantViewModel(Restaurant dbRestaurant)
        {
            this.CategoryID = dbRestaurant.CategoryID;
            this.CategoryName = dbRestaurant.Category.Name;
            this.CityID = dbRestaurant.CityID;
            this.CityName = dbRestaurant.City.Name;
            this.Name = dbRestaurant.Name;
            this.DateCreated = dbRestaurant.DateCreated;
            this.Description = dbRestaurant.Description;
            this.Email = dbRestaurant.Email;
            this.ID = dbRestaurant.ID;
            this.ImageName = dbRestaurant.ImageName;

            this.HasImage = string.IsNullOrEmpty(dbRestaurant.ImageName) == false;
            if (this.HasImage)
            {
                this.ImagePath = Path.Combine(Constants.ImagesDirectory, this.ImageName);
                string realPath = System.Web.HttpContext.Current.Server.MapPath(this.ImagePath);
                bool isFileExists = File.Exists(realPath);
                if (isFileExists == false)
                {
                    this.ImagePath = Path.Combine(Constants.ImagesDirectory, "FileNotFound.jpg");
                }
            }
        }
        #endregion

        #region public methods
        public void UpdateDbModel(Restaurant dbRestaurant)
        {
            dbRestaurant.CategoryID = this.CategoryID;
            dbRestaurant.CityID = this.CityID;
            dbRestaurant.Name = this.Name;
            dbRestaurant.Description = this.Description;
        }
        #endregion
    }
}