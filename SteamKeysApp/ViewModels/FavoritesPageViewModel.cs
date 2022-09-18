namespace SteamKeysApp.ViewModels;

public partial class FavoritesPageViewModel : BaseViewModel
{
    public ObservableCollection<Game> LikedGames { get; set; } = new();
}
