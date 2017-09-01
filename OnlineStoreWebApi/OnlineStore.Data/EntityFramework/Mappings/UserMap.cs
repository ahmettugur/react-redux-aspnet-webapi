using OnlineStore.Entity.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace OnlineStore.Data.EntityFramework.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users","dbo");
            HasKey(_=>_.UserId);

            Property(_=>_.UserId).HasColumnName("UserId");
            Property(_ => _.FullName).HasColumnName("FullName");
            Property(_=>_.Password).HasColumnName("Password");
            Property(_ => _.Email).HasColumnName("Email");
        }
    }
}
