using DotNetRagPgvector.Application.Abstractions;
using DotNetRagPgvector.Domain.Models;

namespace DotNetRagPgvector.Application.Services;

public class RagService(
    IEmbeddingService embeddingService,
    IVectorStore vectorStore,
    ILLMService llmService)
{
    private readonly IEmbeddingService _embeddingService = embeddingService;
    private readonly IVectorStore _vectorStore = vectorStore;
    private readonly ILLMService _llmService = llmService;

    public async Task<string> AskAsync(string question, int topK)
    {
        // Step 1: Embed query
        var queryEmbedding = await _embeddingService.GenerateAsync(question);

        // Step 2: Retrieve context
         var results = await _vectorStore.SearchAsync(queryEmbedding, topK);

        // Step 3: Build prompt
        var prompt = BuildPrompt(question, results);

        // Step 4: Generate answer
        return await _llmService.GenerateAsync(prompt);
        
    }
    
    private static string BuildPrompt(string question, IEnumerable<RetrievedDocument> results)
    {
        var prompt = "You are an expert solution architect.\n\n";

        foreach (var r in results)
        {
            prompt += $"Design Pattern: {r.Title}\nDescription: {r.Content}\n\n";
        }

        prompt += $"User Question: {question}";

        return prompt;
    }
}