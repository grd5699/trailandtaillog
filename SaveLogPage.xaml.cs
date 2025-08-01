using TrailAndTailLogBook.ViewModels;

namespace TrailAndTailLogBook.Pages
{
    public partial class SaveLogPage : ContentPage
    {
        public SaveLogPage(LogEntryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}