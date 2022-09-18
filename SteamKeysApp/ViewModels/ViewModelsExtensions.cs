namespace SteamKeysApp.ViewModels;

public static class ViewModelsExtensions
{
    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ShellViewModel>();

        // main tabs of the app
        builder.Services.AddSingleton<GamesCatalogPageViewModel>();
        builder.Services.AddSingleton<FavoritesPageViewModel>();
        builder.Services.AddSingleton<InfoPageViewModel>();
        builder.Services.AddSingleton<ProfilePageViewModel>(); 
        builder.Services.AddSingleton<SettingsPageViewModel>();

        // pages that are navigated to
        builder.Services.AddTransient<GameDetailsViewModel>();

        return builder;
    }
}