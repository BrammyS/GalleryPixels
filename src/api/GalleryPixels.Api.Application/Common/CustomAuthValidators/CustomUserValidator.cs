using GalleryPixels.Api.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace GalleryPixels.Api.Application.Common.CustomAuthValidators;

public class CustomUserValidator : IUserValidator<User>
{
    public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var errors = new List<IdentityError>();

        if (string.IsNullOrWhiteSpace(user.UserName) || user.UserName.Length < 4)
        {
            errors.Add(new IdentityError
            {
                Code = "UsernameTooShort",
                Description = "Username must be at least 4 characters long."
            });
        }

        return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
    }
}