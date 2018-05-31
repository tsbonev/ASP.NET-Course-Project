using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.ComponentModel.DataAnnotations;

namespace UnitOfWorkSample.Models
{
    /// <summary>
    /// The ViewModel class for the Index.cshtml and Edit.cshtml
    /// Usually we have to use always 1 ViewModel class for 1 view, but the information for the both views quite similar
    /// </summary>
    public class UserViewModel
    {
        #region Properties
        public int ID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName { get; set; }

        public bool IsAdministrator { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Username { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password { get; set; }

        #endregion

        #region Constructors
        public UserViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public UserViewModel(User user)
        {
            this.ID = user.ID;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.IsAdministrator = user.IsAdministrator;
            this.Username = user.Username;
            this.Password = user.Password;
        }
        #endregion

        #region public methods
        public void UpdateDbModel(User dbUser)
        {
            // no need to update the ID: dbUser.ID = this.ID;
            dbUser.FirstName = this.FirstName;
            dbUser.LastName = this.LastName;
            dbUser.IsAdministrator = this.IsAdministrator;
            dbUser.Username = this.Username;
            dbUser.Password = this.Password;
        }
        #endregion
    }
}