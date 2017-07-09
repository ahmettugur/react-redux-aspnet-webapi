using OnlineStore.Data.UnitOfWork;
using OnlineStore.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using OnlineStore.Api.Models;
using System.Web.Routing;
using OnlineStore.Core.Entities;
using System.Net;
using System.Net.Http;

namespace OnlineStore.Api.Controllers
{
    public class ProductController : BaseController
    {
        IProductService _productService;
        public ProductController(IProductService productService, IUnitOfWork uow) : base(uow)
        {
            _productService = productService;
        }

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
                    var product = _productService.Get(productId);
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
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage ProductComplexList(int page = 1)
        {

            int pageSize = 10;
            var productComplex = _productService.GetAllProductWithCategory().OrderByDescending(_ => _.ProductId).ToList();

            ProductComplexResponse ProductComplexResponse = new ProductComplexResponse
            {
                Products = productComplex.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(productComplex.Count / (double)pageSize),
                PageSize = pageSize,

            };

            return Request.CreateResponse(HttpStatusCode.OK, ProductComplexResponse);
        }

        [Route("api/admin/product")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Post([FromBody]Product product)
        {
            try
            {
                _productService.Add(product);
                if (_uow.SaveChanges() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, product);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "An error has occurred");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/admin/product")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
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
                    if (_uow.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, product);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error has occurred.");
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/admin/product/{id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
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
                    _productService.Delete(id);
                    if (_uow.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Your product was deletd succesfully.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error has occurred.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}