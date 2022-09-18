namespace SteamKeysApp.ViewModels;

public partial class SettingsPageViewModel : BaseViewModel
{
    [ObservableProperty]
    bool isDarkModeEnabled;

    partial void OnIsDarkModeEnabledChanged(bool value) =>
        ChangeUserAppTheme(value);

    public SettingsPageViewModel()
    {
        isDarkModeEnabled = Settings.Theme == AppTheme.Dark;
    }

    void ChangeUserAppTheme(bool activateDarkMode)
    {
        Settings.Theme = activateDarkMode
            ? AppTheme.Dark
            : AppTheme.Light;

        Theme.SetTheme();
    }
}
