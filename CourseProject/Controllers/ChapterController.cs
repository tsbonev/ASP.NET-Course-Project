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
{   [CustomAuthorize]
    public class ChapterController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewChapters(int id = 0)
        {
            ChapterRepository chapterRepository = new ChapterRepository();
            List<Chapter> chapters = chapterRepository.GetAll().Where(c => c.StoryID == id).OrderBy(c => c.ChapterNum).ToList();
            ChapterViewModel model = new ChapterViewModel(chapters);

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ViewChapter(string Chapter)
        {

            ChapterRepository chapterRepository = new ChapterRepository();
            StoryRepository storyRepository = new StoryRepository();

            string[] slugs = Chapter.Split('-');

            Story story = storyRepository.GetBySlug(slugs[0]);

            ChapterViewModel model = new ChapterViewModel(story.Chapters.Where(c => c.Slug == slugs[1]).FirstOrDefault());

            return View(model);

        }

        // GET: Chapter

        public ActionResult AddChapter(int storyID = 0)
        {
            StoryRepository storyRepository = new StoryRepository();
            SelectList list = new SelectList(storyRepository.GetAll(), "ID", "Name", storyID);
            ViewBag.AllStories = list;
            ChapterViewModel model = new ChapterViewModel()
            {
                StoryID = storyID
            };
            return View("EditChapter", model);
        }


        [HttpGet]
        public ActionResult EditChapter(int id = 0)
        {
            StoryRepository storyRepository = new StoryRepository();
            ChapterRepository chapterRepository = new ChapterRepository();
            ChapterViewModel model = new ChapterViewModel(chapterRepository.GetByID(id));
            SelectList list = new SelectList(storyRepository.GetAll(), "ID", "Name", id);
            ViewBag.AllStories = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditChapter(ChapterViewModel chapter)
        {
            ChapterRepository chapterRepository = new ChapterRepository();
            Chapter dbChapter = chapterRepository.GetByID(chapter.ID);
            if (dbChapter == null)
            {
                dbChapter = new Chapter();
            }

            dbChapter.ChapterName = chapter.ChapterName;
            dbChapter.ChapterNum = chapter.ChapterNum;
            dbChapter.Slug = chapter.Slug;
            dbChapter.Text = chapter.Text;
            dbChapter.DateCreated = DateTime.Now;
            dbChapter.StoryID = chapter.StoryID;

            chapterRepository.Save(dbChapter);

            TempData["Message"] = "The chapter was saved successfully";

            return RedirectToAction("EditStories", "Stories");
        }

        public ActionResult DeleteChapter(int id = 0)
        {
            ChapterRepository chapterRepository = new ChapterRepository();
            bool isDeleted = chapterRepository.DeleteByID(id);

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a chapter with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The chapter was deleted successfully";
            }

            return RedirectToAction("EditStories", "Stories");
        }

    }
}