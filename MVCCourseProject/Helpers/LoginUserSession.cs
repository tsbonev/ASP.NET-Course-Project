using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using Repositories;

namespace MVCCourseProject.Helpers
{
    public class LoginUserSession
    {

        public int UserID { get; set; }
        public string Username { get; set; }
        public User UserDB { get; set; }

        public bool IsAuthenticated { get; set; }
        public bool IsAdministrator { get; set; }

        private LoginUserSession()
        {
            IsAuthenticated = false;
        }

        public static LoginUserSession Current
        {
            get
            {
                LoginUserSession loginUserSession = (LoginUserSession)HttpContext.Current.Session["LoginUser"];
                if (loginUserSession == null)
                {
                    loginUserSession = new LoginUserSession();
                    HttpContext.Current.Session["LoginUser"] = loginUserSession;
                }
                return loginUserSession;
            }
        }

        public void SetCurrentUser(int userID, string username, bool adminCheck)
        {
            this.IsAuthenticated = true;
            this.IsAdministrator = adminCheck;
            this.UserID = userID;
            this.Username = username;
            this.UserDB = new UnitOfWork().UserRepository.GetByID(UserID);
        }

        public void Logout()
        {
            this.IsAuthenticated = false;
            this.IsAdministrator = false;
            this.UserID = -1;
            this.Username = string.Empty;
            this.UserDB = null;
        }


    }
}