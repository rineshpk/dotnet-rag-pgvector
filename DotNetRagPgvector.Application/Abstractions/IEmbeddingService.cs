namespace DotNetRagPgvector.Application.Abstractions;

public interface IEmbeddingService
{
    Task<float[]> GenerateAsync(string text);
}