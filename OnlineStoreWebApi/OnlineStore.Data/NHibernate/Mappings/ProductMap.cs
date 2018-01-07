using FluentNHibernate.Mapping;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.NHibernate.Mappings
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table(@"Products");
            LazyLoad();
            Id(_ => _.Id).Column("Id");
            Map(_ => _.CategoryId).Column("CategoryId");
            Map(_ => _.Name).Column("Name");
            Map(_ => _.Details).Column("Details");
            Map(_ => _.Price).Column("Price");
            Map(_ => _.StockQuantity).Column("StockQuantity");

        }
    }
}
