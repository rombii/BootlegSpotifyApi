namespace BootlegSpotifyApi.DTOs;

public class AddAlbumDto
{
    public required string Title { get; set; }
    public required bool IsSingle { get; set; }
    public required List<AddSongDto> Songs { get; set; }
    // public DateOnly ReleaseDate { get; set; }
    // TODO - Rethink the date format
    public required string CoverUrl { get; set; }
}