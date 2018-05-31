using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>
    {
        public RestaurantRepository(RestaurantsEntities context)
            : base(context)
        {
        }

        public override void Save(Restaurant restaurant)
        {
            if (restaurant.ID == 0)
            {
                base.Create(restaurant);
            }
            else
            {
                base.Update(restaurant, item => item.ID == restaurant.ID);
            }
        }
    }
}
