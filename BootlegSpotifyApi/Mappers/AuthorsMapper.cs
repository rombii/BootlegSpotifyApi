using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using MongoDB.Bson;

namespace BootlegSpotifyApi.Mappers;

public class AuthorsMapper
{
    public async static Task Map(WebApplication app)
    {
        var authorService = app.Services.GetRequiredService<IAuthorsService>();
        app.MapPost("/add_author", (AuthorDto authorDto) =>
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
        app.MapPut("/update_author/{id}", (Guid id, UpdateAuthorDto authorDto) =>
            {
                authorService.UpdateAuthor(id, authorDto);
                return Results.NoContent();
            })
            .WithName("UpdateAuthor")
            .WithOpenApi();
        app.MapDelete("/delete_author/{id}", (Guid id) =>
            {
                authorService.DeleteAuthor(id);
            })
            .WithName("DeleteAuthor")
            .WithOpenApi();
    }
}