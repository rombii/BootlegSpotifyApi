using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.DTOs.Get;
using BootlegSpotifyApi.DTOs.Post;
using BootlegSpotifyApi.DTOs.Put;
using BootlegSpotifyApi.DTOs.Response;

namespace BootlegSpotifyApi.Interfaces.Services;

public interface IAlbumService
{
    public Task AddAlbum(Guid authorId, AddAlbumDto albumDto);
    public Task<AlbumDto> GetAlbum(Guid id);
    public Task UpdateAlbum(Guid id, UpdateAlbumDto albumDto);
    public Task DeleteAlbum(Guid id);
    public Task<AddedResourceResponseDto> AddCover(IFormFile formFile);
    public Task DeleteCover(Guid id);
}