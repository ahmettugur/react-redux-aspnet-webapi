using FluentNHibernate.Mapping;
using OnlineStore.Entity.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace OnlineStore.Data.NHibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {

            Table(@"Users");
            LazyLoad();
            Id(_ => _.UserId).Column("UserId");
            Map(_ => _.FullName).Column("FullName");
            Map(_=>_.Password).Column("Password");
            Map(_ => _.Email).Column("Email");
        }
    }
}
