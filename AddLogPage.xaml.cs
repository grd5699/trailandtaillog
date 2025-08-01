using TrailAndTailLogBook.ViewModels;
using Microsoft.Maui.Controls;

namespace TrailAndTailLogBook.Pages
{
    public partial class AddLogPage : ContentPage
    {
        public AddLogPage(LogEntryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}