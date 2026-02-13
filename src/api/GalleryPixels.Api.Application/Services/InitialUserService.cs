using GalleryPixels.Api.Application.Common.AppSettings;
using GalleryPixels.Api.Domain.Entities;
using GalleryPixels.Api.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GalleryPixels.Api.Application.Services;

public class InitialUserService(IOptions<InitialUserSettings> options, UserManager<User> userManager, ILogger<InitialUserService> logger) : IInitialUserService
{
    public async Task CreateInitialUserAsync()
    {
        var initialUserSettings = options.Value;
        if (string.IsNullOrEmpty(initialUserSettings.Email) || string.IsNullOrEmpty(initialUserSettings.Password) || string.IsNullOrEmpty(initialUserSettings.Username))
        {
            logger.LogWarning("Initial user settings are not properly configured. Skipping initial user creation");
            return;
        }

        var existingUser = await userManager.FindByNameAsync(initialUserSettings.Username).ConfigureAwait(false);
        if (existingUser != null)
        {
            logger.LogInformation("Initial user with username '{Username}' already exists. Skipping initial user creation", initialUserSettings.Username);
            return;
        }

        var initialUser = new User
        {
            UserName = initialUserSettings.Username,
            Email = initialUserSettings.Email,
            IsInitialUser = true
        };

        var result = await userManager.CreateAsync(initialUser, initialUserSettings.Password).ConfigureAwait(false);
        if (result.Succeeded)
        {
            logger.LogInformation("Initial user with username '{Username}' created successfully", initialUserSettings.Username);
        }
        else
        {
            logger.LogWarning("Failed to create initial user with username '{Username}'. Errors: {Errors}", initialUserSettings.Username, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}