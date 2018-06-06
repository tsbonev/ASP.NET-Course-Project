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

        public ActionResult Logout()
        {
            LoginUserSession.Current.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User dbUser = uow.UserRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.ID, dbUser.Username, dbUser.Username == "admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password");
                    TempData["ErrorMessage"] = "Wrong username or password!";
                }
            }
            return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public ActionResult Like(bool dislike, int id)
        {

            if (LoginUserSession.Current.IsAuthenticated)
            {
                Chapter chapter = uow.ChapterRepository.GetByID(id);
                User user = uow.UserRepository.GetByID(LoginUserSession.Current.UserID);

                if (user != null && chapter != null)
                {
                    if (dislike)
                    {
                        chapter.Likes.Remove(user);
                        user.Likes.Remove(chapter);
                    }
                    else
                    {
                        chapter.Likes.Add(user);
                        user.Likes.Add(chapter);
                    }
                    

                    uow.Save();

                    string liked = dislike ? "disliked" : "liked";

                    TempData["Message"] = "Successfully " + liked + " " + chapter.ChapterName;
                    return RedirectToAction("ViewChapter", "Chapter", new { Chapter = chapter.Story.Slug + "-" + chapter.Slug });

                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong, failed to like";
                }

            }

            return RedirectToAction("Index", "Home");

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