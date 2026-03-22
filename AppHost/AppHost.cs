var builder = DistributedApplication.CreateBuilder(args);

//
// PostgreSQL (pgvector)
//
var postgres = builder.AddPostgres("postgres")
    .WithImage("pgvector/pgvector:pg16")
    .WithVolume("pgdata", "/var/lib/postgresql/data"); // persistent storage

var vectordb = postgres.AddDatabase("vectordb");

//
// Ollama
//
var ollama = builder.AddContainer("ollama", "ollama/ollama")
    .WithHttpEndpoint(name: "http", port: 11434, targetPort: 11434)
    .WithVolume("ollama-data", "/root/.ollama"); // persist models

//
// API Project
//
var api = builder.AddProject<Projects.DotNetRagPgvector_Api>("api")
    .WaitFor(vectordb)
    .WaitFor(ollama)
    .WithReference(vectordb)
    .WithEnvironment("AI__Ollama__BaseUrl", "http://localhost:11434");

//
// Build & Run
//
builder.Build().Run();