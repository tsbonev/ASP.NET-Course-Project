using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitOfWorkSample.Helpers
{
    /// <summary>
    /// Represents the main information for the logged user.
    /// We keep it in the session.
    /// It is very important to keep as little as possible information in this class !
    /// To get the instance in the current session, we have to use LoginUserSession.Current, which is always not null
    /// </summary>
    public class LoginUserSession
    {
        #region Properties

        /// <summary>
        /// The User ID in the Database; it will be used when we want to get data from the DB for the currently logged user
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// The username of the logged user. It is used when want to display it 
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// ! This is the most important property !
        /// A flag if the current user session is authenticated
        /// If the value is false, no other information from the class should not be retrieved
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// A flag if this user is administrator or not
        /// </summary>
        public bool IsAdministrator { get; private set; }
        #endregion

        #region Constructors
        private LoginUserSession()
        {
            // it is very important to have this to false, because we are going to use it in the code
            // actually the value is already false, but write it for better understanding
            // IsAuthenticated = false;    
        }
        #endregion

        #region Public properties

        /// <summary>
        /// get the instance of LoginUserSession in the current session
        /// If it does not exists in the session, then create a new object and set it in the session
        /// This is a variation of the Singleton design pattern
        /// </summary>
        public static LoginUserSession Current
        {
            get
            {
                // get the LoginUserSession object in the session and if it is null -> create new object and set it in the session
                LoginUserSession loginUserSession  = (LoginUserSession) HttpContext.Current.Session["LoginUser"];
                if (loginUserSession == null)
                {
                    loginUserSession = new LoginUserSession();
                    HttpContext.Current.Session["LoginUser"] = loginUserSession;
                }
                return loginUserSession;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Use this method when you log-in and want to set information for the user in the session
        /// </summary>
        public void SetCurrentUser(int userID, string username, bool isAdministrator)
        {
            this.IsAuthenticated = true;
            this.IsAdministrator = isAdministrator;
            this.UserID = userID;
            this.Username = username;
        }

        /// <summary>
        /// Use this method when you want to finish the current session 
        /// </summary>
        public void Logout()
        {
            // set the property values to default values
            // and the most important one is IsAuthenticated = false
            this.IsAuthenticated = false;
            this.IsAdministrator = false;
            this.UserID = 0;
            this.Username = string.Empty;
        }
        #endregion
    }
}