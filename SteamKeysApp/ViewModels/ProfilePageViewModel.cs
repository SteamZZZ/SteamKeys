namespace SteamKeysApp.ViewModels;

public partial class ProfilePageViewModel : BaseViewModel
{
	readonly ProfileService profileService;

    [ObservableProperty] bool loginIsVisible = true;
    [ObservableProperty] bool registrationIsVisible = false;
    [ObservableProperty] bool profileIsVisible = false;

    [ObservableProperty] int userId;

    [NotifyPropertyChangedFor("FieldsAreNotEmpty")]
    [ObservableProperty] string login;

    [NotifyPropertyChangedFor("FieldsAreNotEmpty")]
    [ObservableProperty] string password;

    public bool FieldsAreNotEmpty => 
        !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);

    public ProfilePageViewModel(ProfileService profileService)
    {
        this.profileService = profileService;
    }

    [RelayCommand] async Task SignIn()
	{
        int UserId = 1;

        IsBusy = true;
        UserId = await profileService.RegisterUser(Login, Password);
        IsBusy = false;

        LoginIsVisible = (UserId != -1);
        RegistrationIsVisible = (UserId == -1);
        ProfileIsVisible = false;
    }

    [RelayCommand]
    async Task LogIn()
    {
        IsBusy = true;
        UserId = await profileService.VerifyUser(Login, Password);
        IsBusy = false;

        if (userId == -1)
            Login = Password = "";

        LoginIsVisible = (UserId == -1);
        RegistrationIsVisible = false;
        ProfileIsVisible = (UserId != -1);
    }

    [RelayCommand]
    async Task LogOut()
    {
        UserId = -1;
        Login = Password = "";
        LoginIsVisible = true;
        RegistrationIsVisible = false;
        ProfileIsVisible = false;
    }

    [RelayCommand]
    void ShowRegistration()
    {
        Login = Password = "";

        LoginIsVisible = false;
        RegistrationIsVisible = true;
        ProfileIsVisible = false;
    }

    [RelayCommand]
    void ShowLogin()
    {
        Login = Password = "";

        LoginIsVisible = true;
        RegistrationIsVisible = false;
        ProfileIsVisible = false;
    }
}
