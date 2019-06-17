using System;
using System.Linq;
using System.Collections.Generic;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Data.Provider.Implementation
{
    public class ActiveCollectionProvider : IActiveCollectionProvider
    {
        private IActiveRepository _activeRepo;
        private ICollectionRepository _collectionRepo;

        public ActiveCollectionProvider(IActiveRepository actRepo, ICollectionRepository colRepo)
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

        public List<ActiveCollection> GetActiveCollections()
        {
            var actCols =
                this._collectionRepo.GetCollections()
                .GroupJoin(
                    this._activeRepo.GetActiveRecords(),
                    Left => Left.WorkoutId,
                    Right => Right.ActiveWorkoutId,
                    (x, y) => new { Left = x, Right = y }
                )
                .SelectMany(
                    x => x.Right.DefaultIfEmpty(),
                    (x, y) => new { Left = x.Left, Right = y }
                )
                .Select(s => new ActiveCollection
                {
                    HasActiveRecord = s.Right != null,
                    WorkoutId = s.Left.WorkoutId,
                    WorkoutTitle = s.Left.WorkoutTitle,
                    WorkoutNote = s.Left.WorkoutNote,
                    CaloriesBurntPerMin = s.Left.CaloriesBurntPerMin,
                    CollectionCategoryId = s.Left.CollectionCategoryId,
                    StartTime = s.Right == null ? TimeSpan.Zero : s.Right.StartTime,
                    StartDate = s.Right == null ? DateTime.MinValue : s.Right.StartDate,
                    EndTime = s.Right == null ? TimeSpan.Zero : s.Right.EndTime,
                    EndDate = s.Right == null ? DateTime.MinValue : s.Right.EndDate,
                    Comment = s.Right == null ? string.Empty : s.Right.Comment,
                    Status = s.Right == null ? false : s.Right.Status
                });

            return actCols.ToList();
        }

        public ActiveCollection GetActiveCollection(int activeCollectionId)
        {
            var actCol =
                this._collectionRepo.GetCollections()
                .GroupJoin(
                    this._activeRepo.GetActiveRecords(),
                    Left => Left.WorkoutId,
                    Right => Right.ActiveWorkoutId,
                    (x, y) => new { Left = x, Right = y }
                )
                .SelectMany(
                    x => x.Right.DefaultIfEmpty(),
                    (x, y) => new { Left = x.Left, Right = y }
                )
                .Select(s => new ActiveCollection
                {
                    HasActiveRecord = s.Right != null,
                    WorkoutId = s.Left.WorkoutId,
                    WorkoutTitle = s.Left.WorkoutTitle,
                    WorkoutNote = s.Left.WorkoutNote,
                    CaloriesBurntPerMin = s.Left.CaloriesBurntPerMin,
                    CollectionCategoryId = s.Left.CollectionCategoryId,
                    StartTime = s.Right == null ? TimeSpan.Zero : s.Right.StartTime,
                    StartDate = s.Right == null ? DateTime.MinValue : s.Right.StartDate,
                    EndTime = s.Right == null ? TimeSpan.Zero : s.Right.EndTime,
                    EndDate = s.Right == null ? DateTime.MinValue : s.Right.EndDate,
                    Comment = s.Right == null ? string.Empty : s.Right.Comment,
                    Status = s.Right == null ? false : s.Right.Status
                })
                .Where(w => w.WorkoutId == activeCollectionId);

            return actCol.FirstOrDefault<ActiveCollection>();
        }
    }
}
