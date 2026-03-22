using DotNetRagPgvector.Application.Abstractions;
using DotNetRagPgvector.Infrastructure.Entities;
using DotNetRagPgvector.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore;
using Pgvector;
using DotNetRagPgvector.Domain.Models;

namespace DotNetRagPgvector.Infrastructure.VectorStores;

public class PgVectorStore(VectorDbContext db) : IVectorStore
{
    private readonly VectorDbContext _db = db;

    public async Task StoreAsync(Document doc, float[] embedding)
    {
        _db.DesignPatterns.Add(new DesignPatternEntity
        {
            Id = doc.Id,
            Name = doc.Title,
            Description = doc.Content,
            Embedding = new Vector(embedding)
        });

        await _db.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<RetrievedDocument>> SearchAsync(float[] queryEmbedding, int topK = 3)
    {
        var vector = new Vector(queryEmbedding);

        return await _db.DesignPatterns
            .OrderBy(x => x.Embedding.CosineDistance(vector))
            .Take(topK)
            .Select(x => new RetrievedDocument
            {
                Title = x.Name,
                Content = x.Description,
                Score = x.Embedding.CosineDistance(vector)
            })
            .ToListAsync();
    }
}