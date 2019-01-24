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
	public partial class Aplicaciones : ContentPage
	{
        List<Guias_Asientos> ListaDatos = new List<Guias_Asientos>();
        List<Guias_Asientos> ListaAux = new List<Guias_Asientos>();

        public Aplicaciones ()
		{
			InitializeComponent ();
            CargarPickerProducto();
            CargarPickerMarca();
            CargarPickerTipoAplicacion();
            CargarListaAux();
            btnNuevaBusqueda.IsVisible = false;
        }

        public void CargarPickerMarca()
        {
            pckMarca.Items.Add("[ Todos ]");
            pckMarca.Items.Add("Marca 1");
            pckMarca.Items.Add("Marca 2");
            pckMarca.SelectedIndex = 0;
        }
        public void CargarPickerProducto()
        {
            pckProducto.Items.Add("Guías");
            pckProducto.Items.Add("Asientos");
            pckProducto.SelectedIndex = 0;
        }

        public void CargarPickerTipoAplicacion()
        {
            pckTipoAplicacion.Items.Add("[ Todos ]");
            pckTipoAplicacion.Items.Add("Motor de automóvil");
            pckTipoAplicacion.Items.Add("Motor de motocicleta");
            pckTipoAplicacion.Items.Add("Motor de camión");
            pckTipoAplicacion.SelectedIndex = 0;
        }

        public void CargarListaAux()
        {
           
        }

        public void OcultarCamposAplicaciones()
        {
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = false;
            //Reactivo el button de nueva busqueda.
            btnNuevaBusqueda.IsVisible = true;

        }

        private void btnBuscarAplicaciones_Clicked(object sender, EventArgs e)
        {
            //Oculto la busqueda para mostrar con mas espacio la listview
            OcultarCamposAplicaciones();

            ListaDatos.Clear();
            //filtros
           

            ListViewAplicaciones.ItemsSource = null;
            ListViewAplicaciones.ItemsSource = ListaDatos;

        }

        private void btnNuevaBusqueda_Clicked(object sender, EventArgs e)
        {
            btnNuevaBusqueda.IsVisible = false;
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = true;
        }
    }
}