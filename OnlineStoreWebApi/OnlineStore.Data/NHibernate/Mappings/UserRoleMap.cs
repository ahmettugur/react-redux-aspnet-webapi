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
    public class UserRoleMap: ClassMap<UserRole>
    {
        public UserRoleMap()
        {
            Table(@"UserRoles");
            LazyLoad();
            Id(_ => _.Id).Column("Id");
            Map(_=>_.RoleId).Column("RoleId");
            Map(_ => _.UserId).Column("UserId");
        }
    }
}
