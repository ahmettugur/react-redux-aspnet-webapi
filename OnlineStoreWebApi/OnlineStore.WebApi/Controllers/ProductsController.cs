using Newtonsoft.Json;
using OnlineStore.Business.Contracts;
using OnlineStore.Core.CrossCuttingConcerns.Security;
using OnlineStore.Entity.Concrete;
using OnlineStore.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace OnlineStore.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        //
        //1. Ninject.Web.WebApi
        //2. Ninject.Web.WebApi.WebHost
        //3. WebApiContrib.IoC.Ninject
        /// <summary>
        /// Bearer Token 
        /// Install-Package Microsoft.AspNet.WebApi.Owin
        /// Install-Package Microsoft.Owin.Security.OAuth
        /// Install-Package Microsoft.Owin.Cors
        /// Install-Package Microsoft.Owin.Host.SystemWeb
        /// </summary>

        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //public HttpResponseMessage Get()
        //{

        //    //var claims = ClaimsPrincipal.Current.Identities.First().Claims;
        //    //if (claims != null)
        //    //{
        //    //    var UserData = claims.FirstOrDefault(_ => _.Type == "UserData");
        //    //    if (UserData != null)
        //    //    {
        //    //        string jsonString = UserData.Value;
        //    //        var identity = JsonConvert.DeserializeObject<CustomIdentity<User>>(jsonString);
        //    //    }

        //    //}
        //    var products = _productService.GetAll();

        //    return Request.CreateResponse(HttpStatusCode.OK, products);
        //}

        [Route("api/products/{categoryId?}/{page?}")]
        [HttpGet]
        public HttpResponseMessage ProductList(int categoryId = 0, int page = 1)
        {
            int pageSize = 12;
            var products = (categoryId == 0 ? _productService.GetAll() : _productService.GetAll(_ => _.CategoryId == categoryId)).OrderByDescending(_ => _.Id).ToList();

            ProductResponse productResponse = new ProductResponse
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = categoryId,
                CurrentPage = page

            };
            return Request.CreateResponse(HttpStatusCode.OK, productResponse);
        }

        [Route("api/products/detail/{productId?}")]
        [HttpGet]
        public HttpResponseMessage ProductDetail(int productId)
        {
            try
            {
                if (productId == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ProductId can not be zero. ProductId: " + productId);
                }
                else
                {
                    var product = _productService.Get(_ => _.Id == productId);
                    if (product == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Product not found. ProductId: " + productId);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, product);
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/admin/products/{page?}")]
        [HttpGet]
        public HttpResponseMessage ProductComplexList(int page = 1)
        {

            int pageSize = 10;
            var productComplex = _productService.GetAllProductWithCategory().OrderByDescending(_ => _.ProductId).ToList();

            ProductWithCategoryResponse ProductComplexResponse = new ProductWithCategoryResponse
            {
                Products = productComplex.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(productComplex.Count / (double)pageSize),
                PageSize = pageSize,

            };

            return Request.CreateResponse(HttpStatusCode.OK, ProductComplexResponse);
        }

        [Route("api/admin/product")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Product product)
        {
            try
            {
                _productService.Add(product);

                return Request.CreateResponse(HttpStatusCode.Created, product);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/admin/product")]
        [HttpPut]
        public HttpResponseMessage Put([FromBody]Product product)
        {
            try
            {
                if (product.Id == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ProductId can not be zero. ProductId: " + product.Id);
                }
                else
                {
                    _productService.Update(product);
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/admin/product/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ProductId can not be zero. ProductId: " + id);
                }
                else
                {
                    Product product = new Product { Id = id };
                    _productService.Delete(product);

                    return Request.CreateResponse(HttpStatusCode.OK, "Your product was deletd succesfully.");

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
