using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SteamKeysApp.Services;

public static class ServicesExtensions
{
    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        //builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<GamesService>();
        builder.Services.AddSingleton<SubscriptionsService>();

        return builder;
    }
}
