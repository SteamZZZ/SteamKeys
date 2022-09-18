namespace SteamKeysApp.Services;

public class SubscriptionsService
{
    private List<Game> subscribedShows;

    public SubscriptionsService()
    {
        this.subscribedShows = new List<Game>();
    }

    public IEnumerable<Game> GetSubscribedShows()
    {
        return this.subscribedShows;
    }

    public void SubscribeToShow(Game show)
    {
        if (show == null)
            return;

        SemanticScreenReader.Announce(string.Format("Subscribe to show {0}", show.Name));
        this.subscribedShows.Add(show);
    }

    public async Task<bool> UnSubscribeFromShowAsync(Game podcast)
    {
        var isUnsubcribed = false;
        var userWantUnsubscribe = await App.Current.MainPage.DisplayAlert(
                    $"Do you want to unsubscribe from {podcast.Name} ?",
                    string.Empty,
                    "Yes, unsubcribe",
                    "Cancel");

        if (userWantUnsubscribe)
        {
            var showToRemove = this.subscribedShows
                .FirstOrDefault(p => p.SteamId == podcast.SteamId);
            if (showToRemove != null)
            {
                this.subscribedShows.Remove(showToRemove);
                isUnsubcribed = true;
            }
        }

        return isUnsubcribed;
    }

    internal bool IsSubscribed(int id)
    {
        return this.subscribedShows.Any(p => p.SteamId == id);
    }
}
