using System.Collections.Generic;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Data.Provider.Implementation
{
    public class CollectionProvider : ICollectionProvider
    {
        private ICollectionRepository _collectionRepo;

        public CollectionProvider(ICollectionRepository colRepo)
        {
            if (colRepo != null)
            {
                this._collectionRepo = colRepo;
            }
            else
            {
                this._collectionRepo = new CollectionRepository();
            }
        }

        public List<Collection> GetCollections()
        {
            return this._collectionRepo.GetCollections();
        }

        public Collection GetCollection(int workoutId, bool preserveState)
        {
            return this._collectionRepo.GetCollection(workoutId, preserveState);
        }

        public int AddCollection(Collection inputCollection)
        {
            return this._collectionRepo.AddCollection(inputCollection);
        }

        public int EditCollection(Collection inputCollection)
        {
            return this._collectionRepo.EditCollection(inputCollection);
        }

        public int DeleteCollection(Collection inputCollection)
        {
            return this._collectionRepo.DeleteCollection(inputCollection);
        }
    }
}
