using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCourseProject.Helpers;
using MVCCourseProject.Models;
using DataAccess;
using Repositories;


namespace MVCCourseProject.Controllers
{
    [CustomAuthorize]
    public class CategoriesController : Controller
    {

        UnitOfWork uow = new UnitOfWork();

        public ActionResult EditCategories()
        {
            CategoryViewModel model = new CategoryViewModel(uow.CategoryRepository.GetAll());
            return View(model);
        }

        public ActionResult DeleteCategory(int id)
        {
            Category category = uow.CategoryRepository.GetByID(id);

            if(null == category)
            {
                TempData["ErrorMessage"] = "Could not find a category with ID = " + id;
            }
            else
            {
                foreach (Story story in category.Stories.ToList())
                {
                    foreach (Chapter chapter in story.Chapters.ToList())
                    {

                        foreach (User user in chapter.Likes.ToList())
                        {
                            chapter.Likes.Remove(user);
                        }

                        uow.ChapterRepository.DeleteByID(chapter.ID);
                    }
                    uow.StoryRepository.DeleteByID(story.ID);
                }

                uow.CategoryRepository.DeleteByID(id);

                uow.Save();

                uow.CategoryRepository.AdjustCategories(category.lft, category.rgt, false);

                uow.Save();

                TempData["Message"] = "The category was deleted successfully";
            }
            

            return RedirectToAction("EditCategories");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            CategoryViewModel model = new CategoryViewModel(uow.CategoryRepository.GetByID(id));
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel category)
        {

            Category dbCategory = uow.CategoryRepository.GetByID(category.ID);
            dbCategory.Name = category.Name;

            uow.CategoryRepository.Update(dbCategory, c => c.ID == category.ID);

            uow.Save();

            return RedirectToAction("EditCategories");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            ViewBag.AllCategories = new SelectList(uow.CategoryRepository.GetAll(), "rgt", "Name");
            CategoryViewModel model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel model)
        {

            Category category = new Category();
            category.Name = model.Name;
            category.lft = model.lft;

            uow.CategoryRepository.AdjustCategories(category.lft, category.rgt, true);
            uow.CategoryRepository.Save(category);

            uow.Save();

            return RedirectToAction("EditCategories");
        }

    }
}