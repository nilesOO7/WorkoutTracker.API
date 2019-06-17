using System.Linq;
using System.Collections.Generic;
using WorkoutTracker.Data.EFCore;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository.Contract;

namespace WorkoutTracker.Data.Repository.Implementation
{
    public class ActiveRepository : IActiveRepository
    {
        private WorkoutDbContext _workoutDbContext;

        public ActiveRepository()
        {
            this._workoutDbContext = new WorkoutDbContext();
        }
        public List<Active> GetActiveRecords()
        {
            return this._workoutDbContext.Actives.ToList();
        }

        public Active GetActiveRecord(int activeRecordId, bool preserveState)
        {
            if (preserveState)
                return this._workoutDbContext.Actives.Where(w => w.ActiveWorkoutId == activeRecordId).FirstOrDefault<Active>();
            else
                return this._workoutDbContext.Actives.AsNoTracking().Where(w => w.ActiveWorkoutId == activeRecordId).FirstOrDefault<Active>();
        }

        public int AddActiveRecord(Active inputActiveRecord)
        {
            int rec = 0;
            this._workoutDbContext.Actives.Add(inputActiveRecord);
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }

        public int EditActiveRecord(Active inputActiveRecord)
        {
            int rec = 0;
            this._workoutDbContext.Entry(inputActiveRecord).State = System.Data.Entity.EntityState.Modified;
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }

        public int DeleteActiveRecord(Active inputActiveRecord)
        {
            int rec = 0;
            this._workoutDbContext.Actives.Remove(inputActiveRecord);
            rec = this._workoutDbContext.SaveChanges();
            return rec;
        }
    }
}
