using TrailAndTailLogBook.ViewModels;
using Microsoft.Maui.Controls;

namespace TrailAndTailLogBook.Pages
{
    public partial class LogDetailSearchPage : ContentPage
    {
        public LogDetailSearchPage(LogEntryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Refresh the selected log entry data when page appears
            if (BindingContext is LogEntryViewModel vm)
            {
                vm.OnPropertyChanged(nameof(vm.SelectedLogEntry));
            }
        }
    }
}