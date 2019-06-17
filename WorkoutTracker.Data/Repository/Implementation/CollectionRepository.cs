using System.Collections.Generic;
using System.Linq;
using WorkoutTracker.Data.EFCore;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository.Contract;

namespace WorkoutTracker.Data.Repository.Implementation
{
    public class CollectionRepository : ICollectionRepository
    {
        private WorkoutDbContext _workoutDbContext;

        public CollectionRepository()
        {
            this._workoutDbContext = new WorkoutDbContext();
        }

        public List<Collection> GetCollections()
        {
            return this._workoutDbContext.Collections.ToList();
        }

        public Collection GetCollection(int workoutId, bool preserveState)
        {
            if (preserveState)
                return this._workoutDbContext.Collections.Where(w => w.WorkoutId == workoutId).FirstOrDefault<Collection>();
            else
                return this._workoutDbContext.Collections.AsNoTracking().Where(w => w.WorkoutId == workoutId).FirstOrDefault<Collection>();
        }

        public int AddCollection(Collection inputCollection)
        {
            int rec = 0;
            this._workoutDbContext.Collections.Add(inputCollection);
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }

        public int EditCollection(Collection inputCollection)
        {
            int rec = 0;
            this._workoutDbContext.Entry(inputCollection).State = System.Data.Entity.EntityState.Modified;
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }

        public int DeleteCollection(Collection inputCollection)
        {
            int rec = 0;

            var activeForCollection = this._workoutDbContext.Actives.Where(w => w.ActiveWorkoutId == inputCollection.WorkoutId).FirstOrDefault();

            if (activeForCollection != null)
            {
                this._workoutDbContext.Actives.Remove(activeForCollection);
                rec = this._workoutDbContext.SaveChanges();
            }

            this._workoutDbContext.Collections.Remove(inputCollection);
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }
    }
}
