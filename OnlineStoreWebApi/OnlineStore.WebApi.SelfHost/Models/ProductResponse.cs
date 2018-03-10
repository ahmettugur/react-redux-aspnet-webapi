using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.WebApi.SelfHost.Models
{
    public class ProductResponse
    {
        public int CurrentCategory { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public List<Product> Products { get; set; }
    }
}