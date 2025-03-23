using GalleryPixels.Api;
using GalleryPixels.Api.Common.Configurations;
using GalleryPixels.Api.Infrastructure.Persistence;
using Serilog;

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.RegisterApi(builder.Configuration);
    builder.Host.UseSerilog();

    var origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>() ?? [];
    builder.Services.AddCors(x => x.AddDefaultPolicy(c => c.WithOrigins(origins)));

    var app = builder.Build();

    SerilogConfig.Configure(app.Services);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = SerilogConfig.EnrichFromRequest);

    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<GalleryPixelsDbContext>();
    await dbContext.ExecuteMigrationAsync().ConfigureAwait(false);

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    Console.WriteLine(ex);
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync().ConfigureAwait(false);
}