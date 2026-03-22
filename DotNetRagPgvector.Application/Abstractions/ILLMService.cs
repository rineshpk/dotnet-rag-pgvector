namespace DotNetRagPgvector.Application.Abstractions;

public interface ILLMService
{
    Task<string> GenerateAsync(string prompt);
}