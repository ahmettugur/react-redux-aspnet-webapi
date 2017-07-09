using OnlineStore.Core.Entities;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OnlineStore.Api.Controllers
{
    //[Authorize]
    public class CategoryController : BaseController
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService, IUnitOfWork _uow) : base(_uow)
        {
            _categoryService = categoryService;
        }

        [Route("api/category")]
        [HttpGet]
        public IHttpActionResult CategoryList()
        {
            return Ok(_categoryService.GetAll().OrderByDescending(_=>_.Id));
        }

        [Route("api/category/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.Get(id);

            return Ok(category);
        }


        [Route("api/category")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post([FromBody] Category category)
        {
            _categoryService.Add(category);
            _uow.SaveChanges();

            return Ok(category);
        }

        [Route("api/category")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put([FromBody] Category category)
        {
            _categoryService.Update(category);
            _uow.SaveChanges();

            return Ok(category);
        }


        [Route("api/category/{id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
                _uow.SaveChanges();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
