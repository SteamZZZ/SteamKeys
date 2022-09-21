using SteamKeysApp.Models;
using System.Text.RegularExpressions;

namespace SteamKeysApp.ViewModels;

[QueryProperty(nameof(Game), nameof(Game))]
//[QueryProperty(nameof(GamesService), nameof(GamesService))]
public partial class GameDetailsViewModel : BaseViewModel
{
    public Game Game { get; set; }
    readonly GamesService _gamesService;
    readonly FavoritesPageViewModel _favoritesPageViewModel;

    #region FIELDS
    [ObservableProperty] string name;
    [ObservableProperty] string link;
    [ObservableProperty] string aboutTheGame;
    [ObservableProperty] string description;
    [ObservableProperty] string type;
    [ObservableProperty] bool isFree;

    [ObservableProperty] string background;
    [ObservableProperty] string image;
    //  [ObservableProperty] IEnumerable<string> screenshots;

    [ObservableProperty] IEnumerable<string> developers;
    [ObservableProperty] IEnumerable<string> categories;
    [ObservableProperty] IEnumerable<string> genres;

    [ObservableProperty] string priceUm;
    [ObservableProperty] string priceRu;
    [ObservableProperty] string priceKz;
    [ObservableProperty] string priceTr;
    [ObservableProperty] IEnumerable<Store> stores;
    #endregion

    public GameDetailsViewModel(FavoritesPageViewModel favoritesPageViewModel, GamesService gamesService)
    {
        _favoritesPageViewModel = favoritesPageViewModel;
        _gamesService = gamesService;
    }

    public async Task InitializeAsync()
    {
        GameInfo gameInfo = await _gamesService.GetGameInfo(Game.SteamId);

        Name = Game.Name;
        Link = Game.SteamRef;
        AboutTheGame = HtmlCleaner.Clean(gameInfo.Data.AboutTheGame);
        Description = HtmlCleaner.Clean(gameInfo.Data.DetailedDescription);
        Image = Game.Image;
        Type = gameInfo.Data.Type;
        IsFree = gameInfo.Data.IsFree;

        Background = gameInfo.Data.Background;
        Image = gameInfo.Data.HeaderImage;
        //    Screenshots = gameInfo.Data.Screenshots.Select(x => x.PathThumbnail);

        Developers = gameInfo.Data.Developers;
        Categories = gameInfo.Data.Categories.Select(c => c.Description);
        Genres = gameInfo.Data.Genres.Select(c => c.Description);

        PriceUm = $"${Game.PriceUm}";
        PriceRu = $"{Game.PriceRu}₽";
        PriceKz = $"{Game.PriceKz}₸";
        PriceTr = $"{Game.PriceTr}₺";
        Stores = Game.Stores;
        // IsAvaliable = gameInfo.Data
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}