namespace TrailAndTailLogBook.Pages;
using TrailAndTailLogBook.ViewModels;
public partial class LogEntriesPage : ContentPage
{
    public LogEntriesPage(LogEntryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is LogEntryViewModel vm)
        {
            vm.LoadLogEntries();
        }
    }
}