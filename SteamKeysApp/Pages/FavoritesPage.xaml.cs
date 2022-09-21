namespace SteamKeysApp.Pages;

public partial class FavoritesPage : ContentPage
{
    FavoritesPageViewModel vm => BindingContext as FavoritesPageViewModel;

    public FavoritesPage(FavoritesPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
	{
		base.OnAppearing();
        await vm.LoadLikedGames();
	}
}