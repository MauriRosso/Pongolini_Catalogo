using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.Negocio;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleIntercambios : ContentPage
	{
		public DetalleIntercambios (Guias_Asientos producto)
		{
            InitializeComponent ();
            BindingContext = producto;
		}
	}
}