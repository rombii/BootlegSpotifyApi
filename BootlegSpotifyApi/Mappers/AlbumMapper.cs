using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;

namespace BootlegSpotifyApi.Mappers;

public class AlbumMapper
{
    public async static Task Map(WebApplication app)
    {
        var albumsService = app.Services.GetRequiredService<IAlbumService>();
        app.MapPost("/add_album", (AddAlbumDto albumDto, Guid authorId) =>
            {
                albumsService.AddAlbum(authorId, albumDto);
            })
            .WithName("AddAlbum")
            .WithOpenApi();
        app.MapGet("/album/{id}", (Guid id) =>
            {
                var result = albumsService.GetAlbum(id).Result;
                return Results.Ok(result);
            })
            .WithName("GetAlbum")
            .WithOpenApi();
        app.MapPut("/update_album/{id}", (Guid id, UpdateAlbumDto albumDto) =>
            {
                albumsService.UpdateAlbum(id, albumDto);
                return Results.NoContent();
            })
            .WithName("UpdateAlbum")
            .WithOpenApi();
        app.MapDelete("/delete_album/{id}", (Guid id) =>
            {
                albumsService.DeleteAlbum(id);
            })
            .WithName("DeleteAlbum")
            .WithOpenApi();
    }
}