using MVCCourseProject.Helpers;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCourseProject.Models
{

    public class StoriesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool HasImg { get; set; }
        public string Category { get; set; }
        public string ImgLink { get; set;}
        public string Slug { get; set; }

        public StoriesViewModel(Story story)
        {
            ID = story.ID;
            HasImg = (story.ImgLink != null);
            Category = story.StoryCategory.Name;
            Name = story.Name;
            Slug = story.Slug;
        }
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

                    StoriesViewModel item = new StoriesViewModel(story);
                    
                    item.ImgLink = imageFullPath.Replace(' ', '_');
                    Stories.Add(item);

                }
            }
        }

        public StoryViewModel(Story dbStory)
        {
            ID = dbStory.ID;
            Category = dbStory.Category;
            CategoryName = dbStory.StoryCategory.Name;
            Name = dbStory.Name;
            Slug = dbStory.Slug;
            ImgLink = dbStory.ImgLink;
            HasChapters = false;
            if (dbStory.Chapters.Count > 0)
            {
                HasChapters = true;
            }

            HasImage = string.IsNullOrEmpty(dbStory.ImgLink) == false;
            if (HasImage)
            {
                ImagePath = Path.Combine(Constants.ImagesDirectory, ImgLink);
            }
        }

        #endregion

    }
}