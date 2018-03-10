using OnlineStore.Business.Contracts;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineStore.WebApi.SelfHost.Controllers
{
    public class CategoriesController : ApiController
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("api/categories")]
        [HttpGet]
        public IHttpActionResult CategoryList()
        {
            return Ok(_categoryService.GetAll());
        }

        [Route("api/categories/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = _categoryService.Get(_ => _.Id == id);

            return Ok(category);
        }


        [Route("api/categories")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Category category)
        {
            _categoryService.Add(category);
            var uri = new Uri(Request.RequestUri + "/" + category.Id);
            return Created(uri, category);
        }

        [Route("api/categories")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] Category category)
        {
            _categoryService.Update(category);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [Route("api/categories/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var category = new Category { Id = id };
            _categoryService.Delete(category);

            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}
