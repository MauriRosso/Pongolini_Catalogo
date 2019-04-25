using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Negocio;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleBuje : ContentPage
	{
        List<AplicacionesViewModel> ListaDatos_Final = new List<AplicacionesViewModel>(); //Informacion que se muestra en la listview.

        public DetalleBuje (Bujes buje)
		{
			InitializeComponent ();
            ListaDatos_Final.Clear();
            ///ATRIBUTOS
            lblCodigo.Text = buje.codigo;
            lblTipoProducto.Text = "BUJE DE FUNDICIÓN";
            lbldiamext.Text = buje.diametro_exterior.ToString();
            lbldiamint.Text = buje.diametro_interior.ToString();
            lbllargo.Text = buje.largo.ToString();
            lblmat.Text = (buje.codigo[1].ToString() == "T") ? "Templado" : "Gris aleada";
            lbltipoapl.Text = buje.codigo[1].ToString() == "T" ? "ENG. DIESEL-GNC" : "ENG. NAFTERO";
            lbldur.Text = buje.codigo[1].ToString() == "T" ? "310-350 HB" : "200-240 HB";
        }

        private async void btnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupModal(lblTipoProducto.Text, lblCodigo.Text));
        }
    }
}