using Microsoft.Maui.LifecycleEvents;
using SteamKeysApp.Services;

namespace SteamKeysApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureEssentials()
                .ConfigureServices()
                .ConfigurePages()
                .ConfigureViewModels()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("SFProDisplay-Thin.ttf", "SFProDisplayThin");
                    fonts.AddFont("SFProDisplay-Regular.ttf", "SFProDisplayRegular");
                    fonts.AddFont("SFProDisplay-Semibold.ttf", "SFProDisplaySemibold");
                    fonts.AddFont("SFProDisplay-Bold.ttf", "SFProDisplayBold");
                });

////#if WINDOWS
//            builder.ConfigureLifecycleEvents(events =>
//            {
//                events.AddWindows(wndLifeCycleBuilder =>
//                {
//                    wndLifeCycleBuilder.OnWindowCreated(window =>
//                    {
//                        window.ExtendsContentIntoTitleBar = false; /*This is important to prevent your app content extends into the title bar area.*/
//                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
//                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
//                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
//                        if(winuiAppWindow.Presenter is OverlappedPresenter p)
//                        {
//                            p.SetBorderAndTitleBar(false, false);
//                        }
//const int width = 1200;
//                        const int height = 800;
///*I suggest you to use MoveAndResize instead of Resize because this way you make sure to center the window*/
//                        winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
//                    });
//                });
//            });
////#endif

            return builder.Build();
        }
    }
}