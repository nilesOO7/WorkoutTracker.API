using Newtonsoft.Json;
using System.Collections.Generic;

namespace WorkoutTracker.Data.Models
{
    public class Category
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonIgnore]
        public ICollection<Collection> CollectionsHavingThisCategory { get; set; }
    }
}
