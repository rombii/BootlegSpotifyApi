using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs.Get;

public class AlbumWithoutSongsDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required bool IsSingle { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string CoverUrl { get; set; }
}