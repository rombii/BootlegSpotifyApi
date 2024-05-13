using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs.Get;

public class AuthorDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<AlbumDto> Albums { get; set; } = [];
}