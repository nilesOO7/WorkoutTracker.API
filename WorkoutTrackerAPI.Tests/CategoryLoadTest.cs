using System;
using System.Collections.Generic;
using System.Linq;
using NBench;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Tests
{
    public class CategoryLoadTest
    {
        private Counter _opCounter;

        ICategoryRepository _catRepo;
        ICategoryProvider _catProvider;

        List<string> _catToAdd;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _opCounter = context.GetCounter("CatCounter");

            _catRepo = new CategoryRepository();
            _catProvider = new CategoryProvider(_catRepo);
            _catToAdd = new List<string>();
        }

        [PerfBenchmark(
            Description = "Test to ensure that a minimal throughput test can be rapidly executed.",
            NumberOfIterations = 25,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000,
            TestMode = TestMode.Measurement)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        [CounterMeasurement("CatCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void Benchmark()
        {
            var newCategory = new Category { CategoryName = "UnitTestCategory_" + DateTime.Now.Ticks };

            if (!_catToAdd.Contains(newCategory.CategoryName))
            {
                var response = _catProvider.AddCategory(newCategory);
                _opCounter.Increment();

                _catToAdd.Add(newCategory.CategoryName);
            }
        }

        [PerfCleanup]
        public void Cleanup()
        {
            foreach (string cat in _catToAdd)
            {
                var category = _catProvider.GetCategories().Where(w => w.CategoryName == cat).FirstOrDefault();
                var result = _catProvider.DeleteCategory(category);
            }
        }
    }
}
