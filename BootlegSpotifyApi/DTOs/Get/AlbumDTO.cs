using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs.Get;

public class AlbumDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required bool IsSingle { get; set; }
    public required List<SongDto> Songs { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public required string CoverId { get; set; }
}