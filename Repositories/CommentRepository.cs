using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(RestaurantsEntities context)
            : base(context)
        {
        }

        public override void Save(Comment comment)
        {
            if (comment.ID == 0)
            {
                base.Create(comment);
            }
            else
            {
                base.Update(comment, item => item.ID == comment.ID);
            }
        }
    }
}
