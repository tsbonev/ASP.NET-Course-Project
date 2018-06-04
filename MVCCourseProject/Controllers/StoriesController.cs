using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using MVCCourseProject.Models;
using MVCCourseProject.Helpers;
using DataAccess;
using Repositories;
using System.IO;

namespace MVCCourseProject.Models
{
    [CustomAuthorize]
    public class StoriesController : Controller
    {

        UnitOfWork uow = new UnitOfWork();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string Category, string Story)
        {

            Story = Request.QueryString["Story"];
            Category = Request.QueryString["Category"];

            if (String.IsNullOrEmpty(Story)) Story = "";
            if (String.IsNullOrEmpty(Category)) Category = "Stories";

            SearchViewModel model = new SearchViewModel(Story, Category);

            List<Category> categories = uow.CategoryRepository.GetAll();

            List<CategoriesViewModel> categoriesViewModel = new CategoryViewModel(categories).Categories.ToList();

            model.Categories = categoriesViewModel;

            model.Stories = new StoryViewModel(uow.StoryRepository.GetAll());

            return Index(model);

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(SearchViewModel searchModel)
        {

            List<Category> categories = uow.CategoryRepository.GetAll();

            List<CategoriesViewModel> categoriesViewModel = new CategoryViewModel(categories).Categories.ToList();

            searchModel.Categories = categoriesViewModel;

            List<Story> stories = uow.StoryRepository.GetStoryByCategory(uow.CategoryRepository.GetCategoryTree(searchModel.Category))
                .Where(s => s.Name.Contains(searchModel.Story)).ToList();

            StoryViewModel model = new StoryViewModel(stories);

            searchModel.Stories = model;

            return View(searchModel);
        }

        public ActionResult EditStories()
        {
            StoryViewModel stories = new StoryViewModel(uow.StoryRepository.GetAll());
            return View(stories);
        }

        public ActionResult Delete(int id = 0)
        {
            bool isDeleted = uow.StoryRepository.DeleteByID(id);

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a story with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The story was deleted successfully";
            }

            uow.Save();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            List<Category> allCategories = uow.CategoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories, "ID", "Name");

            StoryViewModel model = new StoryViewModel();

            Story dbStory = uow.StoryRepository.GetByID(id);
            if (dbStory != null)
            {
                model = new StoryViewModel(dbStory);
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(StoryViewModel viewModel)
        {
            if (viewModel == null)
            {
                TempData["Message"] = "No View Model";
                return RedirectToAction("Index");
            }

            if (!viewModel.HasImage)
            {
                TempData["Message"] = "Story must have a valid image";
                return RedirectToAction("Edit", viewModel.ID);
            }

            Story dbStory = uow.StoryRepository.GetByID(viewModel.ID);
            if (dbStory == null)
            {
                dbStory = new Story();
            }

            dbStory.Category = viewModel.Category;
            dbStory.Name = viewModel.Name;
            dbStory.Slug = viewModel.Slug;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength != 0)
                {
                    string ImgLink = Server.MapPath(Constants.ImagesDirectory);
                    string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Millisecond, file.FileName);
                    string savedFileName = Path.Combine(ImgLink, Path.GetFileName(uniqueFileName));
                    file.SaveAs(savedFileName);
                    dbStory.ImgLink = uniqueFileName;
                }
            }

            uow.StoryRepository.Save(dbStory);

            uow.Save();

            TempData["Message"] = "The story was saved successfully";
            return RedirectToAction("Index");

        }

        [AllowAnonymous]
        public ActionResult ViewStoryChapters(string Story)
        {

            Story story = uow.StoryRepository.GetBySlug(Story);

            List<Chapter> chapters = uow.ChapterRepository.GetChaptersByStoryId(story.ID);

            ChapterViewModel model = new ChapterViewModel(chapters);

            return View(model);
        }


    }
}