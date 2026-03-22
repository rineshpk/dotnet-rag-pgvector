using DotNetRagPgvector.Application.Abstractions;
using DotNetRagPgvector.Infrastructure.Embeddings;
using DotNetRagPgvector.Infrastructure.LLM;
using DotNetRagPgvector.Infrastructure.Persistence;
using DotNetRagPgvector.Infrastructure.VectorStores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRagPgvector.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration config,
    string connectionString)
{
    services.AddDbContext<VectorDbContext>(options =>
        options.UseNpgsql(connectionString, o => o.UseVector()));

    services.AddScoped<IVectorStore, PgVectorStore>();

    var provider = config["AI:Provider"];

    if (provider == "Ollama")
    {
        services.AddScoped<IEmbeddingService, OllamaEmbeddingService>();
        services.AddScoped<ILLMService, OllamaLLMService>();
    }
    else if (provider == "OpenAI")
    {
        services.AddScoped<IEmbeddingService, OpenAIEmbeddingService>();
        services.AddScoped<ILLMService, OpenAILLMService>();
    }

    return services;
}
}