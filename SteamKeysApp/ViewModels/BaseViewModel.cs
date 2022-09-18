namespace SteamKeysApp.ViewModels;

[INotifyPropertyChanged]
public partial class BaseViewModel
{
    public BaseViewModel() { }
        
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string subtitle;

    [ObservableProperty]
    string icon;

    [ObservableProperty]
    bool canLoadMore;

    [ObservableProperty]
    string header;

    [ObservableProperty]
    string footer;
}