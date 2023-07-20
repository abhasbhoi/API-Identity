using Microsoft.EntityFrameworkCore;
using APIIdentity.Models;

namespace APIIdentity.Repository
{
    public class MyAppDBContext : DbContext
    {
        public MyAppDBContext()
        {
            
        }

        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;

    }
}
