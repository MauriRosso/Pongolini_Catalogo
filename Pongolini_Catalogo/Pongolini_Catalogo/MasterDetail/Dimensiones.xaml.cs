using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dimensiones : ContentPage
	{
		public Dimensiones ()
		{
			InitializeComponent ();
            CargarPickerProducto();
		}

        public void CargarPickerProducto()
        {
            pckProductoDim.Items.Add("Guías");
            pckProductoDim.Items.Add("Asientos");
            pckProductoDim.SelectedIndex = 0;
            lblAnguloDim.IsVisible = false;
            txtAnguloDim.IsVisible = false;
            lblAltoDim.IsVisible = false;
            txtAltoDim.IsVisible = false;
        }

        private void pckProductoDim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pckProductoDim.SelectedIndex == 0) //Selecciono Guias
            {
                lblFormaDim.IsVisible = true;
                txtFormaDim.IsVisible = true;
                lblLargoDim.IsVisible = true;
                txtLargoDim.IsVisible = true;

                lblAnguloDim.IsVisible = false;
                txtAnguloDim.IsVisible = false;
                lblAltoDim.IsVisible = false;
                txtAltoDim.IsVisible = false;
            }
            else //Selecciono Asientos
            {
                if (pckProductoDim.SelectedIndex == 1)
                {
                    lblFormaDim.IsVisible = false;
                    txtFormaDim.IsVisible = false;
                    lblLargoDim.IsVisible = false;
                    txtLargoDim.IsVisible = false;

                    lblAnguloDim.IsVisible = true;
                    txtAnguloDim.IsVisible = true;
                    lblAltoDim.IsVisible = true;
                    txtAltoDim.IsVisible = true;
                }
            }
        }
    }
}