using DotNetRagPgvector.Application.Abstractions;
using DotNetRagPgvector.Application.DTOs;

namespace DotNetRagPgvector.Application.Services;

public class IngestionService(
    IEmbeddingService embeddingService,
    IVectorStore vectorStore)
{
    private readonly IEmbeddingService _embeddingService = embeddingService;
    private readonly IVectorStore _vectorStore = vectorStore;

    public async Task IngestAsync(IEnumerable<Document> documents)
    {
        foreach (var document in documents)
        {
            var embedding = await _embeddingService.GenerateAsync(document.Content);

            await _vectorStore.StoreAsync(new Domain.Models.Document
            {
                Id = document.Id,
                Title = document.Title,
                Content = document.Content
            }, embedding);
        }
    }
}