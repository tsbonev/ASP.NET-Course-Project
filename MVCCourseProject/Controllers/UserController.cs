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
                    TempData["Message"] = "Wrong username or password!";
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

        public ActionResult ViewLikes()
        {
            if (LoginUserSession.Current.IsAuthenticated)
            {
                User user = uow.UserRepository.GetByID(LoginUserSession.Current.UserID);

                ChapterViewModel model = new ChapterViewModel(user.Likes.ToList());

                return View(model);
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult ViewUser()
        {
            if (LoginUserSession.Current.IsAuthenticated)
            {
                User user = uow.UserRepository.GetByID(LoginUserSession.Current.UserID);

                UserViewModel model = new UserViewModel(user);

                return View(model);
            }

            return RedirectToAction("Index", "Home");

        }

        public void Like(int chapterID)
        {

            if (LoginUserSession.Current.IsAuthenticated)
            {
                Chapter chapter = uow.ChapterRepository.GetByID(chapterID);
                User user = uow.UserRepository.GetByID(LoginUserSession.Current.UserID);

                if (user != null && chapter != null)
                {
                    chapter.Likes.Add(user);
                    user.Likes.Add(chapter);

                    uow.Save();

                    TempData["Message"] = "Successfully liked " + chapter.ChapterName;
                    //return RedirectToAction("ViewChapter", "Chapter", new { Chapter = chapter.Story.Slug + "-" + chapter.Slug });


                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to like";
                }
            }

            //return RedirectToAction("Index", "Home");

        }

        public void Unlike(int chapterID)
        {
            if (LoginUserSession.Current.IsAuthenticated)
            {
                Chapter chapter = uow.ChapterRepository.GetByID(chapterID);
                User user = uow.UserRepository.GetByID(LoginUserSession.Current.UserID);

                if (user != null && chapter != null)
                {
                    chapter.Likes.Remove(user);
                    user.Likes.Remove(chapter);

                    uow.Save();

                    TempData["Message"] = "Successfully unliked " + chapter.ChapterName;
                    //return RedirectToAction("ViewChapter", "Chapter", new { Chapter = chapter.Story.Slug + "-" + chapter.Slug });

                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to unlike";
                }
            }

            //return RedirectToAction("Index", "Home");

        }

        [CustomAuthorize]
        public ActionResult EditUsers()
        {
            UserViewModel users = new UserViewModel(uow.UserRepository.GetAll());
            return View(users);
        }

        [CustomAuthorize]
        public ActionResult DeleteUser(int id)
        {
            User user = uow.UserRepository.GetByID(id);

            if(null != user)
            {
                List<Chapter> chapterList = uow.ChapterRepository.GetAll().Where(c => c.Likes.Equals(user)).ToList();
                foreach (Chapter chapter in chapterList)
                {
                    chapter.Likes.Remove(user);
                }

                uow.UserRepository.DeleteByID(id);

                uow.Save();

                TempData["Message"] = "Successfully deleted user";

            }

            TempData["ErrorMessage"] = "User deletion unsuccessful";

            return RedirectToAction("EditUsers");

        }

    }
}