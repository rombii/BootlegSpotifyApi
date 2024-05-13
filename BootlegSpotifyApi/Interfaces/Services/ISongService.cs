using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.DTOs.Response;

namespace BootlegSpotifyApi.Interfaces.Services;

public interface ISongService
{
    public Task<AddedResourceResponseDto> AddSong(IFormFile formFile);
    public Task<string> GetSong(Guid id);
    
    public Task DeleteSong(Guid id);
}