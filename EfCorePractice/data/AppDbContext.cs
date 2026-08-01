using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCorePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCorePractice.data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // table creation via db-set
        public DbSet<Book> Books {get; set;}
    }
}