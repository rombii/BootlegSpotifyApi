using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.DTOs.Get;
using BootlegSpotifyApi.DTOs.Post;
using BootlegSpotifyApi.DTOs.Put;
using BootlegSpotifyApi.Interfaces.Services;
using MongoDB.Bson;

namespace BootlegSpotifyApi.Mappers;

public static class AuthorMapper
{
    public static void MapAuthor(this IEndpointRouteBuilder app)
    {
        app.MapPost("/add_author", (AddAuthorDto authorDto, IAuthorService authorService) =>
            {
                authorService.AddAuthor(authorDto);
            })
            .WithName("AddAuthor")
            .WithOpenApi();
        app.MapGet("/author/{id}", (Guid id, IAuthorService authorService) =>
            {
                var result = authorService.GetAuthor(id).Result;
                return Results.Ok(result);
            })
            .WithName("GetAuthor")
            .WithOpenApi()
            .Produces<AuthorPageDto>();
        app.MapPut("/update_author/{id}", (Guid id, UpdateAuthorDto authorDto, IAuthorService authorService) =>
            {
                authorService.UpdateAuthor(id, authorDto);
                return Results.NoContent();
            })
            .WithName("UpdateAuthor")
            .WithOpenApi();
        app.MapDelete("/delete_author/{id}", (Guid id, IAuthorService authorService) =>
            {
                authorService.DeleteAuthor(id);
            })
            .WithName("DeleteAuthor")
            .WithOpenApi();
    }
}