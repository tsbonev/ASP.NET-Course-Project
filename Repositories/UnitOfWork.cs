﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IDisposable
    {
        private StoryEntities context = new StoryEntities();

        public int Save()
        {
            return context.SaveChanges();
        }


        private StoryRepository storyRepository;
        private CategoryRepository categoryRepository;
        private ChapterRepository chapterRepository;
        private UserRepository userRepository;

        public StoryRepository StoryRepository
        {
            get
            {
                if (this.storyRepository == null)
                {
                    this.storyRepository = new StoryRepository(context);
                }
                return storyRepository;
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
        
        public ChapterRepository ChapterRepository
        {
            get
            {
                if (this.chapterRepository == null)
                {
                    this.chapterRepository = new ChapterRepository(context);
                }
                return chapterRepository;
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

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}