using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.EntityFrameworkCore;
using Weather.Model;
using static Weather.Model.WeatherResponse;

namespace Weather.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<WeatherResponse> WeatherResponses { get; set; }
        public IConfiguration Config { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("MySqlDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WeatherResponse>(entity =>
            {
                entity.HasKey(e => e.dt);
                entity.Property(e => e.name).IsRequired();
            });
            
        }
    }
}
