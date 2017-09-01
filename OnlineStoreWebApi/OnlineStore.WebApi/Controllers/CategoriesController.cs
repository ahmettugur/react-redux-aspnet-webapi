using OnlineStore.Business.Contracts;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineStore.WebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("api/category")]
        [HttpGet]
        public IHttpActionResult CategoryList()
        {
            return Ok(_categoryService.GetAll().OrderByDescending(_ => _.Id));
        }

        [Route("api/category/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.Get(_=>_.Id == id);

            return Ok(category);
        }


        [Route("api/category")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Category category)
        {
            _categoryService.Add(category);

            return Ok(category);
        }

        [Route("api/category")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] Category category)
        {
            _categoryService.Update(category);

            return Ok(category);
        }


        [Route("api/category/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var category = new Category { Id = id };
                _categoryService.Delete(category);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
