using TrailAndTailLogBook.ViewModels;
using Microsoft.Maui.Controls;

namespace TrailAndTailLogBook.Pages
{
    public partial class LogEntryDetailPage : ContentPage
    {
        public LogEntryDetailPage(LogEntryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Ensure the viewmodel has the latest data
            if (BindingContext is LogEntryViewModel vm)
            {
                vm.OnPropertyChanged(nameof(vm.SelectedLogEntry));
            }
        }
    }
}