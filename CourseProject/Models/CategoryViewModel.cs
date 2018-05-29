using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{

    public class CategoriesViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int lft { get; set; }
        public int rgt { get; set; }
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
            this.ID = category.ID;
            this.Name = category.Name;
            this.lft = category.lft;
            this.rgt = category.rgt;
        }

        public CategoryViewModel(List<Category> categories)
            : this()
        {
            foreach (Category category in categories)
            {
                CategoriesViewModel item = new CategoriesViewModel();
                item.ID = category.ID;
                item.Name = category.Name;
                item.lft = category.lft;
                item.rgt = category.rgt;
                Categories.Add(item);
            }
        }
    }
}