namespace BootlegSpotifyApi.DTOs;

public class AlbumDTO
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required bool IsSingle { get; set; }
    public required List<SongDTO> Songs { get; set; }
    // public DateOnly ReleaseDate { get; set; }
    // TODO - Rethink the date format
    public required string CoverUrl { get; set; }
}