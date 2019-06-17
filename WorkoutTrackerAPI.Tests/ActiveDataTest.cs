using System;
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
    public class ActiveDataTest
    {
        ICategoryRepository _catRepo;
        ICategoryProvider _catProvider;
        IActiveProvider _actProvider;

        ICollectionRepository _colRepo;
        ICollectionProvider _colProvider;
        IActiveRepository _actRepo;

        [SetUp]
        public void Init()
        {
            _catRepo = new CategoryRepository();
            _catProvider = new CategoryProvider(_catRepo);

            _colRepo = new CollectionRepository();
            _colProvider = new CollectionProvider(_colRepo);

            _actRepo = new ActiveRepository();
            _actProvider = new ActiveProvider(_actRepo);
        }

        [Test]
        [Category("ActiveDataTest")]
        public void Test_ActiveTest()
        {
            Test_GetActives();
            Test_AddActive_Success();
            Test_EditActive();
            Test_DeleteActive_Success();
        }

        //[Test]
        //[Category("ActiveDataTest")]
        public void Test_GetActives()
        {
            var actives = _actProvider.GetActiveRecords();
            Assert.IsNotNull(actives);
        }

        [Test]
        [Category("ActiveDataTest")]
        public void Test_AddActive_Failure()
        {
            // Arrange
            var newActive = new Active { ActiveWorkoutId = -1, Comment = "", Status = false, StartDate = DateTime.Now, StartTime = DateTime.Now.TimeOfDay, EndDate = null, EndTime = null };

            //Act
            ActualValueDelegate<object> testDelegate = () => _actProvider.AddActiveRecord(newActive);

            //Assert
            Assert.That(testDelegate, Throws.TypeOf<System.Data.Entity.Validation.DbEntityValidationException>());
        }

        //[Test]
        //[Category("ActiveDataTest")]
        public void Test_AddActive_Success()
        {
            var newCategory = new Category { CategoryName = "UnityTestCat_ActiveTest" };
            var response = _catProvider.AddCategory(newCategory);
            Assert.AreEqual(response, 1);

            var searchCat = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_ActiveTest").FirstOrDefault();
            Assert.IsNotNull(searchCat);

            var newCollection = new Collection { CollectionCategoryId = searchCat.CategoryId, WorkoutTitle = "UnitTest_ActiveTest", WorkoutNote = "" };
            var colResponse = _colProvider.AddCollection(newCollection);
            Assert.AreEqual(colResponse, 1);

            var searchCol = _colProvider.GetCollections().Where(w => w.WorkoutTitle == "UnitTest_ActiveTest").FirstOrDefault();
            Assert.IsNotNull(searchCol);

            var newActive = new Active { ActiveWorkoutId = searchCol.WorkoutId, Comment = "UnitTest_Comment_ActiveTest", Status = false, StartDate = DateTime.Now, StartTime = DateTime.Now.TimeOfDay, EndDate = null, EndTime = null };
            var actResponse = _actProvider.AddActiveRecord(newActive);
            Assert.IsNotNull(actResponse);
        }

        //[Test]
        //[Category("ActiveDataTest")]
        public void Test_EditActive()
        {
            var searchAct = _actProvider.GetActiveRecords().Where(w => w.Comment == "UnitTest_Comment_ActiveTest").FirstOrDefault();
            Assert.IsNotNull(searchAct);

            searchAct.EndDate = DateTime.Now;
            searchAct.EndTime = DateTime.Now.TimeOfDay;

            var response = _actProvider.EditActiveRecord(searchAct);
            Assert.AreEqual(response, 1);

            var searchModAct = _actProvider.GetActiveRecord(searchAct.ActiveWorkoutId, false);
            Assert.IsNotNull(searchModAct);

            Assert.IsNotNull(searchModAct.EndDate);
        }

        //[Test]
        //[Category("ActiveDataTest")]
        //[ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
        public void Test_DeleteActive_Failure()
        {
            var category = _catProvider.GetCategories().Where(w => w.CategoryName == "UnityTestCat_ActiveTest").FirstOrDefault();
            Assert.IsNotNull(category);

            var result = _catProvider.DeleteCategory(category);
            Assert.AreEqual(result, 1);
        }

        //[Test]
        //[Category("ActiveDataTest")]
        public void Test_DeleteActive_Success()
        {
            var searchAct = _actProvider.GetActiveRecords().Where(w => w.Comment == "UnitTest_Comment_ActiveTest").FirstOrDefault();
            Assert.IsNotNull(searchAct);

            var actResponse = _actProvider.DeleteActiveRecord(searchAct);
            Assert.AreEqual(actResponse, 1);

            var searchCol = _colProvider.GetCollections().Where(w => w.WorkoutId == searchAct.ActiveWorkoutId).FirstOrDefault();
            Assert.IsNotNull(searchCol);

            var colResponse = _colProvider.DeleteCollection(searchCol);
            Assert.AreEqual(colResponse, 1);

            var category = _catProvider.GetCategories().Where(w => w.CategoryId == searchCol.CollectionCategoryId).FirstOrDefault();
            Assert.IsNotNull(category);

            var result = _catProvider.DeleteCategory(category);
            Assert.AreEqual(result, 1);
        }
    }
}
