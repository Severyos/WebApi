using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models.Entities;

namespace WebApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderRowEntity> OrderRows { get; set; }

    }
}
