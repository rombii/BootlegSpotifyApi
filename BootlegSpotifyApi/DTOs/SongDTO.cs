using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs;

public class SongDto : AddSongDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    // public required string Title { get; set; }
    // public required int Length { get; set; }
    // public int Popularity { get; set; } = 0;
    // public required string SongUrl { get; set; }
}