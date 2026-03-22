using DotNetRagPgvector.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetRagPgvector.Infrastructure.Persistence;

public class VectorDbContext(DbContextOptions<VectorDbContext> options) : DbContext(options)
{
    public DbSet<DesignPatternEntity> DesignPatterns => Set<DesignPatternEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VectorDbContext).Assembly);
    }
}