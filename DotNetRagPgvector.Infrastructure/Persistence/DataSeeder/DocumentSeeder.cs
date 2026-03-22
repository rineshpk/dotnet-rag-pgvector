using DotNetRagPgvector.Application.Abstractions;
using DotNetRagPgvector.Infrastructure.Entities;

namespace DotNetRagPgvector.Infrastructure.Persistence.DataSeeder;
public class DocumentSeeder
{
    public static async Task SeedAsync(VectorDbContext db, IEmbeddingService embeddingService)
    {
        if (db.DesignPatterns.Any()) return;

        var services = new List<DesignPatternEntity>
        {
         new()
            {
                Name = "API Gateway Pattern",
                Description = "The API Gateway pattern acts as a single entry point for all client requests in a microservices architecture. It handles request routing, composition, authentication, and rate limiting. This pattern helps simplify client interactions and improves security and observability."
            },
            new()
            {
                Name = "CQRS Pattern",
                Description = "Command Query Responsibility Segregation (CQRS) separates read and write operations into different models. This improves scalability and performance, especially in systems with high read and write loads. It is often combined with Event Sourcing."
            },
            new()
            {
                Name = "Event-Driven Architecture",
                Description = "Event-driven architecture enables services to communicate asynchronously using events. Producers emit events without knowing consumers, allowing loose coupling and scalability. This pattern is ideal for distributed systems and real-time processing."
            },
            new()
            {
                Name = "Database per Service",
                Description = "Each microservice owns its own database to ensure loose coupling and independent scalability. This prevents tight coupling caused by shared databases and allows each service to choose the most suitable data storage technology."
            },
            new()
            {
                Name = "Circuit Breaker Pattern",
                Description = "The circuit breaker pattern prevents cascading failures in distributed systems. When a service fails repeatedly, the circuit opens and stops further calls, allowing the system to recover and preventing resource exhaustion."
            },
            new()
            {
                Name = "Bulkhead Pattern",
                Description = "The bulkhead pattern isolates different parts of a system to prevent failures from spreading. Resources such as threads or connections are partitioned so that one failing component does not impact the entire system."
            },
            new()
            {
                Name = "Retry Pattern",
                Description = "The retry pattern handles transient failures by automatically retrying failed operations. It is commonly used in distributed systems where network or service interruptions are temporary."
            },
            new()
            {
                Name = "Saga Pattern",
                Description = "The saga pattern manages distributed transactions across multiple services using a sequence of local transactions. Each step triggers the next, and compensating actions are used to maintain consistency in case of failure."
            }
        };

        foreach (var s in services)
        {
            var embedding = await embeddingService.GenerateAsync(s.Description);
            s.Embedding = new Pgvector.Vector(embedding);
        }

        db.DesignPatterns.AddRange(services);
        await db.SaveChangesAsync();
    }
}