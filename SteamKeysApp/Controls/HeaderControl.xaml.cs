using SteamKeysApp.Pages;
using System.Windows.Input;

namespace SteamKeysApp.Controls;

public partial class HeaderControl : ContentView
{
    public static readonly BindableProperty SearchCommandProperty =
        BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(HeaderControl),
            null);

    public static readonly BindableProperty TextToSearchProperty =
        BindableProperty.Create(
            nameof(TextToSearch),
            typeof(string),
            typeof(HeaderControl),
            string.Empty);

    public ICommand SearchCommand
    {
        get { return (ICommand)GetValue(SearchCommandProperty); }
        set { SetValue(SearchCommandProperty, value); }
    }

    public string TextToSearch
    {
        get { return (string)GetValue(TextToSearchProperty); }
        set { SetValue(TextToSearchProperty, value); }
    }

    public HeaderControl()
    {
        AutomationProperties.SetIsInAccessibleTree(this, true);
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(GamesCatalogPage)}");
    }
}