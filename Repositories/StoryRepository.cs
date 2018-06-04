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
