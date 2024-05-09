using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Misc;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Services;

public class AlbumService(IMongoClient mongoClient) : IAlbumsService
{
    public async Task AddAlbum(Guid authorId, AlbumDto albumDto)
    {
        var database = mongoClient.GetDatabase("AudioStreaming");
        var collection = database.GetCollection<AuthorDto>("Authors");
        
        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, authorId);
        var update = Builders<AuthorDto>.Update.Push(author => author.Albums, albumDto);
        await collection.UpdateOneAsync(filter, update);
    }

    public async Task<AlbumDto> GetAlbum(Guid id)
    {
        var collection = Helper.GetCollection(mongoClient);
        
        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Albums.Any(album => album.Id == id), true);
        var projection = Builders<AuthorDto>.Projection.ElemMatch(author => author.Albums, album => album.Id == id);
        
        var album = await collection.Find(filter).Project<AlbumDto>(projection).FirstOrDefaultAsync();
        return album;
    }

    public async Task UpdateAlbum(Guid id, UpdateAlbumDto albumDto)
    {
        var collection = Helper.GetCollection(mongoClient);
        
        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Albums.Any(album => album.Id == id), true);
        var update = Builders<AuthorDto>.Update.Set<>(author => author.Albums[-1], albumDto);
        await collection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAlbum(Guid id)
    {
        var collection = Helper.GetCollection(mongoClient);
        
        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Albums.Any(album => album.Id == id), true);
        var update = Builders<AuthorDto>.Update.PullFilter(author => author.Albums, album => album.Id == id);
        await collection.UpdateOneAsync(filter, update);
    }
}