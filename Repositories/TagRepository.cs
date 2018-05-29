using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TagRepository : BaseRepository<Tag>
    {
        public override void Save(Tag tag)
        {
            if(tag.ID == 0)
            {
                base.Create(tag);
            }
            else
            {
                base.Update(tag, item => item.ID == tag.ID);
            }
        }
    }
}
