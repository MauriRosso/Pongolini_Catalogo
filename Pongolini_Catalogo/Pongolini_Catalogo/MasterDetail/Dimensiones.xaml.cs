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
        }

    }
}