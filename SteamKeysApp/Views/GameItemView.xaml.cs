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

    private void Starred(object sender, EventArgs e)
    {
        var ib = (ImageButton)sender;
  //      ib.Source = ImageSource.FromFile("Images/starred.png" : "Images/unstarred.png");
    }

    private async void Animate(object sender, EventArgs e)
    {
        ImageButton b = (ImageButton)sender;

        await b.ScaleTo(2.0, 300, Easing.BounceIn);
        b.Source = "starred.png"; // = !
        await b.ScaleTo(1.0, 300, Easing.BounceOut);
    }
}