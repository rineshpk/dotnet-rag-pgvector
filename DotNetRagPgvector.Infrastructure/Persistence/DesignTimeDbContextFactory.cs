using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DotNetRagPgvector.Infrastructure.Persistence;

public class DesignTimeDbContextFactory 
    : IDesignTimeDbContextFactory<VectorDbContext>
{
    public VectorDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<VectorDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=vectordb;Username=postgres;",
                o => o.UseVector())
            .Options;

        return new VectorDbContext(options);
    }
}