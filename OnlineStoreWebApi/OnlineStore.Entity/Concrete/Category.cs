using OnlineStore.Core.Contracts.Attributes;
using OnlineStore.Core.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Entity.Concrete
{
    public class Category : IEntity
    {
        [PrimaryKey]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
