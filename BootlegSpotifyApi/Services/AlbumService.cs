using BootlegSpotifyApi.DTOs;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Misc;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Services;

public class AlbumService(IMongoClient mongoClient) : IAlbumService
{
    public async Task AddAlbum(Guid authorId, AddAlbumDto albumDto)
    {
        List<SongDto> songs = [];
        songs.AddRange(albumDto.Songs.Select(song => new SongDto
        {
            Id = Guid.NewGuid(),
            Title = song.Title,
            Length = song.Length,
            Popularity = song.Popularity,
            SongUrl = song.SongUrl
        }));
        var newAlbum = new AlbumDto
        {
            Id = Guid.NewGuid(),
            Title = albumDto.Title,
            CoverUrl = albumDto.CoverUrl,
            IsSingle = albumDto.IsSingle,
            Songs = songs
        };
        
        var collection = Helper.GetCollection(mongoClient);
        
        var filter = Builders<AuthorDto>.Filter.Eq(author => author.Id, authorId);
        var update = Builders<AuthorDto>.Update.Push(author => author.Albums, newAlbum);
        await collection.UpdateOneAsync(filter, update);
    }

    public async Task<AlbumDto> GetAlbum(Guid id)
    {
        var collection = Helper.GetCollection(mongoClient);

        var filter = Builders<AuthorDto>.Filter.ElemMatch(
            author => author.Albums, 
            album => album.Id == id
        );
        var projection = Builders<AuthorDto>.Projection.ElemMatch(
            author => author.Albums, 
            album => album.Id == id
        );

        var author = await collection.Find(filter).Project<AuthorDto>(projection).FirstOrDefaultAsync();
        var album = author.Albums.First(album => album.Id == id);

        return album;
    }
    public async Task UpdateAlbum(Guid id, UpdateAlbumDto albumDto)
    {
        var collection = Helper.GetCollection(mongoClient);

        var filter = Builders<AuthorDto>.Filter.ElemMatch(
            author => author.Albums,
            album => album.Id == id
        );
        var update = Builders<AuthorDto>.Update
            .Set(author => author.Albums[0].Title, albumDto.Title)
            .Set(author => author.Albums[0].CoverUrl, albumDto.CoverUrl);

        await collection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAlbum(Guid id)
    {
        var collection = Helper.GetCollection(mongoClient);

        var filter = Builders<AuthorDto>.Filter.ElemMatch(
            author => author.Albums,
            album => album.Id == id
        );
        var update = Builders<AuthorDto>.Update.PullFilter(
            author => author.Albums,
            album => album.Id == id
        );

        await collection.UpdateOneAsync(filter, update);
    }
}