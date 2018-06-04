using MVCCourseProject.Models;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCourseProject.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork uow = new UnitOfWork();

        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Sidebar()
        {
            List<Category> categories = uow.CategoryRepository.GetAll();
            HomeViewModel model = new HomeViewModel(categories);

            return PartialView("_Sidebar", model);
        }

        public ActionResult Car()
        {
            List<Story> stories = uow.StoryRepository.GetAll();
            HomeViewModel model = new HomeViewModel(stories);

            return PartialView("_Car", model);
        }

    }
}