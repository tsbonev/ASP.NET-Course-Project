﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCourseProject.Models
{


    public class ChaptersViewModel
    {
        public string ChapterName { get; set; }
        public int StoryID { get; set; }
        public int ID { get; set; }
        public int ChapterNum { get; set; }
        public string Slug { get; set; }
        public DateTime DateCreated { get; set; }
        public Story Story { get; set; }
        public int Likes { get; set; }
    }

    public class ChapterViewModel
    {

        public List<ChaptersViewModel> chapters;

        [Required]
        public string ChapterName { get; set; }

        public int StoryID { get; set; }
        public Story Story { get; set; }

        public int ID { get; set; }

        public bool LikesThis { get; set; }

        [Required]
        public int ChapterNum { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public int Likes { get; set; }

        public ChapterViewModel()
        {
            chapters = new List<ChaptersViewModel>();
        }

        public ChapterViewModel(Chapter chapter)
        {
            this.ChapterName = chapter.ChapterName;
            this.StoryID = chapter.StoryID;
            this.ID = chapter.ID;
            this.ChapterNum = chapter.ChapterNum;
            this.Slug = chapter.Slug;
            this.Text = chapter.Text;
            this.Story = chapter.Story;
            this.Likes = chapter.Likes.Count;
        }


        public ChapterViewModel(List<Chapter> chapterList) : this()
        {
            if (chapterList.Count > 0)
            {
                StoryID = chapterList[0].StoryID;
                foreach (Chapter chapter in chapterList)
                {
                    ChaptersViewModel temp = new ChaptersViewModel();
                    temp.ID = chapter.ID;
                    temp.StoryID = chapter.StoryID;
                    temp.ChapterName = chapter.ChapterName;
                    temp.ChapterNum = chapter.ChapterNum;
                    temp.Slug = chapter.Slug;
                    temp.DateCreated = chapter.DateCreated;
                    temp.Story = chapter.Story;
                    temp.Likes = chapter.Likes.Count;
                    chapters.Add(temp);
                }
            }
        }
    }
}
