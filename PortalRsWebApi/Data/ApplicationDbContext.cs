using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalRSApi.Data;
using System.Linq;

namespace PortalRSApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customers> Customers { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}