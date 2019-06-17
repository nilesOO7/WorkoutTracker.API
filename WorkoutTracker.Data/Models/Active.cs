using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Data.Models
{
    [Table("workout_active")]
    public class Active
    {
        [JsonProperty("startTime")]
        [Column("start_time", TypeName = "time")]
        [Required]
        public TimeSpan StartTime { get; set; }

        [JsonProperty("startDate")]
        [Column("start_date", TypeName = "date")]
        [Required]
        public DateTime StartDate { get; set; }

        [JsonProperty("endTime")]
        [Column("end_time", TypeName = "time")]
        public TimeSpan? EndTime { get; set; }

        [JsonProperty("endDate")]
        [Column("end_date", TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("comment")]
        [Column("comment")]
        [MaxLength(64)]
        [Required]
        public string Comment { get; set; }

        [JsonProperty("status")]
        [Column("status", TypeName = "Bit")]
        [Required]
        public bool Status { get; set; }

        [JsonProperty("workoutId")]
        [Column("workout_id")]
        public int ActiveWorkoutId { get; set; }

        [JsonIgnore]
        public virtual Collection ActiveCollectionRecord { get; set; }
    }
}
