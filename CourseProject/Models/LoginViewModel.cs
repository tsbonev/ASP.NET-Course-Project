using CourseProject.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }

        [Required]
        [CustomPassword]
        public string Password { get; set; }
    }
}