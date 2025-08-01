using TrailAndTailLogBook.ViewModels;
using Microsoft.Maui.Controls;

namespace TrailAndTailLogBook.Pages
{
    public partial class LogInPage : ContentPage
    {
        public LogInPage(UserViewModel userViewModel)
        {
            InitializeComponent();
            BindingContext = userViewModel;
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            try
            {
                // Option 1: Use the Navigation property
                await Navigation.PushAsync(new CreateAccountPage(BindingContext as UserViewModel));

                // OR Option 2: Safe Shell navigation
                if (Application.Current?.MainPage is Shell shell)
                {
                    await shell.GoToAsync(nameof(CreateAccountPage));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation error: {ex}");
                await DisplayAlert("Error", "Couldn't open sign-up page", "OK");
            }
        }
    }
}