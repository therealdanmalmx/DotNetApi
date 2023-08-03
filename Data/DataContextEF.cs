using DotNetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                        options => options.EnableRetryOnFailure());
            }
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);
            modelBuilder.Entity<UserJobInfo>()
                .ToTable("UserJobInfo", "TutorialAppSchema")
                .HasKey(u => u.UserId);
            modelBuilder.Entity<UserSalary>()
                .ToTable("UserSalary", "TutorialAppSchema")
                .HasKey(u => u.UserId);
        }
    }
}
