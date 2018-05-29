using CourseProject.Helpers;
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
    [CustomAuthorize]
    public class CategoriesController : Controller
    {
        // GET: Category

        public ActionResult EditCategories()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            CategoryViewModel model = new CategoryViewModel(categoryRepository.GetAll());
            return View(model);
        }

        public ActionResult DeleteCategory(int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            Category category = categoryRepository.GetByID(id);
            categoryRepository.DeleteByID(id);
            categoryRepository.AdjustCategories(category.lft, category.rgt, false);
            return RedirectToAction("EditCategories");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            CategoryViewModel model = new CategoryViewModel(categoryRepository.GetByID(id));
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            CategoryRepository categoryRepository = new CategoryRepository();

            Category dbCategory = categoryRepository.GetByID(category.ID);
            dbCategory.Name = category.Name;

            categoryRepository.Update(dbCategory, c => c.ID == category.ID);

            return RedirectToAction("EditCategories");
        }


        [HttpGet]
        public ActionResult AddCategory()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ViewBag.AllCategories = new SelectList(categoryRepository.GetAll(), "rgt", "Name");
            Category category = new Category();
            return View(category);
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            categoryRepository.AdjustCategories(category.lft, category.rgt, true);
            categoryRepository.Save(category);
            return RedirectToAction("EditCategories");
        }

    }
}