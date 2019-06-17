using System.Collections.Generic;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Data.Provider.Implementation
{
    public class ActiveProvider : IActiveProvider
    {
        private IActiveRepository _activeRepo;

        public ActiveProvider(IActiveRepository actRepo)
        {
            if (actRepo != null)
            {
                this._activeRepo = actRepo;
            }
            else
            {
                this._activeRepo = new ActiveRepository();
            }
        }
        public List<Active> GetActiveRecords()
        {
            return this._activeRepo.GetActiveRecords();
        }
        public Active GetActiveRecord(int activeRecordId, bool preserveState)
        {
            return this._activeRepo.GetActiveRecord(activeRecordId, preserveState);
        }
        public int AddActiveRecord(Active inputActiveRecord)
        {
            return this._activeRepo.AddActiveRecord(inputActiveRecord);
        }
        public int EditActiveRecord(Active inputActiveRecord)
        {
            return this._activeRepo.EditActiveRecord(inputActiveRecord);
        }
        public int DeleteActiveRecord(Active inputActiveRecord)
        {
            return this._activeRepo.DeleteActiveRecord(inputActiveRecord);
        }
    }
}
