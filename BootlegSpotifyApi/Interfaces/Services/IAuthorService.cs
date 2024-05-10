using BootlegSpotifyApi.DTOs;

namespace BootlegSpotifyApi.Interfaces.Services;

public interface IAuthorService
{
    public Task AddAuthor(AddAuthorDto authorDto);
    public Task<AuthorPageDto> GetAuthor(Guid id);
    public Task UpdateAuthor(Guid id, UpdateAuthorDto authorDto);
    public Task DeleteAuthor(Guid id);
}