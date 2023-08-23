using SmartOtp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartOtp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}