namespace Host.Exception;

public static class HostExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddApplication();
        builder.Host
            .UseSerilog((hostContext, provider, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(hostContext.Configuration)
                    .ReadFrom.Services(provider)
                    .Enrich.FromLogContext();
            });

        return builder;

    }

    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.MapEndpoints();
        app.UseSerilogRequestLogging();

        return app;

    }
}