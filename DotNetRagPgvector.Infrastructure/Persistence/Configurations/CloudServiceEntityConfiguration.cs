using DotNetRagPgvector.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pgvector;

namespace DotNetRagPgvector.Infrastructure.Persistence.Configurations;

public class DesignPatternConfiguration : IEntityTypeConfiguration<DesignPatternEntity>
{
    public void Configure(EntityTypeBuilder<DesignPatternEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.Embedding)
            .HasColumnType("vector(768)");
    }
}

