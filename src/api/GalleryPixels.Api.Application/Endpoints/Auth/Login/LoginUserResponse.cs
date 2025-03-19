namespace GalleryPixels.Api.Application.Endpoints.Auth.Login;

public record LoginUserResponse(string? Token)
{
    public bool IsSuccess => Token != null;
}