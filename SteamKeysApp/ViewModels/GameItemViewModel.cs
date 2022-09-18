namespace SteamKeysApp.ViewModels;

public partial class GameItemViewModel : BaseViewModel
{
    public Game Game { get; set; }

    [ObservableProperty]
    bool isSubscribed;

    public string Name => Game.Name;
    public string Image => Game.Image;
    //public bool IsAvaliable => Game.IsAvaliable;
    //public float PriceDollar => Game.PriceDollar;
    //public float PriceRu => Game.PriceRu;
    //public float PriceKz => Game.PriceKz;
    //public float PriceTr => Game.PriceTr;

    public string CheapestStore => Game.CheapestStore;
    public float LowestPrice => Game.LowestPrice;

    public GameItemViewModel(Game game, bool isSubscribed)
    {
        Game = game;
        IsSubscribed = isSubscribed;
    }

    [RelayCommand]
    Task NavigateToDetail() => Shell.Current.GoToAsync($"{nameof(GameDetailsPage)}?Id={Game.SteamId}");
}
