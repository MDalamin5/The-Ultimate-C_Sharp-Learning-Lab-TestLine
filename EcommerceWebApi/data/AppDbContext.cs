using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories {get; set;}
    }
}