using Microsoft.Maui.Controls.Platform;

namespace SteamKeysApp.Pages;

public partial class GameDetailsPage : ContentPage
{
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
}