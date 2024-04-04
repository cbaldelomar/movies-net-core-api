using System.Globalization;
using FastEndpoints;
using FastEndpoints.Swagger;
using Movies.Api;
using Movies.Application;
using Movies.Infrastructure;
using Serilog;

// See https://www.youtube.com/watch?v=mnPW8PURQOc&t=201s
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateLogger();

try
{
    Log.Logger.Information("Starting up");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();

    builder.Services.SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "Movies Api";
            s.Version = "v1";
        };

        o.ExcludeNonFastEndpoints = true;
    });

    builder.Services.AddFastEndpoints();

    // Add services to the container.

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    app.UseFastEndpoints();

    if (app.Environment.IsDevelopment())
    {
        //app.UseSwagger();
        //app.UseSwaggerUI();
        app.UseSwaggerGen(); // Must come after UseFastEndpoints call;
    }

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    //app.MapMovieEndpoints();

    app.Run();
}
catch
{
#pragma warning disable S6667 // Logging in a catch clause should pass the caught exception as a parameter.
    Log.Logger.Fatal("Application terminated unexpectedly");
#pragma warning restore S6667 // Logging in a catch clause should pass the caught exception as a parameter.
    throw;
}
finally
{
    Log.CloseAndFlush();
}
