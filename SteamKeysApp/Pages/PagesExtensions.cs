namespace SteamKeysApp.Pages;

public static class PagesExtensions
{
    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        // main tabs of the app
        builder.Services.AddSingleton<GamesCatalogPage>();
        builder.Services.AddSingleton<FavoritesPage>();
        builder.Services.AddSingleton<InfoPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<SettingsPage>();

        // pages that are navigated to
        builder.Services.AddTransient<GameDetailsPage>();

        return builder;
    }
}

