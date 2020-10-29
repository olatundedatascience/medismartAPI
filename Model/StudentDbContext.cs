using MedismartsAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Model
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<StudentInformation> StudentInformation { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                string conn_string = "Server=FCMB-IT-L16582\\TUNDE;Database=MedismartsTestAPI;User ID=sa;Password=oRA2018great@@;MultipleActiveResultSets=True";
                optionsBuilder.UseSqlServer(conn_string,builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
           base.OnConfiguring(optionsBuilder);
        }
    }

    public class StudentDbDesignTimeFactory : IDesignTimeDbContextFactory<StudentDbContext>
    {
        string conn_string = "Server=FCMB-IT-L16582\\TUNDE;Database=MedismartsTestAPI;User ID=sa;Password=oRA2018great@@;MultipleActiveResultSets=True";
        public StudentDbContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<StudentDbContext>();
            dbContextBuilder.UseSqlServer(conn_string);

            return new StudentDbContext(dbContextBuilder.Options);
        }
    }
}
