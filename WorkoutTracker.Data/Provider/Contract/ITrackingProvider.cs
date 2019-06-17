using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Provider.Contract
{
    public interface ITrackingProvider
    {
        Track GetTrackingData(string relativeTimestamp);
    }
}
