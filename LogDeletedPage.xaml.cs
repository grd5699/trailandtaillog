namespace TrailAndTailLogBook.Pages
{
    public partial class LogDeletedPage : ContentPage
    {
        public LogDeletedPage()
        {
            InitializeComponent();
        }

        private async void OnBackToHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}