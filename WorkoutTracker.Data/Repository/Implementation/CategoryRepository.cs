using System.Collections.Generic;
using System.Linq;
using WorkoutTracker.Data.EFCore;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository.Contract;

namespace WorkoutTracker.Data.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private WorkoutDbContext _workoutDbContext;

        public CategoryRepository()
        {
            this._workoutDbContext = new WorkoutDbContext();
        }

        public List<Category> GetCategories()
        {
            return this._workoutDbContext.Categories.ToList();
        }

        public Category GetCategory(int categoryId, bool preserveState)
        {
            if (preserveState)
                return this._workoutDbContext.Categories.Where(w => w.CategoryId == categoryId).FirstOrDefault<Category>();
            else
                return this._workoutDbContext.Categories.AsNoTracking().Where(w => w.CategoryId == categoryId).FirstOrDefault<Category>();
        }

        public int AddCategory(Category inputCategory)
        {
            int rec = 0;
            this._workoutDbContext.Categories.Add(inputCategory);
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }

        public int EditCategory(Category inputCategory)
        {
            int rec = 0;
            this._workoutDbContext.Entry(inputCategory).State = System.Data.Entity.EntityState.Modified;
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }

        public int DeleteCategory(Category inputCategory)
        {
            int rec = 0;

            var collectionWithCat = this._workoutDbContext.Collections.AsNoTracking().Where(w => w.CollectionCategoryId == inputCategory.CategoryId).Select(s => s.WorkoutId).ToList();

            if (collectionWithCat != null && collectionWithCat.Count > 0)
            {
                this._workoutDbContext.Actives.Where(w => collectionWithCat.Contains(w.ActiveWorkoutId)).ToList().ForEach(f =>
                {
                    this._workoutDbContext.Actives.Remove(f);
                    rec = this._workoutDbContext.SaveChanges();
                });
            }

            this._workoutDbContext.Categories.Remove(inputCategory);
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }
    }
}
