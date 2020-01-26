using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SoalJavab.DataLayer
{
    /// <summary>
    /// Only used by EF Tooling
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Using `{basePath}` as the BasePath");
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(basePath)
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString  = "Data Source=.;Initial Catalog=newDbCore;Integrated Security = true;Trusted_Connection=True";// configuration.GetConnectionString("ConnectionStrings");
                                                //.Replace("|DataDirectory|", Path.Combine(basePath, "wwwroot", "app_data"));
            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}