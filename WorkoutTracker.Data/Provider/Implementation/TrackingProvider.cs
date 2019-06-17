using System;
using System.Globalization;
using System.Linq;
using WorkoutTracker.Data.Common;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Data.Provider.Implementation
{
    public class TrackingProvider : ITrackingProvider
    {
        private IActiveRepository _activeRepo;
        private ICollectionRepository _collectionRepo;

        public TrackingProvider(IActiveRepository actRepo, ICollectionRepository colRepo)
        {
            if (actRepo != null)
            {
                this._activeRepo = actRepo;
            }
            else
            {
                this._activeRepo = new ActiveRepository();
            }

            if (colRepo != null)
            {
                this._collectionRepo = colRepo;
            }
            else
            {
                this._collectionRepo = new CollectionRepository();
            }
        }

        public Track GetTrackingData(string relativeTimestamp)
        {
            Track trackingData = new Track();

            DateTime currentTimestamp;

            if (!string.IsNullOrWhiteSpace(relativeTimestamp))
            {
                if (!DateTime.TryParseExact(relativeTimestamp, Constants.RelativeTimestampPattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out currentTimestamp))
                {
                    currentTimestamp = DateTime.Now;
                }
            }
            else
            {
                currentTimestamp = DateTime.Now;
            }

            var workoutTimeDay =
                _activeRepo.GetActiveRecords()
                .Where(w => !w.Status && w.StartDate.Date == currentTimestamp.Date)
                .Select(s => ((TimeSpan)s.EndTime).Subtract(s.StartTime).TotalMinutes)
                .Sum();

            var workoutTimeWeek =
                _activeRepo.GetActiveRecords()
                .Where(w => !w.Status && w.StartDate.IsDateInBetween(currentTimestamp.FirstDayOfWeek(), currentTimestamp.LastDayOfWeek()))
                .Select(s => ((TimeSpan)s.EndTime).Subtract(s.StartTime).TotalMinutes)
                .Sum();

            var workoutTimeMonth =
                _activeRepo.GetActiveRecords()
                .Where(w => !w.Status && w.StartDate.IsDateInBetween(currentTimestamp.FirstDayOfMonth(), currentTimestamp.LastDayOfMonth()))
                .Select(s => ((TimeSpan)s.EndTime).Subtract(s.StartTime).TotalMinutes)
                .Sum();

            trackingData.WorkoutTimeOfDay = Convert.ToInt32(workoutTimeDay);
            trackingData.WorkoutTimeOfWeek = Convert.ToInt32(workoutTimeWeek);
            trackingData.WorkoutTimeOfMonth = Convert.ToInt32(workoutTimeMonth);

            var overallCaloriesBurnt =
                _activeRepo.GetActiveRecords()
                .Where(w => !w.Status)
                .GroupJoin(
                    _collectionRepo.GetCollections(),
                    act => act.ActiveWorkoutId,
                    col => col.WorkoutId,
                    (x, y) => new { Act = x, Col = y }
                )
                .SelectMany(
                    x => x.Col.DefaultIfEmpty(),
                    (x, y) => new { Act = x.Act, Col = y }
                )
                .Select(s => new
                {
                    Date = s.Act.StartDate,
                    Calories = s.Col.CaloriesBurntPerMin * ((TimeSpan)s.Act.EndTime).Subtract(s.Act.StartTime).TotalMinutes
                });

            var caloriesBurntInWeek =
                overallCaloriesBurnt
                .Where(w => w.Date.IsDateInBetween(currentTimestamp.FirstDayOfWeek(), currentTimestamp.LastDayOfWeek()))
                .Sum(s => s.Calories);

            var caloriesBurntInMonth =
                overallCaloriesBurnt
                .Where(w => w.Date.IsDateInBetween(currentTimestamp.FirstDayOfMonth(), currentTimestamp.LastDayOfMonth()))
                .Sum(s => s.Calories);

            var caloriesBurntInYear =
                overallCaloriesBurnt
                .Where(w => w.Date.Year == currentTimestamp.Year)
                .Sum(s => s.Calories);

            trackingData.WeekTotalCaloriesBurnt = Convert.ToInt32(caloriesBurntInWeek);
            trackingData.MonthTotalCaloriesBurnt = Convert.ToInt32(caloriesBurntInMonth);
            trackingData.YearTotalCaloriesBurnt = Convert.ToInt32(caloriesBurntInYear);

            var caloriesBurntByWeek =
                Constants.WeeklyDataGroup
                .GroupJoin(
                    overallCaloriesBurnt
                        .Where(w => w.Date.IsDateInBetween(currentTimestamp.FirstDayOfWeek(), currentTimestamp.LastDayOfWeek()))
                        .GroupBy(g => g.Date.DayOfWeek)
                        .Select(s => new ChartDataGroup { Key = s.Key.ToString(), Value = Convert.ToInt32(s.Sum(x => x.Calories)) }),
                    left => left.Key,
                    right => right.Key,
                    (x, y) => new { Left = x, Right = y }
                 )
                 .SelectMany(
                    x => x.Right.DefaultIfEmpty(),
                    (x, y) => new { Left = x.Left, Right = y }
                 )
                 .Select(s => new ChartDataGroup
                 {
                     Key = s.Left.Key,
                     Value = s.Right == null ? s.Left.Value : s.Right.Value
                 });

            var caloriesBurntByMonth =
                Constants.MonthlyDataGroup
                .GroupJoin(
                    overallCaloriesBurnt
                        .Where(w => w.Date.IsDateInBetween(currentTimestamp.FirstDayOfMonth(), currentTimestamp.LastDayOfMonth()))
                        .GroupBy(g => g.Date.GetWeekOfMonthPartial())
                        .Select(s => new ChartDataGroup { Key = s.Key.ToString(), Value = Convert.ToInt32(s.Sum(x => x.Calories)) }),
                    left => left.Key,
                    right => right.Key,
                    (x, y) => new { Left = x, Right = y }
                 )
                 .SelectMany(
                    x => x.Right.DefaultIfEmpty(),
                    (x, y) => new { Left = x.Left, Right = y }
                 )
                 .Select(s => new ChartDataGroup
                 {
                     Key = s.Left.Key,
                     Value = s.Right == null ? s.Left.Value : s.Right.Value
                 });

            var caloriesBurntByYear =
                Constants.YearlyDataGroup
                .GroupJoin(
                    overallCaloriesBurnt
                        .Where(w => w.Date.Year == currentTimestamp.Year)
                        .GroupBy(g => g.Date.ToMonthName())
                        .Select(s => new ChartDataGroup { Key = s.Key.ToString(), Value = Convert.ToInt32(s.Sum(x => x.Calories)) }),
                    left => left.Key,
                    right => right.Key,
                    (x, y) => new { Left = x, Right = y }
                 )
                 .SelectMany(
                    x => x.Right.DefaultIfEmpty(),
                    (x, y) => new { Left = x.Left, Right = y }
                 )
                 .Select(s => new ChartDataGroup
                 {
                     Key = s.Left.Key,
                     Value = s.Right == null ? s.Left.Value : s.Right.Value
                 });

            trackingData.DayWiseCaloryBurnData = caloriesBurntByWeek.ToList();
            trackingData.WeekWiseCaloryBurnData = caloriesBurntByMonth.ToList();
            trackingData.MonthWiseCaloryBurnData = caloriesBurntByYear.ToList();

            return trackingData;
        }
    }
}
