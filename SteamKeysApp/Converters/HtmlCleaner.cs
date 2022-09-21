using System.Text.RegularExpressions;

namespace SteamKeysApp.Converters;

public static class HtmlCleaner
{
    public static string Clean(string html)
    {
        html = Regex.Replace(html, @"<br><br>", "<br>");
        html = Regex.Replace(html, @"<br></li>", @"</li>");
        html = Regex.Replace(html, @"<br><ul", @"<ul");

        return html;
    }
}
