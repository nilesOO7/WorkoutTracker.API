using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Repository.Contract
{
    public interface IActiveRepository
    {
        List<Active> GetActiveRecords();
        Active GetActiveRecord(int activeRecordId, bool preserveState);
        int AddActiveRecord(Active inputActiveRecord);
        int EditActiveRecord(Active inputActiveRecord);
        int DeleteActiveRecord(Active inputActiveRecord);
    }
}
