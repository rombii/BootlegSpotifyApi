namespace BootlegSpotifyApi.DTOs.Post;

public class AddSongDto
{
    public required string Title { get; set; }
    public required int Length { get; set; }
    public Guid FileId { get; set; }
}