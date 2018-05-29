using CourseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using Repositories;
using CourseProject.Helpers;

namespace CourseProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        public ActionResult UserCount()
        {

            UserRepository userRepository = new UserRepository();
            int count = userRepository.GetAll().Count;

            TempData["Count"] = count;
            return View();
        }


        public ActionResult LoginSpan()
        {
            if(!LoginUserSession.Current.IsAuthenticated)
            {
                LoginViewModel model = new LoginViewModel();
                return PartialView("_LoginSpan", model);
            }
            else
            {
                UserRepository userRepository = new UserRepository();
                LoginViewModel model = new LoginViewModel();
                model.Username = LoginUserSession.Current.Username;
                model.FirstName = userRepository.GetUserByName(model.Username).FirstName;
                return PartialView("_LoginSpan", model);
            }
        }

        public void Logout()
        {
            LoginUserSession.Current.Logout();
            Response.Redirect("../");
        }

        [HttpPost]
        public void Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                UserRepository userRepository = new UserRepository();
                User dbUser = userRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.ID, dbUser.Username, dbUser.Username == "admin");
                    Response.Redirect("../");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                }
            }

            Response.Redirect("../");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (LoginUserSession.Current.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();

                User existingDbUser = userRepository.GetUserByName(viewModel.Username);
                if (existingDbUser != null)
                {
                    ModelState.AddModelError("", "This user is already registered in the system!");
                    return View();
                }

                User dbUser = new User();
                dbUser.Username = viewModel.Username;
                dbUser.FirstName = viewModel.FirstName;
                dbUser.LastName = viewModel.LastName;

                userRepository.RegisterUser(dbUser, viewModel.Password);

                TempData["Message"] = "User was registered successfully";
                return RedirectToAction("Index", "Home", new { });
            }
            else
            {
                return View();
            }
        }

    }
}