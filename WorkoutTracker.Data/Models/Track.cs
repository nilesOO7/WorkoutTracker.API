using Newtonsoft.Json;
using System.Collections.Generic;

namespace WorkoutTracker.Data.Models
{
    public class Track
    {
        [JsonProperty("workoutTimeOfDay")]
        public int WorkoutTimeOfDay { get; set; }

        [JsonProperty("workoutTimeOfWeek")]
        public int WorkoutTimeOfWeek { get; set; }

        [JsonProperty("workoutTimeOfMonth")]
        public int WorkoutTimeOfMonth { get; set; }


        [JsonProperty("weekTotalCaloriesBurnt")]
        public int WeekTotalCaloriesBurnt { get; set; }

        [JsonProperty("monthTotalCaloriesBurnt")]
        public int MonthTotalCaloriesBurnt { get; set; }

        [JsonProperty("yearTotalCaloriesBurnt")]
        public int YearTotalCaloriesBurnt { get; set; }


        [JsonProperty("dayWiseCaloryBurnData")]
        public List<ChartDataGroup> DayWiseCaloryBurnData { get; set; }

        [JsonProperty("weekWiseCaloryBurnData")]
        public List<ChartDataGroup> WeekWiseCaloryBurnData { get; set; }

        [JsonProperty("monthWiseCaloryBurnData")]
        public List<ChartDataGroup> MonthWiseCaloryBurnData { get; set; }


        [JsonProperty("relativeTimestamp", NullValueHandling = NullValueHandling.Ignore)]
        public string RelativeTimestamp { get; set; }
    }

    public class ChartDataGroup
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        public override string ToString()
        {
            return "Key=" + this.Key + "; Value=" + this.Value;
        }
    }
}
