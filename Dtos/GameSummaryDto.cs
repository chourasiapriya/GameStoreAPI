using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GameStore.Api.Dtos;

public record GameSummaryDto
(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);