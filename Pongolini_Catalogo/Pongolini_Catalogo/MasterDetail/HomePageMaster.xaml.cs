using Pongolini_Catalogo.Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;
        string PopSalirTitulo;
        string PopSalirContenido;
        string PopSalirSi;
        string PopSalirNo;

        public HomePageMaster()
        {
            InitializeComponent();
            CargarTextoLabels();
        }      

        private void CargarTextoLabels()
        {
            if (App.Idioma == "ES")
            {
                lblInicio.Text = "Inicio";
                lblBuscarPorGuiasAsientos.Text = "Buscar guías y asientos de válvulas por";
                lblAplicaciones.Text = "Aplicaciones";
                lblIntercambios.Text = "Intercambios";
                lbl300Indy.Text = "#300INDY";
                lblDimensiones.Text = "Dimensiones";
                lblNuevosDesarrollos.Text = "SERIE 6000";
                lblConoceNuestrosProductos.Text = "Conocé nuestros nuevos productos";
                lblBujes.Text = "Bujes de fundición";
                lblMisPedidos.Text = "Mis pedidos";
                lblIrAlSitio.Text = "Ir al sitio institucional";
                lblContacto.Text = "Contacto";
                lblSalir.Text = "Salir";
                PopSalirTitulo = "Salir";
                PopSalirContenido = "¿Está seguro que desea salir de la aplicación?";
                PopSalirSi = "Sí";
                PopSalirNo = "No";
            }
            else
            {
                lblInicio.Text = "Home";
                lblBuscarPorGuiasAsientos.Text = "Look for valve guides and seats for";
                lblAplicaciones.Text = "Applications";
                lblIntercambios.Text = "Exchanges";
                lbl300Indy.Text = "#300INDY";
                lblDimensiones.Text = "Dimensions";
                lblNuevosDesarrollos.Text = "SERIES 6000";
                lblConoceNuestrosProductos.Text = "Know our new products";
                lblBujes.Text = "Casting bushings";
                lblMisPedidos.Text = "My orders";
                lblIrAlSitio.Text = "Go to the institutional site";
                lblContacto.Text = "Contact";
                lblSalir.Text = "Exit";
                PopSalirTitulo = "Exit";
                PopSalirContenido = "Are you sure you want to exit the application?";
                PopSalirSi = "Yes";
                PopSalirNo = "No";
            }
        }

        private void OnStackInicio_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Inicio());
            App.MasterD.IsPresented = false;
        }
        private void OnStackAplicaciones_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Aplicaciones());
            App.MasterD.IsPresented = false;
        }
        private void OnStackIntercambios_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Intercambios());
            App.MasterD.IsPresented = false;
        }
        private void OnStack300Indy_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new No300Indy());
            App.MasterD.IsPresented = false;
        }
        private void OnStackDimensiones_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Dimensiones());
            App.MasterD.IsPresented = false;
        }
        private void OnStackNuevosDesarrollos_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new SERIE6000());
            App.MasterD.IsPresented = false;
        }
        private void OnStackBujes_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new BujesPage());
            App.MasterD.IsPresented = false;
        }
        private void OnStackContacto_Tapped(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new Contacto());
            App.MasterD.IsPresented = false;
        }
        private async void OnStackSalir_Tapped(object sender, EventArgs e)
        {
            //Cerrar aplicacion.
            var respuesta = await DisplayAlert(PopSalirTitulo, PopSalirContenido, PopSalirSi, PopSalirNo);
            if (respuesta == true)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        private void OnStackIrAlSitio_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://pongolini.com/"));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Mis pedidos
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
            App.MasterD.IsPresented = false;
        }
    }
}