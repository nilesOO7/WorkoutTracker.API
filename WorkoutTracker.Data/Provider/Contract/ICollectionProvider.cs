using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Provider.Contract
{
    public interface ICollectionProvider
    {
        List<Collection> GetCollections();
        Collection GetCollection(int workoutId, bool preserveState);
        int AddCollection(Collection inputCollection);
        int EditCollection(Collection inputCollection);
        int DeleteCollection(Collection inputCollection);
    }
}
