using DotNetRagPgvector.Application.Abstractions;

namespace DotNetRagPgvector.Infrastructure.LLM;

public class OpenAILLMService : ILLMService
{
    public Task<string> GenerateAsync(string prompt)
    {
        throw new NotImplementedException("Integrate OpenAI here");
    }
}