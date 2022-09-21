namespace SteamKeysApp.Pages;

public partial class GamesCatalogPage : ContentPage
{
    GamesCatalogPageViewModel vm => BindingContext as GamesCatalogPageViewModel;

    public GamesCatalogPage(GamesCatalogPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void Animate(object sender, EventArgs e)
    {
        VisualElement ve = (VisualElement)sender;

        var a1 = ve.ScaleTo(2.0, 1000, Easing.BounceIn);
        var a2 = ve.ScaleTo(1.0, 1000, Easing.BounceOut);
        await Task.WhenAll(a1, a2);

        await Task.WhenAll(a1, a2);
    }

    private void searchBar_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var sb = (SteamKeysApp.Controls.HeaderControl)sender;

        if (vm != null)
        {
            if (string.IsNullOrEmpty(sb.TextToSearch))
            {
                gamesCollectionView.ItemsSource = vm.AllGames;
            }
            else
            {
                gamesCollectionView.ItemsSource = vm.SearchedGames;
            }
        }
    }
}