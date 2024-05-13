using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs.Get;

public class SongDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required int Length { get; set; }
    public int Popularity { get; set; } = 0;
    public Guid FileId { get; set; }
}