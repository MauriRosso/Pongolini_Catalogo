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

        public HomePageMaster()
        {
            InitializeComponent();
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
            var respuesta = await DisplayAlert("Salir", "¿Está seguro que desea salir de la aplicación?", "SI", "NO");
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