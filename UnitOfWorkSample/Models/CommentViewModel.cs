using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.ComponentModel.DataAnnotations;

namespace UnitOfWorkSample.Models
{
    public class CommentViewModel
    {
        #region Properties
        public int ID { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CommentText { get; set; }

        public DateTime DateCreated { get; set; }

        #endregion

        #region Constructors
        public CommentViewModel()
        {
        }
        public CommentViewModel(Comment comment)
        {
            this.ID = comment.ID;
            this.UserID = comment.UserID;
            this.UserName = string.Format("{0} {1}", comment.User.FirstName, comment.User.LastName);
            this.CommentText = comment.CommentText;
            this.DateCreated = comment.DateCreated;
        }
        #endregion
    }
}