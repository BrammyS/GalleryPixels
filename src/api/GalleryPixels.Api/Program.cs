using GalleryPixels.Api;
using GalleryPixels.Api.Domain.Services;
using GalleryPixels.Api.Infrastructure.Persistence;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateBootstrapLogger();

    logger.Information("Starting web host");

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.RegisterApi(builder.Configuration);
    builder.Services.AddSerilog(logger);
    builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

    var origins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>() ?? [];
    builder.Services.AddCors(x =>
        x.AddDefaultPolicy(c => c.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod()
        )
    );

    var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSerilogRequestLogging();
    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<GalleryPixelsDbContext>();
    await dbContext.ExecuteMigrationAsync().ConfigureAwait(false);

    var initialUserService = scope.ServiceProvider.GetRequiredService<IInitialUserService>();
    await initialUserService.CreateInitialUserAsync().ConfigureAwait(false);

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