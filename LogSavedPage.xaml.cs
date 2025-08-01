namespace TrailAndTailLogBook.Pages
{
    public partial class LogSavedPage : ContentPage
    {
        public LogSavedPage()
        {
            InitializeComponent();
        }

        private async void OnOkClicked(object sender, EventArgs e)
        {
            // Navigate directly to HomePage using absolute route
            await Shell.Current.GoToAsync("//HomePage");

            // Alternative if you want to clear navigation stack:
            // await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}