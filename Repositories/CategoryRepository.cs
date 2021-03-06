﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;


namespace Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {


        public CategoryRepository(StoryEntities context)
            : base(context)
        {
        }

        public List<int> GetCategoryTree(string category)
        {
            Category parent = Context.Categories.Where(p => p.Name == category).Select(p => p).FirstOrDefault();

            List<int> categories = Context.Categories.Where(p => p.lft > parent.lft && p.lft < parent.rgt).Select(p => p.ID).ToList();

            categories.Add(parent.ID);

            return categories;
        }

        public List<string> GetAllCategories()
        {
            List<string> categories = Context.Categories.Select(p => p.Name).ToList();

            return categories;
        }


        public override void Save(Category category)
        {
            category.rgt = category.lft + 1;
            base.Create(category);
        }

        private int getDifference(int? right, int insert, bool adding, out bool complexDelete)
        {

            if (adding)
            {
                complexDelete = false;
                return 2;
            }

            if (right - insert == 1)
            {
                complexDelete = false;
                return -2; //deleting children
            }
            else
            {
                complexDelete = true; //category has children
                return -1;
            }

        }

        public void doComplexDelete(Category category, int? right, int difference, int insert)
        {

            if (category.rgt > right)
            {
                category.rgt += difference * 2;
                if (category.lft > right)
                {
                    category.lft += difference * 2;
                }
                base.Update(category, c => c.ID == category.ID);
            }
            if (category.lft > insert && category.rgt < right)
            {
                category.lft += difference;
                category.rgt += difference;
                base.Update(category, c => c.ID == category.ID);
            }
        }

        private void doSimpleDelete(Category category, int? right, int difference, int insert)
        {

            if (category.rgt >= insert)
            {
                category.rgt += difference;
                if (category.lft >= insert)
                {
                    category.lft += difference;
                }
                base.Update(category, c => c.ID == category.ID);
            }

        }

        public void AdjustCategories(int insert, int? right, bool adding)
        {

            List<Category> categories = base.GetAll();

            bool complexDelete;

            int difference = getDifference(right, insert, adding, out complexDelete);

            foreach (Category category in categories)
            {

                if (complexDelete) doComplexDelete(category, right, difference, insert);
                else doSimpleDelete(category, right, difference, insert);

            }

        }

        public int GetByCategory(string category)
        {
            int id = Context.Categories.Where(j => j.Name == category)
                  .OrderByDescending(a => a.lft)
                  .Select(p => p.ID).FirstOrDefault();

            return id;
        }

    }
}
