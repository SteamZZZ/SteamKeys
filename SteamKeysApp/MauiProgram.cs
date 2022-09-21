using Microsoft.Maui.LifecycleEvents;
using SteamKeysApp.Services;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace SteamKeysApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureEssentials()
            .ConfigureServices()
            .ConfigurePages()
            .ConfigureViewModels()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("SFProDisplay-Thin.ttf", "SFProDisplayThin");
                fonts.AddFont("SFProDisplay-Regular.ttf", "SFProDisplayRegular");
                fonts.AddFont("SFProDisplay-Semibold.ttf", "SFProDisplaySemibold");
                fonts.AddFont("SFProDisplay-Bold.ttf", "SFProDisplayBold");
            });

        return builder.Build();
    }
}
