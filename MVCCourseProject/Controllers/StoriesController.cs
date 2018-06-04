using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;

namespace MVCCourseProject.Models
{
    public class StoriesController : Controller
    {
        // GET: Stories
        public ActionResult Index()
        {
            return View();
        }
    }
}