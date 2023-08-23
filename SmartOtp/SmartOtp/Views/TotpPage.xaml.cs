using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartOtp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TotpPage : ContentPage
    {
        public TotpPage()
        {
            InitializeComponent();
        }
    }
}