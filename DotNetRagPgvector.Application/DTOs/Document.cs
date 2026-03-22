namespace DotNetRagPgvector.Application.DTOs;

public class Document
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

}