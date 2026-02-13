using GalleryPixels.UI.Domain.Models;
using GalleryPixels.UI.Domain.Services;

namespace GalleryPixels.UI.Application.Services;

public class ImagePlaceHolderService : IImagePlaceHolderService
{
    public Media GetRandomImagePlaceHolder()
    {
        var rnd = new Random();
        var width = rnd.Next(200, 500);
        var height = rnd.Next(200, 500);
        return new Media(width, height, $"https://placehold.co/{width}x{height}?text=Placeholder");
    }
}