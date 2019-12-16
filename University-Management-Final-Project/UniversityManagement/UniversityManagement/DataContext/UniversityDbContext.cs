using System.Data.Entity;
using UniversityManagement.Models;

namespace UniversityManagement.DataContext
{
    public class UniversityDbContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
    }
}