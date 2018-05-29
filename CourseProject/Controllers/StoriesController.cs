using CourseProject.Helpers;
using CourseProject.Models;
using DataAccess;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Controllers
{
    [CustomAuthorize]
    public class StoriesController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Stories
        //Make this a Get/Post combo so that you can get a query string and build a model in a form
        [AllowAnonymous]
        public ActionResult Stories(string Category = "Stories", string Story = "")
        {

            StoryRepository storyRepository = new StoryRepository();
            CategoryRepository categoryRepository = new CategoryRepository();

            int catNum;

            if (int.TryParse(Category, out catNum))
            {
                Category = categoryRepository.GetByID(catNum).Name;
            }

            StoryViewModel model = new StoryViewModel(storyRepository
                .GetByCategory(storyRepository.GetCategoryTree(Category))
                .Where(s => s.Name.Contains(Story)).ToList());

            List<Category> categories = categoryRepository.GetAll();

            model.CategorySelectList = new SelectList(categories, "Name", "Name", Category);

            //Requires a number to work
            ViewBag.AllCategories = new SelectList(categories, "ID", "Name", Category);

            return View(model);
        }
            
        [AllowAnonymous]
        public ActionResult ViewStoryChapters(string Story)
        {

            StoryRepository storyRepository = new StoryRepository();
            ChapterRepository chapterRepository = new ChapterRepository();

            Story story = storyRepository.GetBySlug(Story);

            List<Chapter> chapters = chapterRepository.GetChaptersByStoryId(story.ID);

            ChapterViewModel model = new ChapterViewModel(chapters);

            return View(model);
        }

        public ActionResult EditStories()
        {
            StoryRepository storyRepository = new StoryRepository();
            StoryViewModel stories = new StoryViewModel(storyRepository.GetAll());
            return View(stories);
        }

        public ActionResult Delete(int id = 0)
        {
            StoryRepository storyRepository = new StoryRepository();
            bool isDeleted = storyRepository.DeleteByID(id);

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a story with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The story was deleted successfully";
            }

            return RedirectToAction("Stories");

        }


        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            List<Category> allCategories = categoryRepository.GetAll();
            ViewBag.AllCategories = new SelectList(allCategories, "ID", "Name");

            TagRepository tagRepository = new TagRepository();
            List<Tag> allTags = tagRepository.GetAll();
            ViewBag.AllTags = new SelectList(allTags, "ID", "Name");

            StoryViewModel model = new StoryViewModel();
            StoryRepository storyRepository = new StoryRepository();
            Story dbStory = storyRepository.GetByID(id);
            if(dbStory != null)
            {
                model = new StoryViewModel(dbStory);
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(StoryViewModel viewModel)
        {
            if(viewModel == null)
            {
                TempData["Message"] = "No View Model";
                return RedirectToAction("Stories");
            }

            if (!viewModel.HasImage)
            {
                TempData["Message"] = "Story must have a valid image";
                return RedirectToAction("Edit", viewModel.ID);
            }
 
            StoryRepository storyRepository = new StoryRepository();
            Story dbStory = storyRepository.GetByID(viewModel.ID);
            if (dbStory == null)
            {
                dbStory = new Story();
            }

            dbStory.Category = viewModel.Category;
            dbStory.Name = viewModel.Name;
            dbStory.Slug = viewModel.Slug;

            if(Request.Files.Count > 0)
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

            storyRepository.Save(dbStory);

            TempData["Message"] = "The story was saved successfully";
            return RedirectToAction("Stories");

        }
    }
}