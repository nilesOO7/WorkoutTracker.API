using System.Collections.Generic;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data.Common
{
    public class Constants
    {
        public const string RelativeTimestampPattern = "MM/dd/yyyy HH:mm:ss";

        public static List<ChartDataGroup> WeeklyDataGroup =
            new List<ChartDataGroup>
            {                
                new ChartDataGroup{Key = "Monday", Value = 0},
                new ChartDataGroup{Key = "Tuesday", Value = 0},
                new ChartDataGroup{Key = "Wednesday", Value = 0},
                new ChartDataGroup{Key = "Thursday", Value = 0},
                new ChartDataGroup{Key = "Friday", Value = 0},
                new ChartDataGroup{Key = "Saturday", Value = 0},
                new ChartDataGroup{Key = "Sunday", Value = 0}
            };

        public static List<ChartDataGroup> MonthlyDataGroup =
            new List<ChartDataGroup>
            {
                new ChartDataGroup{Key = "1", Value = 0},
                new ChartDataGroup{Key = "2", Value = 0},
                new ChartDataGroup{Key = "3", Value = 0},
                new ChartDataGroup{Key = "4", Value = 0},
                new ChartDataGroup{Key = "5", Value = 0}
            };

        public static List<ChartDataGroup> YearlyDataGroup =
            new List<ChartDataGroup>
            {
                new ChartDataGroup{Key = "January", Value = 0},
                new ChartDataGroup{Key = "February", Value = 0},
                new ChartDataGroup{Key = "March", Value = 0},
                new ChartDataGroup{Key = "April", Value = 0},
                new ChartDataGroup{Key = "May", Value = 0},
                new ChartDataGroup{Key = "June", Value = 0},
                new ChartDataGroup{Key = "July", Value = 0},
                new ChartDataGroup{Key = "August", Value = 0},
                new ChartDataGroup{Key = "September", Value = 0},
                new ChartDataGroup{Key = "October", Value = 0},
                new ChartDataGroup{Key = "November", Value = 0},
                new ChartDataGroup{Key = "December", Value = 0}
            };
    }
}
