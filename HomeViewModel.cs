using System.Windows.Input;
using TrailAndTailLogBook.Pages;
using TrailAndTailLogBook.Services;
using TrailAndTailLogBook.ViewModels;

namespace TrailAndTailLogBook.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand NavigateToAddLogCommand { get; }
        public ICommand NavigateToLogEntriesCommand { get; }
        public ICommand NavigateToSearchCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToAddLogCommand = new Command(async () =>
            {
                Console.WriteLine("Add Log button clicked");
                try
                {
                    await _navigationService.GoToAsync(nameof(AddLogPage));
                    Console.WriteLine("Navigation attempted to AddLogPage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Navigation error: {ex.Message}");
                }
            });

            NavigateToLogEntriesCommand = new Command(async () =>
            {
                Console.WriteLine("Log Entries button clicked");
                try
                {
                    await _navigationService.GoToAsync($"//{nameof(LogEntriesPage)}");
                    Console.WriteLine("Navigation attempted to LogEntriesPage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Navigation error: {ex.Message}");
                }
            });

            NavigateToSearchCommand = new Command(async () =>
            {
                Console.WriteLine("Search button clicked");
                try
                {
                    await _navigationService.GoToAsync($"//{nameof(SearchPage)}");
                    Console.WriteLine("Navigation attempted to SearchPage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Navigation error: {ex.Message}");
                }
            });
        }
    }
}