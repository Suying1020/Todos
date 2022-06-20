using Microsoft.EntityFrameworkCore;
using Todos.Models;

namespace Todos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoModel> ToDoModel { get; set; }
    }
}
