namespace DotNetRagPgvector.Application.DTOs;

public record UserInput
{
    public string Query { get; init; } = default!;
    public int TopK { get; init; } = 3;
}