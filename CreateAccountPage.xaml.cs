using TrailAndTailLogBook.ViewModels;

namespace TrailAndTailLogBook.Pages
{
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPage(UserViewModel userViewModel)
        {
            InitializeComponent();
            BindingContext = userViewModel;
        }
    }
}