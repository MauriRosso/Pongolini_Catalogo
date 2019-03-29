using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleAplicacion : ContentPage
	{
		public DetalleAplicacion (Guias guia, Asientos asiento)
		{
			InitializeComponent ();
		}
	}
}