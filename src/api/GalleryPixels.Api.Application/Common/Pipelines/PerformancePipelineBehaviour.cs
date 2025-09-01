using System.Diagnostics;
using Mediator;
using Microsoft.Extensions.Logging;

namespace GalleryPixels.Api.Application.Common.Pipelines;

public class PerformancePipelineBehaviour<TRequest, TResponse>(ILogger<PerformancePipelineBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        var startTime = Stopwatch.GetTimestamp();
        var response = await next(request, cancellationToken).ConfigureAwait(false);
        var elapsedTime = Stopwatch.GetElapsedTime(startTime);

        if (elapsedTime.TotalMilliseconds > 1500)
            logger.LogWarning(
                "Color-Chan.Api Long Running Request: {Name} ({ElapsedTime} milliseconds) {Request}",
                typeof(TRequest).Name,
                elapsedTime.TotalMilliseconds.ToString("F"),
                request
            );

        return response;
    }
}