using BootlegSpotifyApi.DTOs;

namespace BootlegSpotifyApi.Interfaces.Services;

public interface IAlbumService
{
    public Task AddAlbum(Guid authorId, AddAlbumDto albumDto);
    public Task<AlbumDto> GetAlbum(Guid id);
    public Task UpdateAlbum(Guid id, UpdateAlbumDto albumDto);
    public Task DeleteAlbum(Guid id);
}