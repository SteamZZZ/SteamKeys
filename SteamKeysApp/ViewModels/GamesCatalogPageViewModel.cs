using MvvmHelpers;

namespace SteamKeysApp.ViewModels;

public partial class GamesCatalogPageViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableRangeCollection<GameItemViewModel> allGames = new();
    [ObservableProperty]
    ObservableRangeCollection<GameItemViewModel> searchedGames = new();

    [ObservableProperty] bool isRefreshing;
    [ObservableProperty] string text;

    private int _itemsToLoad = 40;

    readonly GamesService gamesService;
    readonly ProfileService profileService;

    public GamesCatalogPageViewModel(GamesService gs, ProfileService ls)
    {
        gamesService = gs;
        profileService = ls;

        Task.Run(() => LoadMoreGames()); // catalog init
    }

    [RelayCommand]
    public async Task LoadMoreGames()
    {
        IsRefreshing = true;

        var games = await gamesService.LoadGames(AllGames.Count + 1, _itemsToLoad);
        foreach (var g in games)
        {
            var gameVM = new GameItemViewModel(g, gamesService, profileService);
            AllGames.Add(gameVM);
        }

        IsRefreshing = false;
    }

    [RelayCommand]
    public async Task LoadSearchedGames() // Delay
    {
        IsRefreshing = true;

        SearchedGames.Clear();

        if (!string.IsNullOrEmpty(text))
        {
            var games = await gamesService.SearchGamesAsync(text);

            if (games != null)
            {
                foreach (var g in games)
                {
                    var gameVM = new GameItemViewModel(g, gamesService, profileService);
                    SearchedGames.Add(gameVM);
                }
            }
        }

        IsRefreshing = false;
    }

    //[RelayCommand]
    //async Task Subscribe(GameItemViewModel vm)
    //{
    //    await subscriptionsService.UnSubscribeFromShowAsync(vm.Game);
    //    OnPropertyChanged(nameof(vm.IsSubscribed));
    //}
}

