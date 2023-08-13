namespace SmartOtp.Views;

public partial class HomeView : ContentPageBase
{
    public HomeView(HomeViewModel homeViewModel)
    {
        BindingContext = homeViewModel;
        InitializeComponent();
    }

    private void MenuItem_OnClicked(object sender, EventArgs e)
    {
        if (SearchHandler.SearchBoxVisibility == SearchBoxVisibility.Hidden)
        {
            SearchHandler.Focus();
            SearchHandler.SearchBoxVisibility = SearchBoxVisibility.Expanded;
        }
        else
        {
            SearchHandler.SearchBoxVisibility = SearchBoxVisibility.Hidden;
        }
    }

    private void SearchHandler_OnUnfocused(object sender, EventArgs e)
    {
        SearchHandler.SearchBoxVisibility = SearchBoxVisibility.Hidden;
    }
}