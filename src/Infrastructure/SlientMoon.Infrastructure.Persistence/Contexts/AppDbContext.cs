using Microsoft.EntityFrameworkCore;
using SlientMoon.Domain.Entities;

namespace SlientMoon.Infrastructure.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserTopic> UserTopics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTopic>()
                .HasKey(x => new { x.UserId, x.TopicId });

            modelBuilder.Entity<UserTopic>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTopics)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = 1, Name = "Reduce Stress", ImageUrl = "stress.png" },
                new Topic { Id = 2, Name = "Improve Performance", ImageUrl = "performance.png" },
                new Topic { Id = 3, Name = "Reduce Anxiety", ImageUrl = "anxiety.png" },
                new Topic { Id = 4, Name = "Increase Happiness", ImageUrl = "happiness.png" },
                new Topic { Id = 5, Name = "Personal Growth", ImageUrl = "growth.png" },
                new Topic { Id = 6, Name = "Better Sleep", ImageUrl = "sleep.png" }
);

            base.OnModelCreating(modelBuilder);
        }
    }
}
