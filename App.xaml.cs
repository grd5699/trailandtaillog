namespace TrailAndTailLogBook;
using TrailAndTailLogBook.Pages;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Make sure to set MainPage to your Shell
        MainPage = new AppShell();

        // Optional: If you want OnboardingPage first
        Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
    }
}