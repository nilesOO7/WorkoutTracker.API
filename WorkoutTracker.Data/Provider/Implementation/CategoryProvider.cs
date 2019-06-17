using System.Collections.Generic;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Data.Provider.Implementation
{
    public class CategoryProvider : ICategoryProvider
    {
        private ICategoryRepository _categoryRepo;

        public CategoryProvider(ICategoryRepository catRepo)
        {
            if (catRepo != null)
            {
                this._categoryRepo = catRepo;
            }
            else
            {
                this._categoryRepo = new CategoryRepository();
            }
        }

        public List<Category> GetCategories()
        {
            return this._categoryRepo.GetCategories();
        }

        public Category GetCategory(int categoryId, bool preserveState)
        {
            return this._categoryRepo.GetCategory(categoryId, preserveState);
        }

        public int AddCategory(Category inputCategory)
        {
            return this._categoryRepo.AddCategory(inputCategory);
        }

        public int EditCategory(Category inputCategory)
        {
            return this._categoryRepo.EditCategory(inputCategory);
        }

        public int DeleteCategory(Category inputCategory)
        {
            return this._categoryRepo.DeleteCategory(inputCategory);
        }
    }
}
