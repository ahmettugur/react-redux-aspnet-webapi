using OnlineStore.Entity.Concrete;
using System.Data.Entity.ModelConfiguration;

namespace OnlineStore.Data.EntityFramework.Mappings
{
    public class ProductMap:EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Products", "dbo");
            HasKey(_ => _.Id);

            Property(_ => _.Id).HasColumnName("Id");
            Property(_ => _.CategoryId).HasColumnName("CategoryId");
            Property(_ => _.Name).HasColumnName("Name");
            Property(_ => _.Details).HasColumnName("Details");
            Property(_ => _.Price).HasColumnName("Price");
            Property(_ => _.StockQuantity).HasColumnName("StockQuantity");
        }
    }
}
