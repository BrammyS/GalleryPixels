using GalleryPixels.UI.Domain.Models;
using GalleryPixels.UI.Domain.Services;

namespace GalleryPixels.UI.Application.Services;

public class ImagePlaceHolderService : IImagePlaceHolderService
{
    public Media GetRandomImagePlaceHolder()
    {
        const int multipleOf = 50;
        var rnd = new Random();
        return GetRandomImagePlaceHolder(multipleOf * rnd.Next(3, 7), multipleOf * rnd.Next(3, 7));
    }

    public Media GetRandomImagePlaceHolder(int width, int height)
    {
        return new Media(width, height, $"https://images.placeholders.dev/?width={width}&height={height}&text={width}x{height}");
    }
}