namespace BootlegSpotifyApi.DTOs;

public class AuthorPageDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<AlbumDTO> Albums { get; set; }
    
    // public required List<SongDTO> Top10Songs { get; set; }
    // TODO - Implement the top 10 songs
}