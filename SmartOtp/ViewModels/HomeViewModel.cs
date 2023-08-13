namespace SmartOtp.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private string _searchText;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public HomeViewModel(INavigationService navigationService) : base(navigationService)
    {
        Title = "Home";
    }
}