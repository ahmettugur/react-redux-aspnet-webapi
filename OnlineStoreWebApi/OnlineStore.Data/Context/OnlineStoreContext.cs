using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OnlineStore.Core.Entities;
using System.Data.Entity;

namespace OnlineStore.Data
{
    public class OnlineStoreContext : DbContext
    {
        public OnlineStoreContext()
            : base("OnlineStore")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
