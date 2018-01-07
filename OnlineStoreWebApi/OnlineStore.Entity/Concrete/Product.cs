using OnlineStore.Core.Attributes;
using OnlineStore.Core.Contracts.Entities;


namespace OnlineStore.Entity.Concrete
{
    public class Product : IEntity
    {
        [PrimaryKey]
        public virtual int Id { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Details { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int StockQuantity { get; set; }
    }
}
