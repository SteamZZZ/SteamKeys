using Application = Microsoft.Maui.Controls.Application;

namespace SteamKeysApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        if (IsDesktop)
            MainPage = new DesktopShell();
        else
            MainPage = new MobileShell();

        Routing.RegisterRoute(nameof(GameDetailsPage), typeof(GameDetailsPage));
        //Routing.RegisterRoute(nameof(ShowDetailPage), typeof(ShowDetailPage));
        //Routing.RegisterRoute(nameof(EpisodeDetailPage), typeof(EpisodeDetailPage));
        //Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        //Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
    }

    public static bool IsDesktop
    {
        get
        {
#if WINDOWS || MACCATALYST
            return true;
#else
            return false;
#endif
        }
    }
}