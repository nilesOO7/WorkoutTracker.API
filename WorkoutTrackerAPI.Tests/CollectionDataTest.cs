using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Tests
{
    [TestFixture]
    public class CollectionDataTest
    {
        ICategoryRepository _catRepo;
        ICategoryProvider _catProvider;

        ICollectionRepository _colRepo;
        ICollectionProvider _colProvider;

        [SetUp]
        public void Init()
        {
            _catRepo = new CategoryRepository();
            _catProvider = new CategoryProvider(_catRepo);

            _colRepo = new CollectionRepository();
            _colProvider = new CollectionProvider(_colRepo);
        }

        [Test]
        [Category("CollectionDataTest")]
        public void TestMethod_CollectionTest()
        {
            TestMethod_GetCollections();
            TestMethod_AddCollection_Success();
            TestMethod_EditCollection();
            TestMethod_DeleteCollection();
        }

        //[Test]
        //[Category("CollectionDataTest")]
        public void TestMethod_GetCollections()
        {
            var collections = _colProvider.GetCollections();
            Assert.IsNotNull(collections);
        }

        [Test]
        [Category("CollectionDataTest")]
        public void TestMethod_AddCollection_Failure()
        {
            // Arrange
            var newCollection = new Collection { CollectionCategoryId = -1, WorkoutTitle = "UnitTest_CollectionTest", WorkoutNote = "" };
            
            //Act
            ActualValueDelegate<object> testDelegate = () => _colProvider.AddCollection(newCollection);

            //Assert
            Assert.That(testDelegate, Throws.TypeOf<System.Data.Entity.Infrastructure.DbUpdateException>());
        }

        //[Test]
        //[Category("CollectionDataTest")]
        public void TestMethod_AddCollection_Success()
        {
            var newCategory = new Category { CategoryName = "UnityTestCat_CollectionTest" };
            var response = _catProvider.AddCategory(newCategory);
            Assert.AreEqual(response, 1);

            var searchCat = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_CollectionTest").FirstOrDefault();
            Assert.IsNotNull(searchCat);

            var newCollection = new Collection { CollectionCategoryId = searchCat.CategoryId, WorkoutTitle = "UnitTest_CollectionTest", WorkoutNote = "" };
            var colResponse = _colProvider.AddCollection(newCollection);
            Assert.AreEqual(colResponse, 1);
        }

        //[Test]
        //[Category("CollectionDataTest")]
        public void TestMethod_EditCollection()
        {
            var searchCol = _colProvider.GetCollections().Where(w => w.WorkoutTitle == "UnitTest_CollectionTest").FirstOrDefault();
            Assert.IsNotNull(searchCol);

            searchCol.WorkoutNote = "UnitTestNote_CollectionTest";
            var response = _colProvider.EditCollection(searchCol);
            Assert.AreEqual(response, 1);

            var searchModCol = _colProvider.GetCollection(searchCol.WorkoutId, false);
            Assert.IsNotNull(searchModCol);

            Assert.AreEqual(searchModCol.WorkoutNote, "UnitTestNote_CollectionTest");
        }

        //[Test]
        //[Category("CollectionDataTest")]
        public void TestMethod_DeleteCollection()
        {
            var searchCol = _colProvider.GetCollections().Where(w => w.WorkoutTitle == "UnitTest_CollectionTest").FirstOrDefault();
            Assert.IsNotNull(searchCol);

            var response = _colProvider.DeleteCollection(searchCol);
            Assert.AreEqual(response, 1);

            var category = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_CollectionTest").FirstOrDefault();
            Assert.IsNotNull(category);

            var result = _catProvider.DeleteCategory(category);
            Assert.AreEqual(result, 1);
        }
    }
}
