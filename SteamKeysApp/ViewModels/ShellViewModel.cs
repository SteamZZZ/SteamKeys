using SteamKeysApp.Resources.Strings;

namespace SteamKeysApp.ViewModels;

public partial class ShellViewModel : BaseViewModel
{
    public AppSection GamesCatalog { get; set; }
    public AppSection Favorites { get; set; }
    public AppSection Info { get; set; }
    public AppSection Profile { get; set; }

    public ShellViewModel()
    {
        GamesCatalog = new AppSection() { Title = AppResource.Games_Catalog, Icon = "games_catalog_page.svg", IconDark = "games_catalog_page_dark.svg", TargetType = typeof(GamesCatalogPage) };
        Favorites = new AppSection() { Title = AppResource.Favorites, Icon = "favorites_page.svg", IconDark = "favorites_page_dark.svg", TargetType = typeof(FavoritesPage) };
        Info = new AppSection() { Title = AppResource.Info, Icon = "info_page.svg", IconDark = "info_page_dark.svg", TargetType = typeof(InfoPage) };
        Profile = new AppSection() { Title = AppResource.Profile, Icon = "profile_page.svg", IconDark = "profile_page_dark.svg", TargetType = typeof(ProfilePage) };
    }
}
