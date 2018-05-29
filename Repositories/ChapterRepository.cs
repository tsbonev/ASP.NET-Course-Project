using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ChapterRepository : BaseRepository<Chapter>
    {

        public Chapter Current()
        {
            Chapter chapter = new Chapter();
            chapter = Context.Chapters.OrderByDescending(p => p.DateCreated).FirstOrDefault();

            return chapter;
        }

        public List<Chapter> GetChaptersByStoryId(int id)
        {

            return Context.Chapters.Where(c => c.Story.ID == id).ToList();

        }

        public override void Save(Chapter chapter)
        {
            if(chapter.ID == 0)
            {
                base.Create(chapter);
            }
            else
            {
                base.Update(chapter, item => item.ID == chapter.ID);
            }
        }

    }
}
