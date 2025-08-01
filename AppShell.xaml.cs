using System.Diagnostics;
using TrailAndTailLogBook.Pages;

namespace TrailAndTailLogBook
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            
            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
            Routing.RegisterRoute(nameof(CreateAccountPage), typeof(CreateAccountPage));
            Routing.RegisterRoute(nameof(AddLogPage), typeof(AddLogPage));
            Routing.RegisterRoute(nameof(EditLogPage), typeof(EditLogPage));
            Routing.RegisterRoute(nameof(LogSavedPage), typeof(LogSavedPage)); 
            Routing.RegisterRoute(nameof(LogDeletedPage), typeof(LogDeletedPage)); 
            Routing.RegisterRoute(nameof(SaveLogPage), typeof(SaveLogPage)); 
            Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LogEntriesPage), typeof(LogEntriesPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(LogEntryDetailPage), typeof(LogEntryDetailPage));
            Routing.RegisterRoute(nameof(LogDetailSearchPage), typeof(LogDetailSearchPage));

            Debug.WriteLine("All routes registered in AppShell");

        }
    }
}