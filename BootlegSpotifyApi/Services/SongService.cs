using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.DTOs.Response;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Misc;
using Google.Cloud.Storage.V1;

namespace BootlegSpotifyApi.Services;

public class SongService : ISongService
{
    private readonly StorageClient _storageClient = StorageClient.Create();
    public async Task<AddedResourceResponseDto> AddSong(IFormFile formFile)
    {
        var fileId = Guid.NewGuid();
        await _storageClient.UploadObjectAsync(
            "songs_local", 
            fileId.ToString(),
            formFile.ContentType,
            formFile.OpenReadStream());
        
        var response = new AddedResourceResponseDto
        {
            Id = fileId,
            ResourceUrl = await Helper.GetFileUrlAsync(fileId, "songs_local")
        };
        
        return response;
    }

    public async Task<string> GetSong(Guid id)
    {
        var result = await Helper.GetFileUrlAsync(id, "songs_local");
        return result;
    }
    
    public async Task DeleteSong(Guid id)
    {
        await _storageClient.DeleteObjectAsync("songs_local", id.ToString());
    }
    
}