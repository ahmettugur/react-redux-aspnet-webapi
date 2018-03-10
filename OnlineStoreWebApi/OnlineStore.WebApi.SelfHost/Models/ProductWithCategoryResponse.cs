using OnlineStore.Entity.ComplexType;
using System.Collections.Generic;

namespace OnlineStore.WebApi.SelfHost.Models
{
    public class ProductWithCategoryResponse
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public List<ProductWithCategory> Products { get; set; }
    }
}