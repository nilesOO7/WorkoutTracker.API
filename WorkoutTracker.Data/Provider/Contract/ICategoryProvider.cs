using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Provider.Contract
{
    public interface ICategoryProvider
    {
        List<Category> GetCategories();
        Category GetCategory(int categoryId, bool preserveState);
        int AddCategory(Category inputCategory);
        int EditCategory(Category inputCategory);
        int DeleteCategory(Category inputCategory);
    }
}
