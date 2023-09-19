using GalleryPixels.UI.Domain.Models;
using GalleryPixels.UI.Domain.Services;

namespace GalleryPixels.UI.Application.Services;

public class ImagePlaceHolderService : IImagePlaceHolderService
{
    public Media GetRandomImagePlaceHolder()
    {
        const int multipleOf = 2;
        var rnd = new Random();
        return GetRandomImagePlaceHolder(multipleOf * rnd.Next(200, 500), multipleOf * rnd.Next(200, 500));
    }

    public Media GetRandomImagePlaceHolder(int width, int height)
    {
        return new Media(width, height, $"https://source.unsplash.com/random/{width}x{height}");
    }
}