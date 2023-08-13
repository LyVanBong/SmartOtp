using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOtp.Views;

public partial class CreateQrCodeView : ContentPageBase
{
    public CreateQrCodeView(CreateQrCodeViewModel createQrCodeViewModel)
    {
        BindingContext = createQrCodeViewModel;
        InitializeComponent();
    }
}