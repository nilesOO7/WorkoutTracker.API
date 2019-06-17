using System;
using System.Web.Http;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Service.Controllers
{
    public class CollectionController : ApiController
    {
        ICollectionProvider _collectionProvider;
        ICollectionRepository _collectionRepository;

        public CollectionController()
            : this(null, null) { }

        public CollectionController(ICollectionRepository colRepo, ICollectionProvider colProvider)
        {
            if (colRepo != null)
            {
                this._collectionRepository = colRepo;
            }
            else
            {
                this._collectionRepository = new CollectionRepository();
            }

            if (colProvider != null)
            {
                this._collectionProvider = colProvider;
            }
            else
            {
                this._collectionProvider = new CollectionProvider(this._collectionRepository);
            }
        }

        // GET api/Collection
        public IHttpActionResult Get()
        {
            try
            {
                var result = this._collectionProvider.GetCollections();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // GET api/Collection/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var currentCollection = this._collectionProvider.GetCollection(id, false);

                if (currentCollection != null)
                    return Ok(currentCollection);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // POST api/Collection
        public IHttpActionResult Post(Collection collectionModel)
        {
            try
            {
                var result = this._collectionProvider.AddCollection(collectionModel);

                if (result > 0)
                {
                    return Ok(result);
                }
                else return InternalServerError();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // PUT api/Collection/5
        public IHttpActionResult Put(Collection collectionModel)
        {
            try
            {
                var currentCategory = this._collectionProvider.GetCollection(collectionModel.WorkoutId, false);

                if (currentCategory != null)
                {
                    var result = this._collectionProvider.EditCollection(collectionModel);

                    if (result > 0)
                    {
                        return Ok(result);
                    }
                    else return InternalServerError();
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // DELETE api/Collection/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var currentCollection = this._collectionProvider.GetCollection(id, true);

                if (currentCollection != null)
                {
                    var result = this._collectionProvider.DeleteCollection(currentCollection);

                    if (result > 0)
                    {
                        return Ok(result);
                    }
                    else return InternalServerError();
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }
    }
}
