# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./BootlegSpotifyApi/BootlegSpotifyApi.csproj" --disable-parallel
RUN dotnet publish "./BootlegSpotifyApi/BootlegSpotifyApi.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
EXPOSE 5001

ENTRYPOINT ["dotnet", "BootlegSpotifyApi.dll"]
