using BootlegSpotifyApi.Interfaces;
using BootlegSpotifyApi.Mappers;

namespace BootlegSpotifyApi.Mappers;


public class Mapper : IMapper
{

    public async void MapAll(WebApplication app)
    {
        await SongMapper.Map(app);
        await AuthorsMapper.Map(app);
    }
}