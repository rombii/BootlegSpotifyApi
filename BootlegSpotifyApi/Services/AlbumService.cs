using BootlegSpotifyApi.DTOs.Get;
using BootlegSpotifyApi.DTOs.Post;
using BootlegSpotifyApi.DTOs.Put;
using BootlegSpotifyApi.DTOs.Response;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Misc;
using BootlegSpotifyApi.Validators;
using FluentValidation;
using Google.Cloud.Storage.V1;
using MongoDB.Driver;

namespace BootlegSpotifyApi.Services;

public class AlbumService(IMongoClient mongoClient) : IAlbumService
{
    private readonly StorageClient _storageClient = StorageClient.Create();
    
    public async Task AddAlbum(Guid authorId, AddAlbumDto albumDto)
    {
        var songsWithId = albumDto.Songs.Select(song => new SongDto
        {
            Id = Guid.NewGuid(),
            Title = song.Title,
            Length = song.Length,
            FileId = song.FileId
        }).ToList();
        
        var newAlbum = new AlbumDto
        {
            Id = Guid.NewGuid(),
            Title = albumDto.Title,
            CoverId = albumDto.CoverId,
            IsSingle = albumDto.IsSingle,
            ReleaseDate = albumDto.ReleaseDate,
            Songs = songsWithId,
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
            .Set(author => author.Albums[0].CoverId, albumDto.CoverId);

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
    
    public async Task<AddedResourceResponseDto> AddCover(IFormFile formFile)
    {
        var fileId = Guid.NewGuid();
        await _storageClient.UploadObjectAsync(
            "covers_local", 
            fileId.ToString(),
            formFile.ContentType,
            formFile.OpenReadStream());
        
        var response = new AddedResourceResponseDto
        {
            Id = fileId,
            ResourceUrl = await Helper.GetFileUrlAsync(fileId, "covers_local")
        };
        
        return response;
    }
    
    public async Task<string> GetCover(Guid id)
    {
        var result = await Helper.GetFileUrlAsync(id, "covers_local");
        return result;
    }
    
    public async Task DeleteCover(Guid id)
    {
        await _storageClient.DeleteObjectAsync("covers_local", id.ToString());
    }
    
}