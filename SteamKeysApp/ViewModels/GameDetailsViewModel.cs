using SteamKeysApp.Services;

namespace SteamKeysApp.ViewModels;

[QueryProperty(nameof(Game), "Game")]
public partial class GameDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    GameInfo gameInfo;

    readonly GamesService gamesService;

    public GameDetailsViewModel(GamesService gs)
    {
        gamesService = gs;
    }

    public async Task InitializeAsync()
    {
        gameInfo = await gamesService.GetGameInfo(1278060);
    }

    //[RelayCommand]
    //async Task GoBack()
    //{
    //	await Shell.Current.GoToAsync("..");
    //}
}
