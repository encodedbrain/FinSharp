using FinSharp.model;
using Microsoft.EntityFrameworkCore;

namespace FinSharp.data
{
    public class LocalDb : DbContext
    {
        public DbSet<UserProfile> Profiles { get; set; } = null!;
        public DbSet<UserData> UserDatas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                //replace appsettings.Development.json with appsettings.json
                .AddJsonFile("appsettings.Development.json")
                .Build();
            optionsBuilder.UseSqlServer(configRoot.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("Users");
            modelBuilder.Entity<UserData>().ToTable("UsersData");
        }
    }
}
