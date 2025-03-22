namespace GalleryPixels.Domain.Responses;

public record LoginUserResponse(string? Token)
{
    public bool IsSuccess => Token != null;
}