namespace SmartOtp.ViewModels.Base;

public interface IViewModelBase : IQueryAttributable
{
    public string Title { get; set; }
    public INavigationService NavigationService { get; }

    public IAsyncRelayCommand InitializeAsyncCommand { get; }

    public bool IsBusy { get; }

    public bool IsInitialized { get; }

    Task InitializeAsync();
}