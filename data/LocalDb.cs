using FinSharp.model;
using Microsoft.EntityFrameworkCore;

namespace FinSharp.data
{
    public class LocalDb : DbContext
    {
        public LocalDb(DbContextOptions<LocalDb> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;


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

    }
}
