using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BootlegSpotifyApi.Mappers;

public static class SongMapper
{
    private static readonly string[] AllowedContentTypes = ["audio/mpeg", "audio/ogg", "audio/wav"];
    public static void MapSong(this IEndpointRouteBuilder app)
    {
        app.MapPost("/upload_song", (IFormFile formFile, ISongService songService) =>
            {
                if (!AllowedContentTypes.Contains(formFile.ContentType))
                {
                    return Results.BadRequest("Invalid file type. Only MP3, OGG, and WAV files are allowed.");
                }

                return Results.Ok(songService.AddSong(formFile).Result);
            })
            .WithName("UploadSong")
            .WithOpenApi()
            .DisableAntiforgery();

        app.MapDelete("/delete_song/{id}", (Guid id, ISongService songService) =>
            {
                songService.DeleteSong(id).Wait();
                return Results.Ok();
            })
            .WithName("DeleteSong")
            .WithOpenApi();
    }
}