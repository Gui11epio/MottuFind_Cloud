using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sprint1_C_.Infrastructure.Data;

namespace TDSPM.Infrastructure.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // ❗ Substitua com sua connection string real (Oracle, SQLite, SQL Server etc.)
            optionsBuilder.UseOracle("Data Source=xxxxxxx/orcl;User Id=xxxxxx;Password=xxxxxx;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
