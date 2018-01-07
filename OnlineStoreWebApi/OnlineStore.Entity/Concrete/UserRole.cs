using OnlineStore.Core.Attributes;
using OnlineStore.Core.Contracts.Entities;

namespace OnlineStore.Entity.Concrete
{
    public class UserRole : IEntity
    {
        [PrimaryKey]
        public virtual int Id { get; set; }
        public virtual int RoleId { get; set; }
        public virtual int UserId { get; set; }
    }
}
