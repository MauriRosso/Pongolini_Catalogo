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
            CargarTextoLabels();
            CargarCarrito();
        }

        private void CargarTextoLabels()
        {
            if (App.Idioma != "ES")
            {
                Title = "Product detail";
                lblDetalle.Text = "DETAIL";
                lblTipoProducto.Text = "BUSHING";
                lblAtributos.Text = "ATTRIBUTES";
                lblDiamIntGrilla.Text = "Ø Inside (mm)";
                lblLargoGrilla.Text = "Long (mm)";
                lblTipoAplicacion.Text = "Application type";
                lblDureza.Text = "Hardness";
                btnRealizarPedido.Text = "ADD TO CART";
            }
        }

        private void CargarCarrito()
        {
            int cant_prod = 0;
            foreach (CarroViewModel item in App.ListaGlobalProductos)
            {
                cant_prod += item.cantidad;
            }
            ToolbarItem cantidadCarro = new ToolbarItem
            {
                Text = "(" + cant_prod + ")",

            };
            ToolbarItems.Add(cantidadCarro);
        }

        private async void btnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            //Se agrega el producto al carrito
            await PopupNavigation.Instance.PushAsync(new PopupModal(lblTipoProducto.Text, lblCodigo.Text, lbllargo.Text, lbldiamext.Text, lbldiamint.Text)); //Le pido la cantidad             
        }

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }
    }
}