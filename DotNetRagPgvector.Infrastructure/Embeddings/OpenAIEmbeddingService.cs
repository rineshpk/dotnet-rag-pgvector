using DotNetRagPgvector.Application.Abstractions;

namespace DotNetRagPgvector.Infrastructure.Embeddings;

public class OpenAIEmbeddingService : IEmbeddingService
{
    public Task<float[]> GenerateAsync(string text)
    {
        throw new NotImplementedException("Integrate OpenAI here");
    }
}