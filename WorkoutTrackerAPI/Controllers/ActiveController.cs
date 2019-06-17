using System;
using System.Web.Http;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Service.Controllers
{
    public class ActiveController : ApiController
    {
        IActiveProvider _activeProvider;
        IActiveRepository _activeRepository;

        public ActiveController()
            : this(null, null) { }

        public ActiveController(IActiveRepository actRepo, IActiveProvider actProvider)
        {
            if (actRepo != null)
            {
                this._activeRepository = actRepo;
            }
            else
            {
                this._activeRepository = new ActiveRepository();
            }

            if (actProvider != null)
            {
                this._activeProvider = actProvider;
            }
            else
            {
                this._activeProvider = new ActiveProvider(this._activeRepository);
            }
        }

        // GET api/Active
        public IHttpActionResult Get()
        {
            try
            {
                var result = this._activeProvider.GetActiveRecords();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // GET api/Active/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var currentActiveRecord = this._activeProvider.GetActiveRecord(id, false);

                if (currentActiveRecord != null)
                    return Ok(currentActiveRecord);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // POST api/Active
        public IHttpActionResult Post(Active activeModel)
        {
            try
            {
                var result = this._activeProvider.AddActiveRecord(activeModel);

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

        // PUT api/Active/5
        public IHttpActionResult Put(Active activeModel)
        {
            try
            {
                var currentCategory = this._activeProvider.GetActiveRecord(activeModel.ActiveWorkoutId, false);

                if (currentCategory != null)
                {
                    var result = this._activeProvider.EditActiveRecord(activeModel);

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

        // DELETE api/Active/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var currentActiveRecord = this._activeProvider.GetActiveRecord(id, true);

                if (currentActiveRecord != null)
                {
                    var result = this._activeProvider.DeleteActiveRecord(currentActiveRecord);

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
