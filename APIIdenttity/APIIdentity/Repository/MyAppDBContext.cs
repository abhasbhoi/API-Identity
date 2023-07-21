using Microsoft.EntityFrameworkCore;
using APIIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using APIIdentity.Authentication;

namespace APIIdentity.Repository
{
    public class MyAppDBContext : IdentityDbContext<ApplicationUser>
    {
        public MyAppDBContext()
        {
            
        }

        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;

    }
}
