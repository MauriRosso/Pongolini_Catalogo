using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace Pongolini_Catalogo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupModalSERIE6000 : PopupPage
    {
		public PopupModalSERIE6000 ()
		{
			InitializeComponent ();
            if (App.Idioma != "ES")
            {
                lblPopUp.Text = "The 6000 line is the ideal solution for the repairer of cylinder heads with oversized problems. They are built in high alloy steels of chromium, nickel and molybdenum. Thermal treatments that obtain a final hardness of 38-44 HRC.";
            }
		}

        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}