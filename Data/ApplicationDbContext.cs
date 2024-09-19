using Microsoft.EntityFrameworkCore;
using ToDoTask.Web.Models.Enitities;

namespace ToDoTask.Web.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
           
        }
        public DbSet<ToDoIdentity> Tasks { get; set; }
    }
}
