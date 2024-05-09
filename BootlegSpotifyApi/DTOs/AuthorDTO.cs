using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs;

public class AuthorDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public List<AlbumDTO> Albums { get; set; } = [];
}