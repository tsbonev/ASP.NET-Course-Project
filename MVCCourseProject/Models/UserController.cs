using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using MVCCourseProject.Helpers;

namespace MVCCourseProject.Models
{
    public class UserController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        public ActionResult LoginSpan()
        {
            LoginViewModel model = new LoginViewModel();
            if (!LoginUserSession.Current.IsAuthenticated)
            {
                return PartialView("_LoginSpan", model);
            }
            else
            {
                model.Username = LoginUserSession.Current.Username;
                model.FirstName = uow.UserRepository.GetUserByName(model.Username).FirstName;
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
                User dbUser = uow.UserRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

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

                User existingDbUser = uow.UserRepository.GetUserByName(viewModel.Username);
                if (existingDbUser != null)
                {
                    ModelState.AddModelError("", "This user is already registered in the system!");
                    return View();
                }

                User dbUser = new User();
                dbUser.Username = viewModel.Username;
                dbUser.FirstName = viewModel.FirstName;
                dbUser.LastName = viewModel.LastName;

                uow.UserRepository.RegisterUser(dbUser, viewModel.Password);

                uow.Save();

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