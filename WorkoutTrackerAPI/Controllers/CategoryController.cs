using System;
using System.Web.Http;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Provider.Contract;
using WorkoutTracker.Data.Provider.Implementation;
using WorkoutTracker.Data.Repository.Contract;
using WorkoutTracker.Data.Repository.Implementation;

namespace WorkoutTracker.Service.Controllers
{
    public class CategoryController : ApiController
    {
        ICategoryProvider _categoryProvider;
        ICategoryRepository _categoryRepository;

        public CategoryController() 
            : this(null, null) { }

        public CategoryController(ICategoryRepository catRepo, ICategoryProvider catProvider)
        {
            if (catRepo != null)
            {
                this._categoryRepository = catRepo;
            }
            else
            {
                this._categoryRepository = new CategoryRepository();
            }

            if (catProvider != null)
            {
                this._categoryProvider = catProvider;
            }
            else
            {
                this._categoryProvider = new CategoryProvider(this._categoryRepository);
            }
        }

        // GET api/Category
        public IHttpActionResult Get()
        {
            try
            {
                var result = this._categoryProvider.GetCategories();
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // GET api/Category/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var currentCategory = this._categoryProvider.GetCategory(id, false);

                if (currentCategory != null)
                    return Ok(currentCategory);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return InternalServerError();
            }
        }

        // POST api/Category
        public IHttpActionResult Post(Category categoryModel)
        {
            try
            {
                var result = this._categoryProvider.AddCategory(categoryModel);

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

        // PUT api/Category
        public IHttpActionResult Put(Category categoryModel)
        {
            try
            {
                var currentCategory = this._categoryProvider.GetCategory(categoryModel.CategoryId, false);

                if (currentCategory != null)
                {
                    var result = this._categoryProvider.EditCategory(categoryModel);

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

        // DELETE api/Category/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var currentCategory = this._categoryProvider.GetCategory(id, true);

                if (currentCategory != null)
                {
                    var result = this._categoryProvider.DeleteCategory(currentCategory);

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
