using TrailAndTailLogBook.ViewModels;

namespace TrailAndTailLogBook.Pages
{
    public partial class DeleteLogPage : ContentPage
    {
        public DeleteLogPage(LogEntryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}