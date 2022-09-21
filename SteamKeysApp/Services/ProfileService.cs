using System.Net;

namespace SteamKeysApp.Services;

public class ProfileService
{
    private HttpClient _httpClient;
    public int UserId { get; private set; } = -1;

    private string BaseApi(string request) => $"https://st-ag.space/api/Steam/{request}&token=STEAM-REALLY_PARSED-STAG-1304";

    public ProfileService()
    {
        HttpClientHandler clientHandler = new();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        HttpClient client = new(clientHandler);
        _httpClient = new HttpClient(clientHandler);
    }


    /// <returns>If ok - returns User Id. If user exists - bad request.</returns>
    public async Task<int> RegisterUser(string login, string password)
    {
        //https://st-ag.space//api/Steam/AddUser?login=test&password=test&token=STEAM-REALLY_PARSED-STAG-1304

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseApi($"AddUser?login={login}&password={password}"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var id = await responseContent.ReadAsStringAsync();
                return Convert.ToInt32(id);
            }
            else
            {
                await Shell.Current.DisplayAlert("Ooops", $"User with that username already exists!", "Ok");
                return -1;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ooops", $"{ex.Message}", "Ok");
            return -1;
        }
    }

    public async Task<int> VerifyUser(string login, string password)
    {
        // https://st-ag.space/api/Steam/VerifyUser?login=test&password=test&token=STEAM-REALLY_PARSED-STAG-1304

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseApi($"VerifyUser?login={login}&password={password}"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var a = await responseContent.ReadAsStringAsync();
                UserId = Convert.ToInt32(a);
                return Convert.ToInt32(a);
            }
            else
            {
                await Shell.Current.DisplayAlert("Ooops", $"Invalid user login or password!", "Ok");
                return -1;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ooops", $"Invalid user login or password!\n{ex.Message}", "Ok");
            return -1;
        }
    }
}