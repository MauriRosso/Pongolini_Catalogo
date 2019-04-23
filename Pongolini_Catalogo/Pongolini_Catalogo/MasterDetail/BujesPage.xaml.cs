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
	public partial class BujesPage : ContentPage
	{
        public BujesPage()
        {
            InitializeComponent();
            CargarTipoBujes();
        }

        protected override bool OnBackButtonPressed()
        {
            App.MasterD.Detail = new NavigationPage(new Inicio());
            return true;
        }

        public void CargarTipoBujes()
        {
            pckTipoBujes.Title = "Seleccione tipo";
            pckTipoBujes.Items.Add("Fundición gris aleada");
            pckTipoBujes.Items.Add("Templado");
        }
    }
}