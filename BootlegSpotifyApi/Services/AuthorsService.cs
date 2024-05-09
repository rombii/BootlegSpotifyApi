using System.Net;
using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Services;

public class AuthorsService(IMongoClient mongoClient) : IAuthorsService
{
    public async Task AddAuthor(AuthorDto authorDto)
    {
        var database = mongoClient.GetDatabase("AudioStreaming");
        var collection = database.GetCollection<BsonDocument>("Authors");
        var document = authorDto.ToBsonDocument();
        await collection.InsertOneAsync(document);
    }

    public async Task<AuthorPageDto> GetAuthor(Guid id)
    {
        var database = mongoClient.GetDatabase("AudioStreaming");
        var collection = database.GetCollection<AuthorDto>("Authors");

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
        var database = mongoClient.GetDatabase("AudioStreaming");
        var collection = database.GetCollection<AuthorDto>("Authors");

        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, id);
        var update = Builders<AuthorDto>.Update.Set(author => author.Name, authorDto.Name);
        await collection.UpdateOneAsync(filter, update);
        
        await collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task DeleteAuthor(Guid id)
    {
        var database = mongoClient.GetDatabase("AudioStreaming");
        var collection = database.GetCollection<AuthorDto>("Authors");

        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, id);
        await collection.FindOneAndDeleteAsync(filter);
    }
}