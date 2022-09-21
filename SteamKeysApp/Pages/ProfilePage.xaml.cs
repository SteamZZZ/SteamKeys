namespace SteamKeysApp.Pages;

public partial class ProfilePage : ContentPage
{
    ProfilePageViewModel vm => BindingContext as ProfilePageViewModel;

    public ProfilePage(ProfilePageViewModel profilePageViewModel)
	{
		InitializeComponent();
		BindingContext = profilePageViewModel;
	}
}