namespace SteamKeysApp.Pages;

public partial class GamesCatalogPage : ContentPage
{
    GamesCatalogPageViewModel vm => BindingContext as GamesCatalogPageViewModel;

    public GamesCatalogPage(GamesCatalogPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.InitializeAsync();
    }
}