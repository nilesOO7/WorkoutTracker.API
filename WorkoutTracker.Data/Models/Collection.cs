using Newtonsoft.Json;

namespace WorkoutTracker.Data.Models
{
    public class Collection
    {
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

        [JsonIgnore]
        public Category CollectionCategory { get; set; }

        [JsonIgnore]
        public virtual Active CollectionActiveData { get; set; }
    }
}
