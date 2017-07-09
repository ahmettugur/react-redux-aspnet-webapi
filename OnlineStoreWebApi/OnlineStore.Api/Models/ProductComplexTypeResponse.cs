using OnlineStore.Core.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Api.Models
{
    public class ProductComplexResponse
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public List<ProductComplex> Products { get; set; }
    }
}