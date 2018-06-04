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

        public StoryRepository(StoryEntities context)
            : base(context)
        {
        }

        public Story GetBySlug(string slug)
        {

            Story story = Context.Stories.Where(s => s.Slug == slug).FirstOrDefault();

            return story;

        }

        public List<Story> GetStoryByCategory(List<int> categories)
        {

            List<Story> stories = new List<Story>();

            foreach (int id in categories)
            {
                List<Story> checkStory = Context.Stories.Where(p => p.Category == id).Select(p => p).ToList();
                if (checkStory != null)
                {
                    stories.AddRange(checkStory);
                }

            }

            return stories;
        }

        public override void Save(Story story)
        {

            if (story.ID == 0)
            {
                base.Create(story);
            }
            else
            {
                base.Update(story, item => item.ID == story.ID);
            }
        }

    }
}
