using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCourseProject.Models
{
    public class SearchViewModel
    {

        public string Story { get; set; }
        public string Category { get; set; }
        public StoryViewModel Stories { get; set; }
        public List<CategoriesViewModel> Categories { get; set; }

        public SearchViewModel(string Story, string Category)
        {
            this.Story = Story;
            this.Category = Category;
        }

        public SearchViewModel() { }

    }
}