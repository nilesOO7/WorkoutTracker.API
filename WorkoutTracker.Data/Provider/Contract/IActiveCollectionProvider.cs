using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Provider.Contract
{
    public interface IActiveCollectionProvider
    {
        List<ActiveCollection> GetActiveCollections();
        ActiveCollection GetActiveCollection(int activeCollectionId);
    }
}
