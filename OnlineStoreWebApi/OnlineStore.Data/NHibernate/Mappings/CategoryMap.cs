using FluentNHibernate.Mapping;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.NHibernate.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Table(@"Categories");
            LazyLoad();
            Id(_ => _.Id).Column("Id");
            Map(_ => _.Name).Column("Name");
            Map(_ => _.Description).Column("Description");
        }
    }
}
