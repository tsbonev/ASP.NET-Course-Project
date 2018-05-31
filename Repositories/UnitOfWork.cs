using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork //: IDisposable
    {
        private RestaurantsEntities context = new RestaurantsEntities();

        private RestaurantRepository restaurantRepository;
        private CategoryRepository categoryRepository;
        private CommentRepository commentRepository;
        private UserRepository userRepository;

        public RestaurantRepository RestaurantRepository
        {
            get
            {
                if (this.restaurantRepository == null)
                {
                    this.restaurantRepository = new RestaurantRepository(context);
                }
                return restaurantRepository;
            }
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public CommentRepository CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new CommentRepository(context);
                }
                return commentRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
