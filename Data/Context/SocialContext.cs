using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Context
{
    public class SocialContext : DbContext
    {
        public SocialContext(DbContextOptions<SocialContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().ToTable("Comments");

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasIndex(u => new {u.Username, u.Email})
                .IsUnique(true);

            modelBuilder.Entity<Post>().ToTable("Posts");
        }
    }
}
