using BootlegSpotifyApi.DTOs;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Misc;

public class Helper
{
    public static IMongoCollection<AuthorDto> GetCollection(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("AudioStreaming");
        return database.GetCollection<AuthorDto>("Authors");
    }
}