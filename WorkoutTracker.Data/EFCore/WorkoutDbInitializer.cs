using System;
using System.Collections.Generic;
using System.Data.Entity;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.EFCore
{
    //public class WorkoutDbInitializer : DropCreateDatabaseAlways<WorkoutDbContext>
    public class WorkoutDbInitializer : DropCreateDatabaseIfModelChanges<WorkoutDbContext>
    {
        protected override void Seed(WorkoutDbContext context)
        {
            /*
            workout_category
            */
            IList<Category> defaultCategories = new List<Category>();

            defaultCategories.Add(new Category { CategoryId = 1, CategoryName = "Sports" });
            defaultCategories.Add(new Category { CategoryId = 2, CategoryName = "Exercise" });
            defaultCategories.Add(new Category { CategoryId = 3, CategoryName = "Running" });
            defaultCategories.Add(new Category { CategoryId = 4, CategoryName = "Cycling" });

            context.Categories.AddRange(defaultCategories);

            /*
            workout_collection
            */
            IList<Collection> defaultCollections = new List<Collection>();

            defaultCollections.Add(new Collection { WorkoutId = 1, CollectionCategoryId = 1, WorkoutTitle = "Badminton", WorkoutNote = "Take racket", CaloriesBurntPerMin = 0.4f });
            defaultCollections.Add(new Collection { WorkoutId = 2, CollectionCategoryId = 1, WorkoutTitle = "Golf", WorkoutNote = "Take club", CaloriesBurntPerMin = 0.3f });
            defaultCollections.Add(new Collection { WorkoutId = 3, CollectionCategoryId = 2, WorkoutTitle = "Squats", WorkoutNote = "", CaloriesBurntPerMin = 0.9f });
            defaultCollections.Add(new Collection { WorkoutId = 4, CollectionCategoryId = 2, WorkoutTitle = "Jumping Rope", WorkoutNote = "", CaloriesBurntPerMin = 0.7f });
            defaultCollections.Add(new Collection { WorkoutId = 5, CollectionCategoryId = 2, WorkoutTitle = "Weights", WorkoutNote = "Take gym kit", CaloriesBurntPerMin = 0.5f });
            defaultCollections.Add(new Collection { WorkoutId = 6, CollectionCategoryId = 3, WorkoutTitle = "Trail Running", WorkoutNote = "", CaloriesBurntPerMin = 0.4f });
            defaultCollections.Add(new Collection { WorkoutId = 7, CollectionCategoryId = 3, WorkoutTitle = "Lap Run 500 Mtr", WorkoutNote = "", CaloriesBurntPerMin = 0.2f });
            defaultCollections.Add(new Collection { WorkoutId = 8, CollectionCategoryId = 4, WorkoutTitle = "Bicycling (5.5 mph)", WorkoutNote = "Stay hydrated", CaloriesBurntPerMin = 0.3f });

            context.Collections.AddRange(defaultCollections);

            /*
            workout_active
            */
            IList<Active> defaultActiveCollection = new List<Active>();

            DateTime start1 = new DateTime(2018, 04, 02, 19, 10, 05); DateTime end1 = new DateTime(2018, 04, 02, 21, 25, 45);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 1, Comment = "Get Set Go...", Status = false, StartDate = start1, StartTime = start1.TimeOfDay, EndDate = end1, EndTime = end1.TimeOfDay });
            DateTime start2 = new DateTime(2018, 04, 05, 07, 00, 00); DateTime end2 = new DateTime(2018, 04, 05, 09, 18, 23);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 2, Comment = "Hole in one", Status = false, StartDate = start2, StartTime = start2.TimeOfDay, EndDate = end2, EndTime = end2.TimeOfDay });
            DateTime start3 = new DateTime(2018, 04, 20, 06, 30, 00); DateTime end3 = new DateTime(2018, 04, 20, 07, 15, 50);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 3, Comment = "Exhausted!", Status = false, StartDate = start3, StartTime = start3.TimeOfDay, EndDate = end3, EndTime = end3.TimeOfDay });
            DateTime start4 = new DateTime(2018, 06, 27, 10, 15, 25); DateTime end4 = new DateTime(2018, 06, 27, 10, 45, 22);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 4, Comment = "Swing by swing", Status = false, StartDate = start4, StartTime = start4.TimeOfDay, EndDate = end4, EndTime = end4.TimeOfDay });
            DateTime start5 = new DateTime(2018, 09, 10, 09, 00, 00); DateTime end5 = new DateTime(2018, 09, 10, 10, 05, 13);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 5, Comment = "Too much", Status = false, StartDate = start5, StartTime = start5.TimeOfDay, EndDate = end5, EndTime = end5.TimeOfDay });
            DateTime start6 = new DateTime(2018, 09, 21, 07, 41, 21); DateTime end6 = new DateTime(2018, 09, 21, 10, 50, 00);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 6, Comment = "Need new shoes", Status = false, StartDate = start6, StartTime = start6.TimeOfDay, EndDate = end6, EndTime = end6.TimeOfDay });
            DateTime start7 = new DateTime(2018, 12, 11, 17, 20, 19); DateTime end7 = new DateTime(2018, 12, 11, 19, 26, 07);
            defaultActiveCollection.Add(new Active { ActiveWorkoutId = 7, Comment = "Lance Armstrong who?", Status = false, StartDate = start7, StartTime = start7.TimeOfDay, EndDate = end7, EndTime = end7.TimeOfDay });

            context.Actives.AddRange(defaultActiveCollection);
            base.Seed(context);
        }
    }
}
