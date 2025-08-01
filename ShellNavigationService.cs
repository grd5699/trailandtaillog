using TrailAndTailLogBook.Pages;
using TrailAndTailLogBook.ViewModels;
using TrailAndTailLogBook.Services;
using System.Diagnostics;

public class ShellNavigationService : INavigationService
{
    public async Task GoToAsync(string route)
    {
        try
        {
            Debug.WriteLine($"Attempting to navigate to: {route}");

            // Use Shell's navigation system
            await Shell.Current.GoToAsync(route);

            Debug.WriteLine($"Successfully navigated to: {route}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation Error to {route}: {ex}");
            throw;
        }
    }
}