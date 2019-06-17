using System;
using System.Web.Http;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Service.Controllers
{
    /// <summary>
    /// Tracking Controller Class
    /// </summary>
    public class TrackingController : ApiController
    {
        private IActiveRepository _activeRepo;
        private ICollectionRepository _colRepo;
        private ITrackingProvider _trackingProvider;

        public TrackingController()
            : this(null, null, null) { }

        public TrackingController(IActiveRepository activeRepo, ICollectionRepository colRepo, ITrackingProvider trackingProvider)
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

            if (trackingProvider != null)
            {
                this._trackingProvider = trackingProvider;
            }
            else
            {
                this._trackingProvider = new TrackingProvider(this._activeRepo, this._colRepo);
            }
        }

        // POST api/Tracking
        /// <summary>
        /// Get Tracking data
        /// </summary>
        /// <param name="relativeTimestamp">"MM/dd/yyyy HH:mm:ss" or empty/null</param>
        /// <returns>Tracking data set</returns>
        public IHttpActionResult Post(Track request)
        {
            try
            {
                var result = this._trackingProvider.GetTrackingData(request.RelativeTimestamp);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }
    }
}
