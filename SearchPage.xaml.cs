namespace TrailAndTailLogBook.Pages;
using TrailAndTailLogBook.ViewModels;

public partial class SearchPage : ContentPage
{
    public SearchPage(LogEntryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}