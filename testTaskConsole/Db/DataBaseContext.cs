using Microsoft.EntityFrameworkCore;
using testTaskConsole.Models;

namespace testTaskConsole;

public class DataBaseContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public DataBaseContext (DbContextOptions<DataBaseContext> options)
        :base (options)
    {
        
    }
}