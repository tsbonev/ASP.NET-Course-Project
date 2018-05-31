using DataAccess;
using UnitOfWorkSample.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitOfWorkSample.Helpers;

namespace UnitOfWorkSample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Restaurant> allRestaurants = unitOfWork.RestaurantRepository.GetAll();

            HomeViewModel model = new HomeViewModel(allRestaurants);
            return View(model);
        }

        public ActionResult Search(string searchVal)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            // just in case prevent NullPointerException
            if (searchVal == null)
            {
                searchVal = string.Empty;
            }

            // compare all words to lower case
            searchVal = searchVal.ToLower();

			// use lambda expression to filter the matched objects
            List<Restaurant> foundRestarants = unitOfWork.RestaurantRepository.GetAll()
                .Where(c => c.Name.ToLower().Contains(searchVal)
                    || c.Description.ToLower().Contains(searchVal))
                .ToList();

			// convert the DB objects to ViewModel objects
            List<SearchViewModel> model = new List<SearchViewModel>();
            foreach (Restaurant dbRestaurant in foundRestarants)
            {
                SearchViewModel modelItem = new SearchViewModel(dbRestaurant);
                model.Add(modelItem);
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // here we have to check if the username exists in the database
                UnitOfWork unitOfWork = new UnitOfWork();
                User dbUser = unitOfWork.UserRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.ID, dbUser.Username, dbUser.IsAdministrator);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }

            // if we are here, this means there is some validation error and we have to show the login screen again
            return View();
        }

        public ActionResult Logout()
        {
            LoginUserSession.Current.Logout();
            return RedirectToAction("Index");
        }
    }
}