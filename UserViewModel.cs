using TrailAndTailLogBook.Models;
using TrailAndTailLogBook.Data;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TrailAndTailLogBook.Pages;

namespace TrailAndTailLogBook.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly DatabaseContext _database;
        private readonly IServiceProvider _services;

        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand CreateAccountCommand { get; }
        public ICommand LoginCommand { get; }

        public UserViewModel(DatabaseContext database, IServiceProvider services)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _services = services ?? throw new ArgumentNullException(nameof(services));

            CreateAccountCommand = new Command(async () => await CreateAccount());
            LoginCommand = new Command(async () => await AuthenticateUser());
        }

        private async Task NavigateToHome()
        {
            try
            {
                // Option 1: Use the service provider to get Shell
                if (Application.Current?.MainPage is Shell shell)
                {
                    await shell.GoToAsync("//HomePage");
                }
                else
                {
                    // Option 2: Fallback navigation
                    Application.Current.MainPage = _services.GetService<HomePage>();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task<bool> AuthenticateUser()
        {
            try
            {
                var userExists = _database.Users.Any(u => u.Email == Email && u.Password == Password);
                if (userExists)
                {
                    await NavigateToHome();
                }
                return userExists;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public async Task<bool> CreateAccount()
        {
            try
            {
                if (_database.Users.Any(u => u.Email == Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Email already exists", "OK");
                    return false;
                }

                _database.Users.Add(new User
                {
                    Email = Email,
                    Password = Password,
                    Username = Username
                });

                await _database.SaveChangesAsync();
                await NavigateToHome();
                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }
    }
}