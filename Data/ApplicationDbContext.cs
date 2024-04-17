using RESTfulAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RESTfulAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
