using System.Linq;
using NUnit.Framework;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Tests
{
    [TestFixture]
    public class CategoryDataTest
    {
        ICategoryRepository _catRepo;
        ICategoryProvider _catProvider;

        [SetUp]
        public void Init()
        {
            _catRepo = new CategoryRepository();
            _catProvider = new CategoryProvider(_catRepo);
        }

        [Test]
        [Category("CategoryDataTest")]
        public void TestMethod_CategoryTest()
        {
            TestMethod_GetCategories();
            TestMethod_AddCategory();
            TestMethod_EditCategory();
            TestMethod_DeleteCategory();
        }

        //[Test]
        //[Category("CategoryDataTest")]
        public void TestMethod_GetCategories()
        {
            var categories = _catProvider.GetCategories();
            Assert.IsNotNull(categories);
        }

        //[Test]
        //[Category("CategoryDataTest")]
        public void TestMethod_AddCategory()
        {
            var newCategory = new Category { CategoryName = "UnityTestCat_CategoryTest" };
            var response = _catProvider.AddCategory(newCategory);
            Assert.AreEqual(response, 1);

            var searchCount = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_CategoryTest").ToList().Count;
            Assert.IsTrue(searchCount > 0);
        }

        //[Test]
        //[Category("CategoryDataTest")]
        public void TestMethod_EditCategory()
        {
            var category = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_CategoryTest").FirstOrDefault();
            Assert.IsNotNull(category);

            category.CategoryName = "UnityTestCatModified_CategoryTest";
            var result = _catProvider.EditCategory(category);
            Assert.AreEqual(result, 1);

            var searchCount = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_CategoryTest").ToList().Count;
            Assert.IsTrue(searchCount == 0);

            var searchCountMod = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCatModified_CategoryTest").ToList().Count;
            Assert.IsTrue(searchCountMod > 0);
        }

        //[Test]
        //[Category("CategoryDataTest")]
        public void TestMethod_DeleteCategory()
        {
            var category = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCatModified_CategoryTest").FirstOrDefault();
            Assert.IsNotNull(category);

            var result = _catProvider.DeleteCategory(category);
            Assert.AreEqual(result, 1);

            var searchCountMod = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCatModified_CategoryTest").ToList().Count;
            Assert.IsTrue(searchCountMod == 0);
        }
    }
}
