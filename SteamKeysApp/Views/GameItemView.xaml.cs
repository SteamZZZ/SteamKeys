using System.Windows.Input;
using MvvmHelpers;

namespace SteamKeysApp.Views;

public partial class GameItemView
{
    public static readonly BindableProperty SubscriptionCommandProperty =
        BindableProperty.Create(
            nameof(SubscriptionCommand),
            typeof(ICommand),
            typeof(GameItemView),
            default(string));

    public static readonly BindableProperty SubscriptionCommandParameterProperty =
        BindableProperty.Create(
            nameof(SubscriptionCommandParameter),
            typeof(GameItemView),
            typeof(GameItemView),
            default(GameItemView));

    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(GameItemView),
            true);

    public ICommand SubscriptionCommand
    {
        get { return (ICommand)GetValue(SubscriptionCommandProperty); }
        set { SetValue(SubscriptionCommandProperty, value); }
    }

    public GameItemView SubscriptionCommandParameter
    {
        get { return (GameItemView)GetValue(SubscriptionCommandParameterProperty); }
        set { SetValue(SubscriptionCommandParameterProperty, value); }
    }

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
}