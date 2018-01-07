using OnlineStore.Core.Contracts.Attributes;
using OnlineStore.Core.Contracts.Entities;

namespace OnlineStore.Entity.Concrete
{
    public class User : IEntity
    {
        [PrimaryKey]
        public virtual int UserId { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
    }
}
