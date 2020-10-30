using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UserTasksService.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration config;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)
            : base(options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("ApplicationDB"));
        }

        public DbSet<UserTask> UserTasks { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
