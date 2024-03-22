using System.Globalization;
using FastEndpoints;
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
    builder.Services.AddSwaggerGen();

    builder.Services.AddFastEndpoints();

    // Add services to the container.

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();

    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    //app.MapMovieEndpoints();
    app.UseFastEndpoints();

    app.Run();
}
catch
{
    Log.Logger.Fatal("Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}