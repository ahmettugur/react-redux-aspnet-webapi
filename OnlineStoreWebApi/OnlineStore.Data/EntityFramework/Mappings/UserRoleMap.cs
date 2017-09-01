using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.EntityFramework.Mappings
{
    public class UserRoleMap:EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            ToTable("UserRoles", "dbo");
            HasKey(_=>_.Id);

            Property(_ => _.Id).HasColumnName("Id");
            Property(_=>_.RoleId).HasColumnName("RoleId");
            Property(_ => _.UserId).HasColumnName("UserId");

        }
    }
}
