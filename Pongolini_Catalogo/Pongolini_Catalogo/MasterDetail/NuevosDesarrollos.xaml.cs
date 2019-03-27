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
	public partial class NuevosDesarrollos : ContentPage
	{
		public NuevosDesarrollos ()
		{
			InitializeComponent ();
            CargarPickerNuevosDesarrollos();
		}

        public void CargarPickerNuevosDesarrollos()
        {
            pckTipoProductoNuevDes.Title = "Seleccione producto";
            pckTipoProductoNuevDes.Items.Add("Guías");
            pckTipoProductoNuevDes.Items.Add("Asientos");
        }

        private void btnBuscarNuevDes_Clicked(object sender, EventArgs e)
        {

        }
    }
}