using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Repository.Contract
{
    public interface ICollectionRepository
    {
        List<Collection> GetCollections();
        Collection GetCollection(int workoutId, bool preserveState);
        int AddCollection(Collection inputCollection);
        int EditCollection(Collection inputCollection);
        int DeleteCollection(Collection inputCollection);
    }
}
