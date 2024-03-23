namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IRandomGenerator, RandomGenerator>();
        services.AddSingleton<IGameCreationService, GameCreationService>();
        services.AddSingleton<IGameService, GameService>();
    }
}