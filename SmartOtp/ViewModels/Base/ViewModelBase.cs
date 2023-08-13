namespace SmartOtp.ViewModels.Base;

public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
{
    private long _isBusy;
    [ObservableProperty]
    private bool _isInitialized;
    private readonly INavigationService _navigationService;
    private readonly IAsyncRelayCommand _initializeAsyncCommand;
    private string _title;
    public bool IsBusy => Interlocked.Read(ref _isBusy) > 0;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public INavigationService NavigationService => _navigationService;

    public IAsyncRelayCommand InitializeAsyncCommand => _initializeAsyncCommand;

    public ViewModelBase(INavigationService navigationService)
    {
        _navigationService = navigationService;

        _initializeAsyncCommand =
            new AsyncRelayCommand(
                async () =>
                {
                    await IsBusyFor(InitializeAsync);
                    IsInitialized = true;
                },
                AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task IsBusyFor(Func<Task> unitOfWork)
    {
        Interlocked.Increment(ref _isBusy);
        OnPropertyChanged(nameof(IsBusy));

        try
        {
            await unitOfWork();
        }
        finally
        {
            Interlocked.Decrement(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));
        }
    }
}