using OnlineStore.Core.Attributes;
using OnlineStore.Core.Contracts.Entities;

namespace OnlineStore.Entity.Concrete
{
    public class Role : IEntity
    {
        [PrimaryKey]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

}
