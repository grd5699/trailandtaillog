namespace TrailAndTailLogBook.Services
{
    public interface INavigationService
    {
        Task GoToAsync(string route);
    }
}