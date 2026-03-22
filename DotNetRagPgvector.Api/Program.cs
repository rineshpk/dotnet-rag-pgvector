
using DotNetRagPgvector.Application.Abstractions;
using DotNetRagPgvector.Application.Services;
using DotNetRagPgvector.Infrastructure;
using DotNetRagPgvector.Infrastructure.Persistence;
using DotNetRagPgvector.Infrastructure.Persistence.DataSeeder;
using DotNetRagPgvector.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(
    builder.Configuration,
    builder.Configuration.GetConnectionString("vectordb")!);

builder.Services.AddScoped<IngestionService>();
builder.Services.AddScoped<RagService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // /openapi/v1.json

    app.MapScalarApiReference(options =>
    {
        options.Title = "DotNet RAG pgvector API";
        options.Theme = ScalarTheme.BluePlanet;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VectorDbContext>();
    var embeddingService = scope.ServiceProvider.GetRequiredService<IEmbeddingService>();
    
    // Apply migrations and seed data for demonstration purposes. In production, consider using a more robust approach.
    await dbContext.Database.MigrateAsync();
    await DocumentSeeder.SeedAsync(dbContext, embeddingService);
}

app.MapPost("/ingest", async ( List<Document> docs, IngestionService ingestion) =>
{
    await ingestion.IngestAsync(docs);
    return Results.Ok();
}).WithDescription("Ingest documents with their embeddings into the vector store");

app.MapPost("/ask", async (UserInput request, RagService rag) =>
{
    var result = await rag.AskAsync(request.Query, request.TopK);
    return Results.Ok(new { answer = result });
})
.WithDescription("Ask a question using RAG (Retrieval-Augmented Generation)");

app.Run();