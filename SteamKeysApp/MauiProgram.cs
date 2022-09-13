namespace SteamKeysApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("SFProDisplay-Thin.ttf", "SFProDisplayThin");
                    fonts.AddFont("SFProDisplay-Regular.ttf", "SFProDisplayRegular");
                    fonts.AddFont("SFProDisplay-Semibold.ttf", "SFProDisplaySemibold");
                    fonts.AddFont("SFProDisplay-Bold.ttf", "SFProDisplayBold");

                });

            return builder.Build();
        }
    }
}