using DotNetRagPgvector.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace DotNetRagPgvector.Infrastructure.LLM;

public class OllamaLLMService : ILLMService
{
    private readonly OllamaApiClient _client;

    public OllamaLLMService(IConfiguration config)
    {
        var baseUrl = config["AI:Ollama:BaseUrl"];
        var model = config["AI:Ollama:ChatModel"];

        _client = new OllamaApiClient(baseUrl!, model!);
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var response = await _client.GetResponseAsync(prompt);
        return response.Text;
    }
}