using DataAccess;
using UnitOfWorkSample.Helpers;
using UnitOfWorkSample.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitOfWorkSample.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            UnitOfWork unitOfWork = new UnitOfWork();
            List<Restaurant> allRestauraurants = unitOfWork.RestaurantRepository.GetAll();

            List<RestaurantViewModel> model = new List<RestaurantViewModel>();
            foreach (Restaurant dbRestaurant in allRestauraurants)
            {
                RestaurantViewModel restaurantViewModel = new RestaurantViewModel(dbRestaurant);
                model.Add(restaurantViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            // add the Cities to the Viewbag
            CityRepository cityRepository = new CityRepository();
            List<City> allCities = cityRepository.GetAll();
            ViewBag.AllCities = new SelectList(allCities, "ID", "Name");

            // add the Categories to the Viewbag
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Category> allCategories = unitOfWork.CategoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories, "ID", "Name");

            // create the viewmodel, based on the Restaurant from the database
            RestaurantViewModel model = new RestaurantViewModel();
            Restaurant dbRestaurant = unitOfWork.RestaurantRepository.GetByID(id);
            if (dbRestaurant != null)
            {
                model = new RestaurantViewModel(dbRestaurant);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RestaurantViewModel viewModel)
        {
            if (viewModel == null)
            {
                // this should not be possible, but just in case, validate
                TempData["ErrorMessage"] = "Ups, a serious error occured: No Viewmodel.";
                return RedirectToAction("Index");
            }

            // get the item from the database by ID
            UnitOfWork unitOfWork = new UnitOfWork();
            Restaurant dbRestaurant = unitOfWork.RestaurantRepository.GetByID(viewModel.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbRestaurant == null)
            {
                dbRestaurant = new Restaurant();
                dbRestaurant.DateCreated = DateTime.Now;
            }

            // update the DB object from the viewModel 
            viewModel.UpdateDbModel(dbRestaurant);

            // save the image to the local folder if there is an uploaded image
            // we have to generate a unique file name, to avoid duplication when the same image is uploaded for another restaurant
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                // check if the uploaded file is a valid file and only then proceed
                if (file.ContentLength > 0 && string.IsNullOrEmpty(file.FileName) == false)
                {
                    string imagesPath = Server.MapPath(Constants.ImagesDirectory);
                    string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName);
                    string savedFileName = Path.Combine(imagesPath, Path.GetFileName(uniqueFileName));
                    file.SaveAs(savedFileName);
                    dbRestaurant.ImageName = uniqueFileName;
                }
            }

            unitOfWork.RestaurantRepository.Save(dbRestaurant);
            unitOfWork.Save();

            TempData["Message"] = "The restaurant was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            
            bool isDeleted = false;

            Restaurant dbRestaurant = unitOfWork.RestaurantRepository.GetByID(id);
            if (dbRestaurant != null)
            {
                foreach (Comment comment in dbRestaurant.Comments.ToList())
                {
                    unitOfWork.CommentRepository.Delete(comment);
                }
                unitOfWork.RestaurantRepository.Delete(dbRestaurant);
                isDeleted = unitOfWork.Save() > 0;
            }

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a restaurant with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The restaurant was deleted successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            // get the DB object
            Restaurant dbRestaurant = unitOfWork.RestaurantRepository.GetByID(id);
            if (dbRestaurant == null)
            {
                // when we have RedirectToAction, we can not use Viewbag - so we use a TempData!
                TempData["ErrorMessage"] = "Could not find a restaurant with ID = " + id;
                return RedirectToAction("Index");
            }
            else
            {
                // create the view model
                RestaurantViewModel model = new RestaurantViewModel(dbRestaurant);
                return View(model);
            }
        }
    }
}