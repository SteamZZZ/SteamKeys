namespace SteamKeysApp.Helpers;

public static class Theme
{
    public static void SetTheme()
    {
        switch (Settings.Theme)
        {
            default:
            case AppTheme.Light:
                App.Current.UserAppTheme = AppTheme.Light;
                break;
            case AppTheme.Dark:
                App.Current.UserAppTheme = AppTheme.Dark;
                break;

        }

        MessagingCenter.Instance.Send<string>("SteamKeysApp", "ChangeWebTheme");
    }
}
