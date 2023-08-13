namespace SmartOtp.Views;

public class ContentPageBase : ContentPage
{
    public ContentPageBase()
    {
        NavigationPage.SetBackButtonTitle(this, string.Empty);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is not IViewModelBase viewModel)
        {
            return;
        }
        await viewModel.InitializeAsyncCommand.ExecuteAsync(null);
    }
}