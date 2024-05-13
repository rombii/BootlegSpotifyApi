namespace BootlegSpotifyApi.DTOs.Post;

public class AddAlbumDto
{
    public required string Title { get; set; }
    public required bool IsSingle { get; set; }
    public required List<AddSongDto> Songs { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public required string CoverId { get; set; }
}