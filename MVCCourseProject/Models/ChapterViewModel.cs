using DataAccess;
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

        public ChaptersViewModel(Chapter chapter)
        {
            ID = chapter.ID;
            StoryID = chapter.StoryID;
            ChapterName = chapter.ChapterName;
            ChapterNum = chapter.ChapterNum;
            Slug = chapter.Slug;
            DateCreated = chapter.DateCreated;
            Story = chapter.Story;
            Likes = chapter.Likes.Count;
        }

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
            ChapterName = chapter.ChapterName;
            StoryID = chapter.StoryID;
            ID = chapter.ID;
            ChapterNum = chapter.ChapterNum;
            Slug = chapter.Slug;
            Text = chapter.Text;
            Story = chapter.Story;
            Likes = chapter.Likes.Count;
        }


        public ChapterViewModel(List<Chapter> chapterList) : this()
        {
            if (chapterList.Count > 0)
            {
                StoryID = chapterList[0].StoryID;
                foreach (Chapter chapter in chapterList)
                {
                    ChaptersViewModel temp = new ChaptersViewModel(chapter);
                    chapters.Add(temp);
                }
            }
        }
    }
}
