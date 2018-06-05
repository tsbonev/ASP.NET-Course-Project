using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace MVCCourseProject.Models
{

    public class UsersViewModel
    {

        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool IsAdmin { get; private set; }

        public UsersViewModel(User user)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsAdmin = user.Username == "admin";
        }

    }

    public class UserViewModel
    {

        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool IsAdmin { get; private set; }
        public List<Chapter> Likes;
        public List<UsersViewModel> Users { get; set; }

        public UserViewModel()
        {
            Likes = new List<Chapter>();
            Users = new List<UsersViewModel>();
        }

        public UserViewModel(User user)
            : this()
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsAdmin = user.Username == "admin";
            Likes.AddRange(user.Likes);
        }

        public UserViewModel(List<User> users)
            : this()
        {
            foreach (User user in users)
            {
                Users.Add(new UsersViewModel(user));
            }
        }

    }
}