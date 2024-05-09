using BootlegSpotifyApi.Interfaces;
using BootlegSpotifyApi.Interfaces.Services;
using BootlegSpotifyApi.Mappers;
using BootlegSpotifyApi.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IMapper, Mapper>();
builder.Services.AddTransient<IMongoClient, MongoClient>(
    provider => new MongoClient(builder.Configuration.GetConnectionString("MongoDBConnection"))
);
builder.Services.AddTransient<IAuthorsService, AuthorsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

mapper.MapAll(app);

app.Run();