namespace GalleryPixels.Domain.Responses.Register;

public record RegisterUserResponse(IReadOnlyList<RegisterUserErrorDetails> Errors)
{
    public bool IsSuccess => Errors.Count == 0;
}