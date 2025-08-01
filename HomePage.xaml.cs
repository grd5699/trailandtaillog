namespace TrailAndTailLogBook.Pages;

using System.Diagnostics;
using TrailAndTailLogBook.ViewModels;
using TrailAndTailLogBook.Services;
public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Debug.WriteLine("HomePage initialized with ViewModel");
    }
}