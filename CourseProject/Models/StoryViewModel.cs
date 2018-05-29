using CourseProject.Helpers;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseProject.Models
{

    public class StoriesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasImg { get; set; }
        public string Category { get; set; }
        public string ImgLink { get; set; }
        public string Slug { get; set; }
    }

    public class StoryViewModel
    {
        #region Properties
        public int ID { get; set; }
        public int Category { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public string ImgLink { get; set; }

        public List<StoriesViewModel> Stories { get; set; }
        public SelectList CategorySelectList { get; set; }

        public bool HasImage { get; set; }
        public bool HasChapters { get; set; }

        public string ImagePath { get; set; }
        #endregion

        #region Constructors

        public StoryViewModel()
        {
            Stories = new List<StoriesViewModel>();
        }

        public StoryViewModel(List<Story> stories)
            : this()
        {
            foreach (Story story in stories)
            {
                if (string.IsNullOrEmpty(story.ImgLink) == false)
                {
                    string imageFullPath = Path.Combine(Constants.ImagesDirectory, story.ImgLink);

                    StoriesViewModel item = new StoriesViewModel();
                    item.ID = story.ID;
                    item.HasImg = (story.ImgLink != null);
                    item.Category = story.Categories.Name;
                    item.Name = story.Name;
                    item.ImgLink = imageFullPath;
                    item.Slug = story.Slug;
                    Stories.Add(item);

                }
            }
        }

        public StoryViewModel(Story dbStory)
        {
            this.ID = dbStory.ID;
            this.Category = dbStory.Category;
            this.CategoryName = dbStory.Categories.Name;
            this.Name = dbStory.Name;
            this.Slug = dbStory.Slug;
            this.ImgLink = dbStory.ImgLink;
            this.HasChapters = false;
            if(dbStory.Chapters.Count > 0)
            {
                HasChapters = true;
            }

            this.HasImage = string.IsNullOrEmpty(dbStory.ImgLink) == false;
            if (this.HasImage)
            {
                this.ImagePath = Path.Combine(Constants.ImagesDirectory, this.ImgLink);
            }
        }

        #endregion

    }
}