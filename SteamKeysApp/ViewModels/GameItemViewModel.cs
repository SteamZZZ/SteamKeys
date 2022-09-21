namespace SteamKeysApp.ViewModels;

public partial class GameItemViewModel : BaseViewModel
{
    public Game Game { get; set; }
    readonly GamesService _gamesService;
    readonly ProfileService _profileService;

    [ObservableProperty] string name;
    [ObservableProperty] string image;

    [ObservableProperty] bool isFree;
    [ObservableProperty] Color color;
    [ObservableProperty] bool isInStore;

    [ObservableProperty] string priceRu;
    [ObservableProperty] string lowestPrice;
    [ObservableProperty] bool isAvaliable;

    [ObservableProperty] bool isFavorite;

    public GameItemViewModel(Game game, GamesService gs, ProfileService ps)
    {
        Game = game;
        _gamesService = gs;
        _profileService = ps;

        Name = Game.Name;
        Image = Game.Image;

        IsFree = Game.PriceRu == 0 ? true : false;
        Color = IsFree ? Colors.Green : Colors.Blue;
        PriceRu = IsFree ? "Бесплатно" : $"{Game.PriceRu}₽";

        isInStore = Game.StoreLowestPrice != 0;
        LowestPrice = $"{Game.StoreLowestPrice}";
        IsAvaliable = Game.IsAvaliable;
    }

    [RelayCommand]
    async Task ToFavorite()
    {
        if (_profileService.UserId != -1)
            IsFavorite = await _gamesService.ToFavorite(_profileService.UserId, Game.SteamId);
        else
            await Shell.Current.DisplayAlert("Ooops", $"You need to be logged in.", "Ok");
    }

    [RelayCommand]
    async Task GoToGameDetailsPage(Game game)
    {
        IsBusy = true;

        if (game is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(GameDetailsPage)}", true, new Dictionary<string, object>
            {
                { "Game", game }
            });

        IsBusy = false;
    }
}
