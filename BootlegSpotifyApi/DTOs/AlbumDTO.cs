using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BootlegSpotifyApi.DTOs;

public class AlbumDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required bool IsSingle { get; set; }
    public required List<SongDto> Songs { get; set; }
    // public DateOnly ReleaseDate { get; set; }
    // TODO - Rethink the date format
    public required string CoverUrl { get; set; }
}