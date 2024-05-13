using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.DTOs.Get;
using Google.Cloud.Storage.V1;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Misc;

public class Helper
{
    public static IMongoCollection<AuthorDto> GetCollection(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("AudioStreaming");
        return database.GetCollection<AuthorDto>("Authors");
    }
    public static async Task<string> GetFileUrlAsync(Guid id, string bucketName)
    {
        var urlSigner = await UrlSigner.FromCredentialFileAsync("storage_key.json");
        var url = await urlSigner.SignAsync(
            "songs_local", 
            id.ToString(), 
            TimeSpan.FromMinutes(15), 
            HttpMethod.Get);
        return url;
    }
}