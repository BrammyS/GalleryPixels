namespace GalleryPixels.Api.Application.Endpoints.Auth.Register;

public record RegisterUserResponse(IReadOnlyList<RegisterUserErrorDetails> Errors);