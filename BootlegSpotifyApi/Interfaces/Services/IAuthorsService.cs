using BootlegSpotifyApi.DTOs;

namespace BootlegSpotifyApi.Interfaces.Services;

public interface IAuthorsService
{
    public Task<AuthorDto> AddAuthor(AuthorDto authorDto);
    public Task<AuthorPageDTO> GetAuthor(Guid id);
}