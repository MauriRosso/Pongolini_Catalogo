using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using Plugin.Connectivity;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            this.BackgroundImage = "Guias_Asientos_ok.jpg";
            CargarTextoLabels();
            CargarCarrito();
        }

        private void CargarTextoLabels()
        {
            if (App.Idioma == "ES")
            {
                lblBuscarGuiasyAsientosPor.Text = "Buscar Guías y Asientos por";
                lblAplicaciones.Text = "APLICACIONES";
                lblIntercambios.Text = "INTERCAMBIOS";
                lbl300Indy.Text = "#300INDY";
                lblDimensiones.Text = "DIMENSIONES";
                lblConoceNuestrosNuevosProductos.Text = "Conocé nuestros nuevos productos";
                lblSerie6000.Text = "SERIE 6000";
                lblBujes.Text = "BUJES";
            }
            else
            {
                lblBuscarGuiasyAsientosPor.Text = "Look for Guides and Seats for";
                lblAplicaciones.Text = "APPLICATIONS";
                lblIntercambios.Text = "EXCHANGES";
                lbl300Indy.Text = "#300INDY";
                lblDimensiones.Text = "DIMENSIONS";
                lblConoceNuestrosNuevosProductos.Text = "Know our new products";
                lblSerie6000.Text = "SERIES 6000";
                lblBujes.Text = "BUSHINGS";
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

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            DeseaSalir();
            return true;            
        }

        private async void DeseaSalir()
        {
            //Cerrar aplicacion.
            var respuesta = await DisplayAlert("Salir", "¿Está seguro que desea salir de la aplicación?", "SI", "NO");
            if (respuesta == true)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void OnStackAplicaciones_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Aplicaciones());
        }
        private void OnStackIntercambios_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Intercambios());
        }
        private void OnStack300Indy_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new No300Indy());
        }
        private void OnStackDimensiones_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Dimensiones());
        }
        private void OnStackNuevosDesarrollos_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new SERIE6000());
        }
        private void OnStackBujes_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new BujesPage());
        }
        private void OnStackContacto_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Contacto());
        }

        private void imgCarro_Activated(object sender, EventArgs e) //Cuando se presiona el carro de compras
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }
    }
}