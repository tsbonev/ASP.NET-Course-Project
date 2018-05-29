using CourseProject.Models;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ChapterRepository chapterRepository = new ChapterRepository();
            Chapter CurrentChapter = chapterRepository.Current();
            HomeViewModel model = new HomeViewModel(CurrentChapter);
       
            return View(model);
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
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> categories = categoryRepository.GetAll();
            HomeViewModel model = new HomeViewModel(categories);

            return PartialView("_Sidebar", model);
        }

        public ActionResult Car()
        {
            StoryRepository storyRepository = new StoryRepository();
            List<Story> stories = storyRepository.GetAll();
            HomeViewModel model = new HomeViewModel(stories);

            return PartialView("_Car", model);
        }

    }
}