using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using MongoDB.Bson;

namespace BootlegSpotifyApi.Mappers;

public class AuthorsMapper
{
    public async static Task Map(WebApplication app)
    {
        var authorService = app.Services.GetRequiredService<IAuthorsService>();
        app.MapPost("/addauthor", (AuthorDto authorDto) =>
            {
                authorService.AddAuthor(authorDto);
            })
            .WithName("AddAuthor")
            .WithOpenApi();
        app.MapGet("/author/{id}", (Guid id) =>
            {
                var result = authorService.GetAuthor(id).Result;
                return Results.Ok(result);
            })
            .WithName("GetAuthor")
            .WithOpenApi();
    }
}