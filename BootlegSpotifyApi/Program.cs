using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Mappers;
using BootlegSpotifyApi.Services;
using BootlegSpotifyApi.Validators;
using FluentValidation;
using MongoDB.Driver;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IMongoClient, MongoClient>(
    _ => new MongoClient(builder.Configuration.GetConnectionString("MongoDBConnection"))
);
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddAntiforgery();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<AddAlbumValidator>();


var app = builder.Build();

// var mapper = app.Services.GetRequiredService<IMapper>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapAuthor();
app.MapAlbum();
app.MapSong();
// mapper.MapAll(app);

app.Run();