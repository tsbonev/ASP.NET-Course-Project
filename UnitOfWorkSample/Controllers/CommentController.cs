
using DataAccess;
using UnitOfWorkSample.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitOfWorkSample.Helpers;

namespace UnitOfWorkSample.Controllers
{
    public class CommentController : Controller
    {
        // GET: Category
        [ChildActionOnly]
        public ActionResult CommentsForRestaurant(int restaurantID = 0)
        {
           
            // 1. get all categories from the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Comment> restaurantComments = unitOfWork.CommentRepository.GetAll()
                .Where(c => c.RestaurantID == restaurantID)
                .OrderByDescending(c => c.DateCreated)
                .ToList();

            // initialize the model for the View
            List<CommentViewModel> model = new List<CommentViewModel>();

            // 2. convert all Comment objects to ViewModel objects
            foreach (Comment comment in restaurantComments)
            {
                CommentViewModel newItem = new CommentViewModel(comment);
                model.Add(newItem);
            }

            ViewBag.RestaurantID = restaurantID;

            // 3. pass the viewModel to the view
            return PartialView("_CommentsForRestaurant", model);
        }

        [HttpPost]
        public EmptyResult AddComment(string text, int restaurantID = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            Comment dbComment = new Comment();
            dbComment.RestaurantID = restaurantID;
            dbComment.UserID = LoginUserSession.Current.UserID;
            dbComment.CommentText = text;
            dbComment.DateCreated = DateTime.Now;

            unitOfWork.CommentRepository.Save(dbComment);
            unitOfWork.Save();
            return new EmptyResult();
        }

        public EmptyResult Delete(int id = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.CommentRepository.DeleteByID(id);
            unitOfWork.Save();
            return new EmptyResult();
        }
    }
}