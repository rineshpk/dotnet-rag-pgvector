# .NET RAG with PostgreSQL pgvector + Aspire

A production-style **Retrieval-Augmented Generation (RAG)** implementation in .NET using PostgreSQL `pgvector`, .NET Aspire, and pluggable LLM providers (Ollama / OpenAI).

---

## Overview

This project demonstrates how to build a **semantic search + RAG pipeline** using modern .NET practices:

* Vector similarity search with **pgvector**
* LLM-based answer generation (Ollama / OpenAI)
* Clean Architecture (Domain, Application, Infrastructure)
* **.NET Aspire orchestration** (Postgres + Ollama + API)
* Provider-agnostic design (LLM + vector store)

---

## Architecture

```text
User Query
   ↓
Embedding Service (Ollama / OpenAI)
   ↓
Vector Store (pgvector)
   ↓
Top-K Retrieved Documents
   ↓
LLM (RAG Prompt)
   ↓
Generated Answer
```

---

## Solution Structure

```text
DotNetRagPgvector/
│
├── AppHost/                 # Aspire orchestration
├── Api/                     # Minimal API (entry point)
├── Application/             # Use cases + abstractions
│   ├── Abstractions/        # IEmbeddingService, IVectorStore, ILLMService
│   ├── Services/            # RagService, IngestionService
│   └── DTOs/                # AskRequest
│
├── Domain/                  # Core models
│   └── Entities/
│       ├── Document
│       └── RetrievedDocument
│
├── Infrastructure/          # Implementations
│   ├── Persistence/         # EF Core + pgvector
│   ├── VectorStores/        # PgVectorStore
│   ├── Embeddings/          # Ollama / OpenAI
│   ├── LLM/                 # Ollama / OpenAI
│   └── Options/             # Provider configs
```

---

## Features

* ✅ Semantic search using embeddings
* ✅ Retrieval-Augmented Generation (RAG)
* ✅ pgvector integration with PostgreSQL
* ✅ Clean architecture with strict separation of concerns
* ✅ Pluggable LLM providers (Ollama / OpenAI)
* ✅ Aspire-based container orchestration
* ✅ Scalar UI for API testing

---

## Tech Stack

* .NET 8+
* ASP.NET Core Minimal APIs
* Entity Framework Core
* PostgreSQL + pgvector
* .NET Aspire
* Ollama (local LLM)
* OpenAI (optional)

---

## Getting Started

### 1. Prerequisites

* .NET 8 SDK
* Docker Desktop
* .NET Aspire workload

```bash
dotnet workload install aspire
```

---

### 2. Run the Application

```bash
dotnet run --project AppHost
```

This starts:

* PostgreSQL (pgvector)
* Ollama
* API

---

### 3. Open API UI (Scalar)

Navigate to:

```text
http://localhost:<api-port>/scalar
```

---

## Testing the API

### POST `/ask`

```json
{
  "query": "How to handle distributed transactions?"
}
```

### Example Questions

* How to prevent cascading failures?
* How to scale read-heavy systems?
* How do microservices communicate asynchronously?

---

## Configuration

### appsettings.json

```json
{
  "AI": {
    "Provider": "Ollama",
    "Ollama": {
      "BaseUrl": "http://localhost:11434",
      "EmbeddingModel": "nomic-embed-text",
      "ChatModel": "phi4-mini"
    },
    "OpenAI": {
      "ApiKey": "",
      "EmbeddingModel": "text-embedding-3-small",
      "ChatModel": "gpt-4o-mini"
    }
  }
}
```

---

### Provider Switching

Switch between Ollama and OpenAI:

```json
"Provider": "Ollama"
// or
"Provider": "OpenAI"
```

No code changes required.

---

## 🗄️ Database & Vector Search

* Uses PostgreSQL with `pgvector`
* Embeddings stored as `vector` column
* Cosine similarity used for retrieval:

```c#
var results = await dbContext.DesignPatterns
    .OrderBy(x => x.Vector.CosineDistance(questionVector))
    .Take(2)
    .ToListAsync();
```

```sql
ORDER BY "Embedding" <=> @queryVector
LIMIT 2
```

---

## Data Seeding

* Automatic on startup
* Uses real-world **architecture patterns dataset**
* Embeddings generated via selected provider

---

## RAG Flow (Code-Level)

```csharp
// 1. Embed query
var queryEmbedding = await _embedding.GenerateAsync(query);

// 2. Retrieve relevant documents
var docs = await _vectorStore.SearchAsync(queryEmbedding);

// 3. Build prompt
// 4. Generate response using LLM
```

---

## Extensibility

You can easily extend:

### Vector Stores

* pgvector ✅
* Pinecone (future)
* FAISS (future)

### LLM Providers

* Ollama ✅
* OpenAI ✅
* Azure OpenAI (easy to add)

---

## Design Principles

* Domain is persistence-agnostic
* Infrastructure handles external dependencies
* Application orchestrates the RAG pipeline
* API acts as composition root

---

## Future Improvements

* Hybrid search (BM25 + vector)
* HNSW indexing for pgvector
* Streaming LLM responses
* Evaluation & benchmarking
* Metadata filtering

---

## Contributing

Contributions are welcome! Feel free to open issues or PRs.

---

## License

MIT License

---

## ⭐ Why This Project?

This repository demonstrates a **production-ready, provider-agnostic RAG architecture in .NET**.

---

## Acknowledgements

* pgvector
* .NET Aspire
* Ollama
