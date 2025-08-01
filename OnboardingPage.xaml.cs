using TrailAndTailLogBook.ViewModels;
using TrailAndTailLogBook.Data;
using Microsoft.Extensions.DependencyInjection;

namespace TrailAndTailLogBook.Pages
{
    public partial class OnboardingPage : ContentPage
    {
        public OnboardingPage()
        {
            InitializeComponent();
        }

        private async void OnGetStartedClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the required services
                var serviceProvider = Handler.MauiContext.Services;

                // Resolve both dependencies
                var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();
                var services = serviceProvider.GetRequiredService<IServiceProvider>();

                // Create ViewModel with both parameters
                var userViewModel = new UserViewModel(databaseContext, services);

                // Navigate to LogInPage
                await Navigation.PushAsync(new LogInPage(userViewModel));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Could not navigate to login page", "OK");
                Console.WriteLine($"Navigation error: {ex}");

                // Emergency fallback using basic constructor
                Application.Current.MainPage = new NavigationPage(
                    new LogInPage(new UserViewModel(new DatabaseContext(),
                        Handler.MauiContext.Services)));
            }
        }
    }
}