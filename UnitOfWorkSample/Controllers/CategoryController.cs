
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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. get all categories from the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Category> allCategories = unitOfWork.CategoryRepository.GetAll();

            // initialize the model for the View
            List<CategoryViewModel> model = new List<CategoryViewModel>();

            // 2. convert all Category objects to ViewModel objects
            foreach (Category category in allCategories)
            {
                CategoryViewModel newItem = new CategoryViewModel(category);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the Category to edit
            UnitOfWork unitOfWork = new UnitOfWork();
            Category category = unitOfWork.CategoryRepository.GetByID(id);

            CategoryViewModel model = new CategoryViewModel();
            if (category != null)
            {
                // create the viewModel from the Category
                model = new CategoryViewModel(category);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryEdit)
        {
            // find the Category in the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            Category dbCategory = unitOfWork.CategoryRepository.GetByID(categoryEdit.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbCategory == null)
            {
                dbCategory = new Category();
            }

            // update the DB object from the viewModel 
            categoryEdit.UpdateDbModel(dbCategory);

            unitOfWork.CategoryRepository.Save(dbCategory);
            unitOfWork.Save();

            TempData["Message"] = "The category was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.CategoryRepository.DeleteByID(id);
            bool isDeleted = unitOfWork.Save() > 0;

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a category with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The category was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}