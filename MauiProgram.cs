using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using TrailAndTailLogBook.Data;
using TrailAndTailLogBook.Pages;
using TrailAndTailLogBook.ViewModels;
using TrailAndTailLogBook.Services;

#if WINDOWS
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using WinRT.Interop;
using Microsoft.Maui.Platform;
#endif

namespace TrailAndTailLogBook
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(windows =>
                {
                    windows.OnWindowCreated(window =>
                    {
                        // Get native HWND for the current window
                        IntPtr hWnd = WindowNative.GetWindowHandle(window);

                        // Get the WindowId and then the AppWindow
                        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
                        var appWindow = AppWindow.GetFromWindowId(windowId);

                        // Resize to simulate mobile screen (360x640 is common mobile size)
                        appWindow.Resize(new SizeInt32(360, 640));

                        // Make window non-resizable to maintain mobile-like experience
                        if (appWindow.Presenter is OverlappedPresenter presenter)
                        {
                            presenter.IsResizable = false;
                            presenter.IsMaximizable = false;
                        }
                    });
                });
            });
#endif

            // Register DatabaseContext as singleton (one instance for whole app)
            builder.Services.AddSingleton<DatabaseContext>();

            builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
            builder.Services.AddSingleton<IServiceProvider>(sp => sp);

            // Register ViewModels
            builder.Services.AddSingleton<LogEntryViewModel>();
            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<BaseViewModel>();

            // Register Pages
            builder.Services.AddTransient<OnboardingPage>();
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<CreateAccountPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<AddLogPage>();
            builder.Services.AddTransient<EditLogPage>();
            builder.Services.AddTransient<LogEntriesPage>();
            builder.Services.AddTransient<LogSavedPage>();
            builder.Services.AddTransient<LogDeletedPage>();
            builder.Services.AddTransient<SearchPage>();
            builder.Services.AddTransient<LogEntryDetailPage>();
            builder.Services.AddTransient<SaveLogPage>(sp => new SaveLogPage(sp.GetRequiredService<LogEntryViewModel>()));

            return builder.Build();
        }
    }
}