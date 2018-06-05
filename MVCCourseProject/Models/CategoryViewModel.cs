using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCourseProject.Models
{

    public class CategoriesViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int lft { get; set; }
        public int rgt { get; set; }

        public CategoriesViewModel(Category category)
        {
            ID = category.ID;
            Name = category.Name;
            lft = category.lft;
            rgt = category.rgt;
        }

    }

    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int lft { get; set; }
        public int rgt { get; set; }

        public List<CategoriesViewModel> Categories { get; set; }

        public CategoryViewModel()
        {
            Categories = new List<CategoriesViewModel>();
        }

        public CategoryViewModel(Category category)
        {
            ID = category.ID;
            Name = category.Name;
            lft = category.lft;
            rgt = category.rgt;
        }

        public CategoryViewModel(List<Category> categories)
            : this()
        {
            foreach (Category category in categories)
            {
                CategoriesViewModel item = new CategoriesViewModel(category);
                Categories.Add(item);
            }
        }
    }
}