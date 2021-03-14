using APIApp.Models;
using Microsoft.EntityFrameworkCore;

namespace APIApp.Date
{
    public class AppContext : DbContext
    {
        public AppContext( DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}