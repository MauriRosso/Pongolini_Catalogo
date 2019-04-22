using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{
		public Inicio ()
		{
			InitializeComponent ();
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
    }
}