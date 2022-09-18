using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace SteamKeysApp.Services;

public class GamesService
{
    List<Game> games = new List<Game>();
    HttpClient httpClient;

    public GamesService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Game>> GetGames()
    {
        if (games.Count > 0)
            return games;

        //string url = "https://montemagno.com/monkeys.json";
        //var response = await httpClient.GetAsync(url);
        //if(response.IsSuccessStatusCode)
        //{
        //    games = await response.Content.ReadFromJsonAsync<List<Game>>();
        //}

        using var stream = await FileSystem.OpenAppPackageFileAsync("10games.json");
        using var reader = new StreamReader(stream);
        var contents = reader.ReadToEnd();
        return JsonSerializer.Deserialize<List<Game>>(contents);
    }


    public async Task<GameInfo> GetGameInfo(int id)
    {
        string url = $"https://store.steampowered.com/api/appdetails?appids={id}";
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Regex regex = new Regex($"{id}");
            string result = regex.Replace(responseBody, "GameInfo", 1);
            var jobject = JObject.Parse(result);
            Root root = jobject.ToObject<Root>();

            return root.GameInfo;
        }
        else
            return null;

        //using var stream = await FileSystem.OpenAppPackageFileAsync("game.json");
        //using var reader = new StreamReader(stream);
        //var contents = reader.ReadToEnd();

        //var result = JObject.Parse(contents);
        //Root gggg = result.ToObject<Root>();

        //return gggg.GameInfo;
    }


    public async Task<IEnumerable<Game>> SearchGamesAsync(string term)
    {
        //var showsResponse = await TryGetAsync<IEnumerable<GameResponse>>($"shows?limit=10&term={term}");

        //return showsResponse?.Select(response => GetGame(response));

        return null;
    }

}
