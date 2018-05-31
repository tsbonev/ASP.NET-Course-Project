using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(RestaurantsEntities context)
            : base(context)
        {
        }

        public override void Save(User user)
        {
            if (user.ID == 0)
            {
                base.Create(user);
            }
            else
            {
                base.Update(user, item => item.ID == user.ID);
            }
        }

        public User GetUserByNameAndPassword(string username, string password)
        {
            User user = base.DBSet.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public void RegisterUser(User user)
        {
            base.Create(user);
        }

        public User GetUserByName(string username)
        {
            User user = base.DBSet.FirstOrDefault(u => u.Username == username);
            return user;
        }
    }
}
