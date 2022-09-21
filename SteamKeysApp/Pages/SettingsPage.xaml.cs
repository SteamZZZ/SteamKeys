namespace SteamKeysApp.Pages;

public partial class SettingsPage : ContentPage
{
    SettingsPageViewModel vm => BindingContext as SettingsPageViewModel;

    public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}