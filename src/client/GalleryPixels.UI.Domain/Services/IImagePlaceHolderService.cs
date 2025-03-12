using GalleryPixels.UI.Domain.Models;

namespace GalleryPixels.UI.Domain.Services;

public interface IImagePlaceHolderService
{
    Media GetRandomImagePlaceHolder();
}