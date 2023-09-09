using System.Reflection;
using Mediator;

namespace GalleryPixels.Api.Application.Endpoints.Version;

public class GetVersionQuery : IRequest<VersionResponse>
{
    public GetVersionQuery(Assembly assembly)
    {
        Assembly = assembly;
    }

    public Assembly Assembly { get; init; }
}