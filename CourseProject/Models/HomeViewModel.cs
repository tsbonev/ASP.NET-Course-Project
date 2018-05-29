using CourseProject.Helpers;
using DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{

    public class HomeStoriesViewModel
    {
        public int ID { get; set; }
        public string ImgLink { get; set; }
        public string Slug { get; set; }
    }

    public class HomeCategoriesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class HomeViewModel
    {

        #region Properties
        public List<HomeStoriesViewModel> StoriesList { get; set; }
        public List<HomeCategoriesViewModel> CategoriesList { get; set; }
        public Chapter CurrentChapter { get; set; }
        #endregion

        #region Constructor

        public HomeViewModel()
        {
            StoriesList = new List<HomeStoriesViewModel>();
            CategoriesList = new List<HomeCategoriesViewModel>();
        }

        public HomeViewModel(Chapter chapter)
            :this()
        {
            CurrentChapter = chapter;
        }

        public HomeViewModel(List<Story> allStories)
            : this()
        {
            foreach (Story story in allStories)
            {
                if(string.IsNullOrEmpty(story.ImgLink) == false)
                {
                    string imageFullPath = Path.Combine(Constants.ImagesDirectory, story.ImgLink);

                    HomeStoriesViewModel item = new HomeStoriesViewModel();
                    item.ID = story.ID;
                    item.ImgLink = imageFullPath;
                    item.Slug = story.Slug;
                    StoriesList.Add(item);

                }
            }
        }

        public HomeViewModel(List<Category> allCategories)
            : this()
        {
            foreach (Category category in allCategories)
            {
                    HomeCategoriesViewModel item = new HomeCategoriesViewModel();
                    item.ID = category.ID;
                    item.Name = category.Name;
                    CategoriesList.Add(item);
            }
        }

        #endregion

    }
}