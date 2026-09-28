using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GameStore.Api.Dtos;

public record GameDetailsDto
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);