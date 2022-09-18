namespace SteamKeysApp.ViewModels;

public partial class GamesCatalogPageViewModel : BaseViewModel
{
    [ObservableProperty]
    string text;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    public List<GameItemViewModel> games;

    readonly GamesService gamesService;
    readonly SubscriptionsService subscriptionsService;

    public GamesCatalogPageViewModel(GamesService gs, SubscriptionsService ss)
    {
        gamesService = gs;
        subscriptionsService = ss;

        TryParseSteam.ParserManager pm = new TryParseSteam.ParserManager();
        pm.Get
    }

    public async Task InitializeAsync()
    {
        var games = await gamesService.GetGames();
        Games = LoadGames(games);
    }


    //[RelayCommand]
    //async Task GetGamesAsync()
    //{
    //    if (IsBusy)
    //        return;

    //    try
    //    {
    //        //if (connectivity.NetworkAccess != NetworkAccess.Internet)
    //        //{
    //        //    await Shell.Current.DisplayAlert("No connectivity!",
    //        //        $"Please check internet and try again.", "OK");
    //        //    return;
    //        //}

    //        IsBusy = true;
    //        var games = await gamesService.GetGames();

    //        if (Games.Count != 0)
    //            Games.Clear();

    //        foreach (var game in games)
    //            Games.Add(game);
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine($"Unable to get games: {ex.Message}");
    //        await Shell.Current.DisplayAlert("Error", $"Unable to get games: {ex.Message}", "OK");
    //    }
    //    finally
    //    {
    //        IsBusy = false;
    //        IsRefreshing = false;
    //    }
    //}


    [RelayCommand]
    async Task Subscribe(GameItemViewModel vm)
    {
        await subscriptionsService.UnSubscribeFromShowAsync(vm.Game);
        OnPropertyChanged(nameof(vm.IsSubscribed));
    }


    [RelayCommand]
    async void Search()
    {
        var games = await gamesService.SearchGamesAsync(Text);
        Games = LoadGames(games);
    }


    List<GameItemViewModel> LoadGames(IEnumerable<Game> games)
    {
        var gamesList = new List<GameItemViewModel>();

        if (games == null)
        {
            return gamesList;
        }

        foreach (var game in games)
        {
            var gameVM = new GameItemViewModel(game, subscriptionsService.IsSubscribed(game.SteamId));
            gamesList.Add(gameVM);
        }

        return gamesList;
    }


    [RelayCommand]
    async Task GoToDetailsAsync(Game game)
    {
        if (game is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(GameDetailsPage)}", true, new Dictionary<string, object>
            {
                { "Game", game }
            });
    }
}

