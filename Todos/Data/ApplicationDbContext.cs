using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todos.Models;

namespace Todos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoModel> ToDoModel { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
