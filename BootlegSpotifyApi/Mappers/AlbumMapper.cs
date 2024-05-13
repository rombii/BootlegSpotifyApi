using BootlegSpotifyApi.DTOs.Get;
using BootlegSpotifyApi.DTOs.Post;
using BootlegSpotifyApi.DTOs.Put;
using BootlegSpotifyApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace BootlegSpotifyApi.Mappers;

public static class AlbumMapper
{
    private static readonly string[] AllowedContentTypes = ["image/jpeg", "image/png"];
    public static void MapAlbum(this IEndpointRouteBuilder app)
    {
        app.MapPost("/add_album", (AddAlbumDto albumDto, Guid authorId, IAlbumService albumService) =>
            {
                albumService.AddAlbum(authorId, albumDto);
            })
            .WithName("AddAlbum")
            .WithOpenApi()
            .AddFluentValidationAutoValidation();
        app.MapGet("/album/{id}", (Guid id, IAlbumService albumService) =>
            {
                var result = albumService.GetAlbum(id).Result;
                return Results.Ok(result);
            })
            .WithName("GetAlbum")
            .WithOpenApi()
            .Produces<AlbumDto>();
        app.MapPut("/update_album/{id}", (Guid id, UpdateAlbumDto albumDto, IAlbumService albumService) =>
            {
                albumService.UpdateAlbum(id, albumDto);
                return Results.NoContent();
            })
            .WithName("UpdateAlbum")
            .WithOpenApi();
        app.MapDelete("/delete_album/{id}", (Guid id, IAlbumService albumService) =>
            {
                albumService.DeleteAlbum(id);
            })
            .WithName("DeleteAlbum")
            .WithOpenApi();
        app.MapPost("/upload_cover", ([FromBody] IFormFile formFile, IAlbumService albumService) =>
            {
                if (!AllowedContentTypes.Contains(formFile.ContentType))
                {
                    return Results.BadRequest("Invalid file type. Only JPEG and PNG files are allowed.");
                }

                return Results.Ok(albumService.AddCover(formFile).Result);
            })
            .WithName("UploadCover")
            .WithOpenApi()
            .DisableAntiforgery();

        app.MapDelete("/delete_cover/{id}", (Guid id, IAlbumService albumService) =>
            {
                albumService.DeleteCover(id).Wait();
                return Results.Ok();
            })
            .WithName("DeleteCover")
            .WithOpenApi();
    }
}