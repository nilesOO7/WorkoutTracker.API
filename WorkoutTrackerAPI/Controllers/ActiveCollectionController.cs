using System;
using System.Web.Http;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Service.Controllers
{
    public class ActiveCollectionController : ApiController
    {
        private IActiveRepository _activeRepo;
        private ICollectionRepository _colRepo;
        private IActiveCollectionProvider _actColProvider;

        public ActiveCollectionController()
            : this(null, null, null) { }

        public ActiveCollectionController(IActiveRepository activeRepo, ICollectionRepository colRepo, IActiveCollectionProvider actColProvider)
        {
            if (activeRepo != null)
            {
                this._activeRepo = activeRepo;
            }
            else
            {
                this._activeRepo = new ActiveRepository();
            }

            if (colRepo != null)
            {
                this._colRepo = colRepo;
            }
            else
            {
                this._colRepo = new CollectionRepository();
            }

            if (actColProvider != null)
            {
                this._actColProvider = actColProvider;
            }
            else
            {
                this._actColProvider = new ActiveCollectionProvider(this._activeRepo, this._colRepo);
            }
        }

        // GET api/ActiveCollection
        public IHttpActionResult Get()
        {
            try
            {
                var result = this._actColProvider.GetActiveCollections();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // GET api/ActiveCollection/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var currentActCol = this._actColProvider.GetActiveCollection(id);

                if (currentActCol != null)
                    return Ok(currentActCol);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }
    }
}
