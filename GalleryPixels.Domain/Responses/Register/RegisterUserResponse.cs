namespace GalleryPixels.Domain.Responses.Register;

public record RegisterUserResponse(IReadOnlyList<RegisterUserErrorDetails> Errors);