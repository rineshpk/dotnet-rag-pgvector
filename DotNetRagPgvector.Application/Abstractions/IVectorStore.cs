using DotNetRagPgvector.Domain.Models;

namespace DotNetRagPgvector.Application.Abstractions;

public interface IVectorStore
{
    Task StoreAsync(Document document, float[] embedding);

    Task<IReadOnlyList<RetrievedDocument>> SearchAsync(float[] queryEmbedding, int topK);
}