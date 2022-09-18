namespace SteamKeysApp.Pages;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
		BindingContext = new InfoPageViewModel();
	}
}