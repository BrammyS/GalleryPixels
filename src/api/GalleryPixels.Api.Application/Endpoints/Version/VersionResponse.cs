namespace GalleryPixels.Api.Application.Endpoints.Version;

public class VersionResponse
{
    public VersionResponse(string version, DateTime buildDate)
    {
        Version = version;
        UtcBuildDate = buildDate;
    }

    public string Version { get; }
    public DateTime UtcBuildDate { get; }
}