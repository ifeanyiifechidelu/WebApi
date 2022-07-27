using FirstApiSQ011.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstApiSQ011.Data
{
    public class APIContext : IdentityDbContext<User>
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        public DbSet<Task> Taskone { get; set; }
        public DbSet<UserTask> UserTaskone { get; set; }
    }
}
