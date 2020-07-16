using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace RuHuaLibrary.Models
{
    public class RuHuaContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public  DbSet<Service> Service { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source = ruhua.database.windows.net; Initial Catalog = ruhua; User ID = ruhua; Password =!2Qwaszx");
            
        }
    }
}
