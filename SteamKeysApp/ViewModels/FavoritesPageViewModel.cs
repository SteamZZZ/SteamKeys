namespace SteamKeysApp.ViewModels;

public partial class FavoritesPageViewModel : BaseViewModel
{
    public ObservableCollection<GameItemViewModel> LikedGames { get; set; } = new();

    readonly GamesService gamesService;
    readonly ProfileService profileService;

    public FavoritesPageViewModel(GamesService gs, ProfileService ls)
    {
        gamesService = gs;
        profileService = ls;
    }

    public async Task LoadLikedGames()
    {
        if (profileService.UserId != -1)
        {
            IsBusy = true;

            LikedGames.Clear();
            
            var games = await gamesService.GetFavorites(profileService.UserId);
            foreach (var g in games)
            {
                var gameVM = new GameItemViewModel(g, gamesService, profileService);
                LikedGames.Add(gameVM);
            }

            IsBusy = false;
        }
    }
}
