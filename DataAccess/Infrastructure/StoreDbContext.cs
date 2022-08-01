using Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-AK0MLSE8; Database=BitirmeProjesi; integrated security=True; MultipleActiveResultSets=True;");
        }



        protected override void OnModelCreating(ModelBuilder x)
        {
            CompositeKeyRelation(x);
            OneToManyRelations(x);
            //OneToOneRelations(modelBuilder);
        }

            

        private void CompositeKeyRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketProduct>().HasKey(x => new { x.ProductId, x.BasketId });

            modelBuilder.Entity<BasketProduct>()
               .HasOne<Product>(x => x.Product)
               .WithMany(x => x.BasketProducts)
               .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<BasketProduct>()
                .HasOne<Basket>(x => x.Basket)
                .WithMany(x => x.BasketProducts)
                .HasForeignKey(x => x.BasketId);
        }

        private void OneToManyRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Sales)
                .WithOne(x => x.Customer);
        }

        //private void OneToOneRelations(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>()
        //        .HasOne<Basket>(x => x.Basket)
        //        .WithOne(x => x.Customer)
        //        .HasForeignKey<Basket>(x => x.BasketOfCustomerId);
        //}

        public DbSet<Basket>? Baskets { get; set; }

        public DbSet<BasketProduct>? BasketProducts { get; set; }

        public DbSet<Customer>? Customers { get; set; }

        public DbSet<Product>? Products { get; set; }

        public DbSet<Sale>? Sales { get; set; }
    }
}
