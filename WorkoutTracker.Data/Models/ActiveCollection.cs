using Newtonsoft.Json;
using System;

namespace WorkoutTracker.Data.Models
{
    public class ActiveCollection
    {
        [JsonProperty("hasActiveRecord")]
        public bool HasActiveRecord { get; set; }

        [JsonProperty("startTime")]
        public TimeSpan StartTime { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endTime")]
        public TimeSpan? EndTime { get; set; }

        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("workoutId")]
        public int WorkoutId { get; set; }

        [JsonProperty("workoutTitle")]
        public string WorkoutTitle { get; set; }

        [JsonProperty("workoutNote")]
        public string WorkoutNote { get; set; }

        [JsonProperty("caloriesBurntPerMin")]
        public float CaloriesBurntPerMin { get; set; }

        [JsonProperty("categoryId")]
        public int CollectionCategoryId { get; set; }
    }
}
