
using DataAccess;
using UnitOfWorkSample.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UnitOfWorkSample.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. get all categories from the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            List<User> allUsers = unitOfWork.UserRepository.GetAll();

            // initialize the model for the View
            List<UserViewModel> model = new List<UserViewModel>();

            // 2. convert all User objects to ViewModel objects
            foreach (User user in allUsers)
            {
                UserViewModel newItem = new UserViewModel(user);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the User to edit
            UnitOfWork unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetByID(id);

            UserViewModel model = new UserViewModel();
            if (user != null)
            {
                // create the viewModel from the User
                model = new UserViewModel(user);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userEdit)
        {
            // find the User in the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            User dbUser = unitOfWork.UserRepository.GetByID(userEdit.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbUser == null)
            {
                dbUser = new User();
            }

            // update the DB object from the viewModel 
            userEdit.UpdateDbModel(dbUser);

            unitOfWork.UserRepository.Save(dbUser);
            unitOfWork.Save();

            TempData["Message"] = "The user was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            bool isDeleted = false;
            UnitOfWork unitOfWork = new UnitOfWork();

            User dbUser = unitOfWork.UserRepository.GetByID(id);
            if (dbUser != null)
            {
                foreach (Comment comment in dbUser.Comments.ToList())
                {
                    unitOfWork.CommentRepository.Delete(comment);
                }
                unitOfWork.UserRepository.Delete(dbUser);
                isDeleted = unitOfWork.Save() > 0;
            }

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a user with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The user was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}