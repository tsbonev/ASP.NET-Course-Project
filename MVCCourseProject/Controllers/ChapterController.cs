using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using MVCCourseProject.Helpers;
using MVCCourseProject.Models;


namespace MVCCourseProject.Controllers
{
    [CustomAuthorize]
    public class ChapterController : Controller
    {

        UnitOfWork uow = new UnitOfWork();

        public ActionResult ViewChapters(int id = 0)
        {
            List<Chapter> chapters = uow.ChapterRepository.GetAll().Where(c => c.StoryID == id).OrderBy(c => c.ChapterNum).ToList();
            ChapterViewModel model = new ChapterViewModel(chapters);

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ViewChapter(string Chapter)
        {
            string[] slugs = Chapter.Split('-');

            Story story = uow.StoryRepository.GetBySlug(slugs[0]);

            ChapterViewModel model = new ChapterViewModel(story.Chapters.Where(c => c.Slug == slugs[1]).FirstOrDefault());

            if (LoginUserSession.Current.IsAuthenticated)
            {
                User user = uow.UserRepository.GetByID(LoginUserSession.Current.UserID);
                Chapter chapter = uow.ChapterRepository.GetByID(model.ID);
                model.LikesThis = user.Likes.Contains(chapter) ? true : false;
            }

            return View(model);

        }

        public ActionResult AddChapter(int storyID = 0)
        {
            SelectList list = new SelectList(uow.StoryRepository.GetAll(), "ID", "Name", storyID);
            ViewBag.AllStories = list;
            ChapterViewModel model = new ChapterViewModel()
            {
                StoryID = storyID
            };

            uow.Save();

            return View("EditChapter", model);
        }

        [HttpGet]
        public ActionResult EditChapter(int id = 0)
        {
            ChapterViewModel model = new ChapterViewModel(uow.ChapterRepository.GetByID(id));
            SelectList list = new SelectList(uow.StoryRepository.GetAll(), "ID", "Name", id);
            ViewBag.AllStories = list;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditChapter(ChapterViewModel chapter)
        {
            Chapter dbChapter = uow.ChapterRepository.GetByID(chapter.ID);
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

            uow.ChapterRepository.Save(dbChapter);

            uow.Save();

            TempData["Message"] = "The chapter was saved successfully";

            return RedirectToAction("EditStories", "Stories");
        }

        public ActionResult DeleteChapter(int id = 0)
        {

            Chapter chapter = uow.ChapterRepository.GetByID(id);

            if (null == chapter)
            {
                TempData["ErrorMessage"] = "Could not find a chapter with ID = " + id;
            }
            else
            {

                foreach(User user in chapter.Likes.ToList())
                {
                    chapter.Likes.Remove(user);
                }

                uow.ChapterRepository.DeleteByID(id);
                TempData["Message"] = "The chapter was deleted successfully";
            }

            uow.Save();

            return RedirectToAction("ViewChapters", "Chapter", new { id = chapter.StoryID });
        }

    }
}