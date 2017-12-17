using OnlineStore.Core.Attributes;
using OnlineStore.Core.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Entity.Concrete
{
    public class Category:IEntity 
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
