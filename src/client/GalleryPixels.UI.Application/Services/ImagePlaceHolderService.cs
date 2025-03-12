using GalleryPixels.UI.Domain.Models;
using GalleryPixels.UI.Domain.Services;

namespace GalleryPixels.UI.Application.Services;

public class ImagePlaceHolderService : IImagePlaceHolderService
{
    private static readonly IReadOnlyList<Media> PlaceHolders =
    [
        new(4160, 6240, "https://minio.proxied.brammys.com/screenshots/2025/03/20220804-DSCF1014.JPG"),
        new(4582, 3055, "https://minio.proxied.brammys.com/screenshots/2025/03/20220807-DSCF2801.JPG"),
        new(5697, 3798, "https://minio.proxied.brammys.com/screenshots/2025/03/20220806-DSCF2133.JPG"),
        new(4160, 6240, "https://minio.proxied.brammys.com/screenshots/2025/03/20220807-DSCF2833.JPG"),
        new(3679, 5519, "https://minio.proxied.brammys.com/screenshots/2025/03/20220807-DSCF3167.JPG"),
        new(6240, 4160, "https://minio.proxied.brammys.com/screenshots/2025/03/20220808-DSCF4302.JPG"),
        new(6240, 4160, "https://minio.proxied.brammys.com/screenshots/2025/03/20220808-DSCF3685.JPG"),
        new(4866, 3244, "https://minio.proxied.brammys.com/screenshots/2025/03/20220806-DSCF2464.JPG"),
        new(5122, 7728, "https://minio.proxied.brammys.com/screenshots/2025/03/20230811-DSCF4536.JPG"),
        new(7728, 5152, "https://minio.proxied.brammys.com/screenshots/2025/03/20230814-DSCF5029.JPG"),
        new(7728, 5152, "https://minio.proxied.brammys.com/screenshots/2025/03/20230810-DSCF4356.JPG"),
        new(5618, 3745, "https://minio.proxied.brammys.com/screenshots/2025/03/20230911-DSCF3023.JPG"),
        new(4696, 7044, "https://minio.proxied.brammys.com/screenshots/2025/03/20231129-DSCF7243.JPG"),
        new(4335, 6502, "https://minio.proxied.brammys.com/screenshots/2025/03/20230814-DSCF5303.JPG"),
        new(547, 498, "https://minio.proxied.brammys.com/screenshots/2025/03/cHgLfNXN8G5UJbsw.png")
    ];

    public Media GetRandomImagePlaceHolder()
    {
        var rnd = new Random();
        return PlaceHolders[rnd.Next(PlaceHolders.Count)];
    }
}