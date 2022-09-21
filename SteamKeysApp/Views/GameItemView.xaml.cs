using System.Windows.Input;
using MvvmHelpers;

namespace SteamKeysApp.Views;

public partial class GameItemView
{
    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(GameItemView),
            true);

    public bool IsLoading
    {
        get { return (bool)GetValue(IsLoadingProperty); }
        set { SetValue(IsLoadingProperty, value); }
    }

    public GameItemView()
    {
        InitializeComponent();
    }

    private void Image_Loaded(object sender, EventArgs e)
    {
        Task.Run(async () =>
        {
            await Task.Delay(2000);
            IsLoading = false;
        });
    }

    private async void Animate(object sender, EventArgs e)
    {
        ImageButton b = (ImageButton)sender;

        await b.ScaleTo(2.0, 300, Easing.BounceIn);
        b.Source = "starred.png"; // = !
        await b.ScaleTo(1.0, 300, Easing.BounceOut);
    }
}