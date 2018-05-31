using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(RestaurantsEntities context)
            : base(context)
        {
        }
        public override void Save(Category category)
        {
            if (category.ID == 0)
            {
                base.Create(category);
            }
            else
            {
                base.Update(category, item => item.ID == category.ID);
            }
        }
    }
}
