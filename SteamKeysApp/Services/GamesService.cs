using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SteamKeysApp.Services;

public class GamesService
{
    private HttpClient _httpClient;
    private string SteamApi(string request) => $"https://store.steampowered.com/api/{request}";
    private string BaseApi(string request) => $"https://st-ag.space/api/Steam/{request}&token=STEAM-REALLY_PARSED-STAG-1304";

    public GamesService()
    {
        HttpClientHandler clientHandler = new();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        HttpClient client = new(clientHandler);
        _httpClient = new HttpClient(clientHandler);
    }

    public async Task<IEnumerable<Game>> LoadGames(int start, int count)
        => await GetGames($"JsonData?start={start}&count={count}");

    public async Task AddToFavorites(int userId, int gameId)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseApi($"AddToFaforites?{userId}=1&{gameId}=10"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var id = await responseContent.ReadAsStringAsync();
                switch (Convert.ToInt32(id))
                {
                    case 0: await Shell.Current.DisplayAlert("Yo!", $"Added to favirites!", "Ok"); break;
                    case 1: await Shell.Current.DisplayAlert("Yo!", $"Already in favorites!", "Ok"); break;
                    case 2: await Shell.Current.DisplayAlert("Ooops", $"No such game or user!", "Ok"); break;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ooops", $"{ex.Message}", "Ok");
        }
    }

    public async Task RemoveFromFavorites(int userId, int gameId)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(BaseApi($"RemoveFromFaforites?{userId}=1&{gameId}=10"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var id = await responseContent.ReadAsStringAsync();
                switch (Convert.ToInt32(id))
                {
                    case 0: await Shell.Current.DisplayAlert("Yo!", $"Added to favirites!", "Ok"); break;
                    case 1: await Shell.Current.DisplayAlert("Yo!", $"Already in favorites!", "Ok"); break;
                    case 2: await Shell.Current.DisplayAlert("Ooops", $"No such game or user!", "Ok"); break;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ooops", $"{ex.Message}", "Ok");
        }
    }

    public async Task<IEnumerable<Game>> GetFavorites(int id)
        => await GetGames($"GetFavorites?user_id={id}");

    public async Task<IEnumerable<Game>> SearchGamesAsync(string term)
        => await GetGames($"JsonDataByName?name={term}");

    public async Task<GameInfo> GetGameInfo(int id)
    {
#if true
        var response = await _httpClient.GetAsync(SteamApi($"appdetails?appids={id}"));

        try
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Regex regex = new Regex($"{id}");
            string result = regex.Replace(responseBody, "GameInfo", 1);
            var jobject = JObject.Parse(result);
            Root root = jobject.ToObject<Root>();

            if (root.GameInfo.Success == false) throw new Exception();

            return root.GameInfo;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ooops", $"Failed to load game info!\n{ex.Message}", "Ok");
            return null; // ??????????
        }
#else

        using var stream = await FileSystem.OpenAppPackageFileAsync("game.json");
        using var reader = new StreamReader(stream);
        var contents = reader.ReadToEnd();

        var result = JObject.Parse(contents);
        Root gggg = result.ToObject<Root>();

        return gggg.GameInfo;
#endif
    }

    private async Task<IEnumerable<Game>> GetGames(string request)
    {
#if true
        try
        {
            using Stream s = _httpClient.GetStreamAsync(BaseApi(request)).Result;
            using StreamReader sr = new StreamReader(s);
            using JsonReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();
            var games = serializer.Deserialize<List<Game>>(reader);

            if (games == null) throw new Exception();

            return games;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ooops", $"Failed to get games by request:\n{BaseApi(request)}!\n\n{ex.Message}", "Ok");
            return null;
        }
#else
        using var stream = await FileSystem.OpenAppPackageFileAsync("10games.json");
        using var reader = new StreamReader(stream);
        JArray result = (JArray)JToken.ReadFrom(new JsonTextReader(reader));

        return result.ToObject<List<Game>>();
#endif
    }


    //    private async Task<T> TryGetImplementationAsync<T>(string path)
    //    {
    //        var json = string.Empty;
    //
    //#if !MACCATALYST
    //        if (Connectivity.NetworkAccess == NetworkAccess.None)
    //            json = Barrel.Current.Get<string>(path);
    //#endif
    //        if (!Barrel.Current.IsExpired(path))
    //            json = Barrel.Current.Get<string>(path);
    //
    //        T responseData = default;
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(json))
    //            {
    //                var response = await httpClient.GetAsync(path);
    //                if (response.IsSuccessStatusCode)
    //                {
    //                    responseData = await response.Content.ReadFromJsonAsync<T>();
    //                }
    //            }
    //            else
    //            {
    //                responseData = JsonSerializer.Deserialize<T>(json);
    //            }
    //
    //            if (responseData != null)
    //                Barrel.Current.Add(path, responseData, TimeSpan.FromMinutes(10));
    //        }
    //        catch (Exception ex)
    //        {
    //            System.Diagnostics.Debug.WriteLine(ex);
    //        }
    //
    //        return responseData;
    //    }
}
