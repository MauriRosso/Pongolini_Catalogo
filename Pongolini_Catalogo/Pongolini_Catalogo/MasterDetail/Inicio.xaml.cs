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
            CargarCarrito();
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