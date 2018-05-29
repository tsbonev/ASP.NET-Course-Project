using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StoryRepository : BaseRepository<Story>
    {

        public override void Save(Story story)
        {
            if(story.ID == 0)
            {
                base.Create(story);
            }
            else
            {
                base.Update(story, item => item.ID == story.ID);
            }
        }

        public List<int> GetCategoryTree (string category)
        {
            Category parent = Context.Categories.Where(p => p.Name == category).Select(p => p).FirstOrDefault();

            List<int> categories = Context.Categories.Where(p => p.lft > parent.lft && p.lft < parent.rgt).Select(p => p.ID).ToList();

            categories.Add(parent.ID);

            return categories;
        }

        public List<string> GetAllCategories()
        {
            List<string> categories = Context.Categories.Select(p => p.Name).ToList();

            return categories;
        }

        public Story GetBySlug(string slug)
        {

            Story story = Context.Stories.Where(s => s.Slug == slug).FirstOrDefault();

            return story;

        }

        public List<Story> GetByCategory(List<int> categories)
        {

            CategoryRepository categoryRepository = new CategoryRepository();

            List<Story> stories = new List<Story>();

            foreach (int id in categories)
            {
                List<Story> checkStory = Context.Stories.Where(p => p.Category == id).Select(p => p).ToList();
                if(checkStory != null)
                {
                    stories.AddRange(checkStory);
                }
                
            }

            return stories;
        }




    }
}
