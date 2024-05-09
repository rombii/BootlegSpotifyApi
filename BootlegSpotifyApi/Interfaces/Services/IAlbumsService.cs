using BootlegSpotifyApi.DTOs;

namespace BootlegSpotifyApi.Interfaces.Services;

public interface IAlbumsService
{
    public Task AddAlbum(Guid authorId, AlbumDto albumDto);
    public Task<AlbumDto> GetAlbum(Guid id);
    public Task UpdateAlbum(Guid id, UpdateAlbumDto albumDto);
    public Task DeleteAlbum(Guid id);
}