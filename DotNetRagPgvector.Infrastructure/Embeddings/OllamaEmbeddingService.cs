using DotNetRagPgvector.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using OllamaSharp;

namespace DotNetRagPgvector.Infrastructure.Embeddings;

public class OllamaEmbeddingService : IEmbeddingService
{
    private readonly OllamaApiClient _client;

    public OllamaEmbeddingService(IConfiguration config)
    {
        var baseUrl = config["AI:Ollama:BaseUrl"];
        var model = config["AI:Ollama:EmbeddingModel"];

        _client = new OllamaApiClient(baseUrl!, model!);
    }

    public async Task<float[]> GenerateAsync(string text)
    {
        var result = await _client.EmbedAsync(text);
        return result.Embeddings[0].ToArray();
    }
}