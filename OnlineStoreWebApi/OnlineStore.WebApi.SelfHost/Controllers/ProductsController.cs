using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OnlineStore.Business.Contracts;
using OnlineStore.Entity.ComplexType;
using OnlineStore.Entity.Concrete;
using OnlineStore.WebApi.SelfHost.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http;

namespace OnlineStore.WebApi.SelfHost.Controllers
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
        public IHttpActionResult ProductList(int categoryId = 0, int page = 1)
        {
            //return BadRequest("Hata Oluştu.");
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
            return Ok(productResponse);
        }

        [Route("api/products/detail/{productId?}")]
        [HttpGet]
        public IHttpActionResult ProductDetail(int productId)
        {
            if (productId == 0)
            {
                return BadRequest("ProductId can not be zero. ProductId: " + productId);
            }
            else
            {
                var product = _productService.Get(_ => _.Id == productId);
                if (product == null)
                {
                    return BadRequest("Product not found. ProductId: " + productId);
                }
                else
                {
                    return Ok(product);
                }
            }
        }

        [Route("api/admin/products/{page?}")]
        [HttpGet]
        public IHttpActionResult ProductComplexList(int page = 1)
        {
            int pageSize = 10;
            var productComplex = _productService.GetAllProductWithCategory().OrderByDescending(_ => _.ProductId).ToList();

            ProductWithCategoryResponse ProductComplexResponse = new ProductWithCategoryResponse
            {
                Products = productComplex.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(productComplex.Count / (double)pageSize),
                PageSize = pageSize,

            };

            return Ok(ProductComplexResponse);
        }

        [Route("api/admin/products")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]Product product)
        {
            _productService.Add(product);
            var uri = new Uri(Request.RequestUri + "/" + product.Id);
            return Created(uri, product);

        }

        [Route("api/admin/products")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]Product product)
        {
            if (product.Id == 0)
            {
                return BadRequest("ProductId can not be zero. ProductId: " + product.Id);
            }
            else
            {
                _productService.Update(product);

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("api/admin/products/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("ProductId can not be zero. ProductId: " + id);
            }
            else
            {
                Product product = new Product { Id = id };
                _productService.Delete(product);

                return StatusCode(HttpStatusCode.NoContent);

            }
        }

        [Route("api/admin/products/download")]
        [HttpGet]
        public HttpResponseMessage Download()
        {
            MediaTypeHeaderValue mediaType =
                   MediaTypeHeaderValue.Parse("application/octet-stream");
            string fileName = "Product List - " + DateTime.Now.ToShortDateString() + ".xlsx";
            byte[] excelFile = ExcelSheet(fileName, _productService.GetAllProductWithCategory());

            MemoryStream memoryStream = new MemoryStream(excelFile);
            HttpResponseMessage response =
                response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(memoryStream);
            response.Content.Headers.ContentType = mediaType;
            response.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("fileName") { FileName = fileName };
            return response;
        }

        public byte[] ExcelSheet(string fileName, List<ProductWithCategory> productList)
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(fileName.Replace(".xlsx", string.Empty));
                worksheet.Row(1).Height = 30;
                worksheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[1, 1].Value = "Product Id";
                //worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1].AutoFitColumns();


                worksheet.Cells[1, 2].Value = "Product Name";
                //worksheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 2].AutoFitColumns();

                worksheet.Cells[1, 3].Value = "Category";
                //worksheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 3].AutoFitColumns();

                worksheet.Cells[1, 4].Value = "Price";
                //worksheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 4].AutoFitColumns();

                worksheet.Cells[1, 5].Value = "Stock Quantity";
                //worksheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 5].AutoFitColumns();

                for (int i = 1; i <= 5; i++)
                {
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                    worksheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#448AFF"));
                    worksheet.Cells[1, i].Style.Font.Color.SetColor(Color.White);
                }

                for (int k = 0; k < productList.Count; k++)
                {
                    worksheet.Cells[k + 2, 1].Value = productList[k].ProductId;
                    worksheet.Cells[k + 2, 2].Value = productList[k].Name;
                    worksheet.Cells[k + 2, 3].Value = productList[k].CategoryName;
                    worksheet.Cells[k + 2, 4].Value = productList[k].Price;
                    worksheet.Cells[k + 2, 5].Value = productList[k].StockQuantity;
                    if (productList[k].StockQuantity <= 10)
                    {
                        worksheet.Cells[k + 2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[k + 2, 5].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#D50000"));
                        worksheet.Cells[k + 2, 5].Style.Font.Color.SetColor(Color.White);
                    }

                }
                byte[] bytes = package.GetAsByteArray();
                return bytes;
            }
        }


    }
}
