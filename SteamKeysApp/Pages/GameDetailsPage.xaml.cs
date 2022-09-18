namespace SteamKeysApp.Pages;

public partial class GameDetailsPage : ContentPage
{
	public Game Game { get; set; }

    private GameDetailsViewModel viewModel => BindingContext as GameDetailsViewModel;

    public GameDetailsPage(GameDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
        await viewModel.InitializeAsync();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}