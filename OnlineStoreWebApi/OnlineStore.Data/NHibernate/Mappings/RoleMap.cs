using FluentNHibernate.Mapping;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.NHibernate.Mappings
{
    public class RoleMap: ClassMap<Role>
    {
        public RoleMap()
        {
            Table(@"Roles");
            LazyLoad();

            Id(_ => _.Id);
            Map(_=>_.Id).Column("Id");
            Map(_ => _.Name).Column("Name");

        }
    }
}
