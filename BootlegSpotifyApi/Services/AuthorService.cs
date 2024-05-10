using System.Net;
using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Misc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Services;

public class AuthorService(IMongoClient mongoClient) : IAuthorService
{
    public async Task AddAuthor(AddAuthorDto authorDto)
    {
        var newAuthor = new AuthorDto
        {
            Id = Guid.NewGuid(),
            Name = authorDto.Name,
            Albums = []
        };
        var collection = Helper.GetCollection(mongoClient);
        await collection.InsertOneAsync(newAuthor);
    }

    public async Task<AuthorPageDto> GetAuthor(Guid id)
    {
        var collection = Helper.GetCollection(mongoClient);

        var projection = Builders<AuthorDto>.Projection.Exclude(author => author.Albums.Select(album => album.Songs));
        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, id);
        var author = await collection.Find(filter).Project<AuthorDto>(projection).FirstOrDefaultAsync();
        
        var authorPage = new AuthorPageDto
        {
            Id = author.Id,
            Name = author.Name,
            Albums = author.Albums
        };
        return authorPage;
    }
    
    public async Task UpdateAuthor(Guid id, UpdateAuthorDto authorDto)
    {
        var collection = Helper.GetCollection(mongoClient);

        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, id);
        var update = Builders<AuthorDto>.Update.Set(author => author.Name, authorDto.Name);
        await collection.UpdateOneAsync(filter, update);
        
        await collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task DeleteAuthor(Guid id)
    {
        var collection = Helper.GetCollection(mongoClient);

        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, id);
        await collection.FindOneAndDeleteAsync(filter);
    }
}