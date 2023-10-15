using FormModule.Models;
using FormModule.Models.Account;

using Microsoft.EntityFrameworkCore;

namespace FormModule.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) { }

        public DbSet<Employee> Employees { get; set; }
        
       public DbSet<User> Users { get; set; }
      


    }
}
