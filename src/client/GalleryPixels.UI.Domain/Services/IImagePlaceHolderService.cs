using GalleryPixels.UI.Domain.Models;

namespace GalleryPixels.UI.Domain.Services;

public interface IImagePlaceHolderService
{
    Media GetRandomImagePlaceHolder();
    Media GetRandomImagePlaceHolder(int width, int height);
}