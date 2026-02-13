using Serilog;
using Serilog.Events;

namespace GalleryPixels.Api.Common.Configurations;

internal static class SerilogConfig
{
    /// <summary>
    ///     Configures misc settings for serilog.
    /// </summary>
    internal static void Configure(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Filter.ByExcluding(log => log.ShouldExcludeLog())
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithAssemblyName()
            .Enrich.WithAssemblyVersion()
            .Enrich.WithAssemblyInformationalVersion()
            .WriteTo.Async(
                writeTo =>
                {
                    writeTo.Console();
                }
            )
            .CreateLogger();
    }

    /// <summary>
    ///     Enriches the HTTP request log with additional data via the Diagnostic Context
    /// </summary>
    /// <param name="diagnosticContext">The Serilog diagnostic context</param>
    /// <param name="httpContext">The current HTTP Context</param>
    internal static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString()!);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent.FirstOrDefault()!);
    }

    /// <summary>
    ///     Sets up serilog to be used with <see cref="Microsoft.Extensions.Logging.ILogger" />.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors</param>
    internal static IServiceCollection AddSerilog(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        return services;
    }

    /// <summary>
    ///     Checks whether a <see cref="LogEvent" /> should be excluded.
    /// </summary>
    /// <param name="logEvent">The <see cref="LogEvent" />.</param>
    /// <returns>
    ///     Whether a <see cref="LogEvent" /> should be excluded.
    /// </returns>
    private static bool ShouldExcludeLog(this LogEvent logEvent)
    {
        if (!logEvent.Properties.TryGetValue("RequestPath", out var requestPath))
            return false;

        var requestPathString = requestPath.ToString();
        return requestPathString.StartsWith("\"/health", StringComparison.Ordinal) ||
            requestPathString.Equals("\"/favicon.ico\"", StringComparison.Ordinal);
    }
}