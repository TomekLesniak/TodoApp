using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TodoLibrary
{
    /// <summary>
    /// DbContext factory for entity framework so it can be located in separate class library, instead
    /// of user interface.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionStringName = configuration.GetSection("ConnectionStringName").Value;
            var connectionString = configuration.GetConnectionString(connectionStringName);

            dbContextBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(dbContextBuilder.Options);
        }
    }
}