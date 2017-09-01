using OnlineStore.Entity.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace OnlineStore.Data.EntityFramework.Mappings
{
    public class CategoryMap:EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Categories","dbo");
            HasKey(_=>_.Id);

            Property(_ => _.Id).HasColumnName("Id");
            Property(_ => _.Name).HasColumnName("Name");
            Property(_ => _.Description).HasColumnName("Description");
        }
    }
}
