using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;
using PawfectMatch.DataLayer;

namespace PawfectMatch.DatabaseContextManager
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
             
        }

        //for migrations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PawfectMatch;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}