using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Provider.Contract
{
    public interface IActiveProvider
    {
        List<Active> GetActiveRecords();
        Active GetActiveRecord(int activeRecordId, bool preserveState);
        int AddActiveRecord(Active inputActiveRecord);
        int EditActiveRecord(Active inputActiveRecord);
        int DeleteActiveRecord(Active inputActiveRecord);
    }
}
