using BootlegSpotifyApi.DTOs.Post;
using FluentValidation;

namespace BootlegSpotifyApi.Validators;

public class AddAlbumValidator : AbstractValidator<AddAlbumDto>
{
    public AddAlbumValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.IsSingle).NotNull().NotEmpty();
        RuleFor(x => x.Songs).NotNull().NotEmpty().Must(x => x.Count < 0);
        RuleFor(x => x.ReleaseDate).NotNull().NotEmpty();
        RuleFor(x => x.CoverId).NotNull().NotEmpty();
    }
}