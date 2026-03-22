namespace DotNetRagPgvector.Domain.Models;

public class RetrievedDocument
{
    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

    public double Score { get; set; }
}