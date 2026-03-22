using Pgvector;

namespace DotNetRagPgvector.Infrastructure.Entities;

public class DesignPatternEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public Vector Embedding { get; set; } =default!;
}