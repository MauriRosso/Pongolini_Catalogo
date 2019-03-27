using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Negocio;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Aplicaciones : ContentPage
	{
        List<Guias> ListaDatos = new List<Guias>();
        List<Guias> ListaAux = new List<Guias>();

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
            pckMarca.Items.Add("AGRALE");
            pckMarca.Items.Add("ALCO");
            pckMarca.Items.Add("ALFA ROMEO");
            pckMarca.Items.Add("AUDI");
            pckMarca.Items.Add("AUSTIN");
            pckMarca.Items.Add("BEDFORD");
            pckMarca.Items.Add("BERLIET");
            pckMarca.Items.Add("BIELARUS");
            pckMarca.Items.Add("BMW");
            pckMarca.Items.Add("BORGWARD");
            pckMarca.Items.Add("BOUNUS");
            pckMarca.Items.Add("CASE");
            pckMarca.Items.Add("CAVALIER");
            pckMarca.Items.Add("CATERPILLAR");
            pckMarca.Items.Add("CHEVROLET");
            pckMarca.Items.Add("CHRYSLER");
            pckMarca.Items.Add("CITROEN");
            pckMarca.Items.Add("CONTINENTAL");
            pckMarca.Items.Add("CUMMINS");
            pckMarca.Items.Add("DAEWOO");
            pckMarca.Items.Add("DAF");
            pckMarca.Items.Add("DAIHATSU");
            pckMarca.Items.Add("DATSUN");
            pckMarca.Items.Add("DETROIT");
            pckMarca.Items.Add("DEUTZ");
            pckMarca.Items.Add("DIAR");
            pckMarca.Items.Add("DI TELLA");
            pckMarca.Items.Add("DODGE");
            pckMarca.Items.Add("FIAT");
            pckMarca.Items.Add("FORD");
            pckMarca.Items.Add("GARDNER");
            pckMarca.Items.Add("GM");
            pckMarca.Items.Add("HANOMAG");
            pckMarca.Items.Add("HINO");
            pckMarca.Items.Add("HONDA");
            pckMarca.Items.Add("HYUNDAI");
            pckMarca.Items.Add("IKA");
            pckMarca.Items.Add("INDENOR");
            pckMarca.Items.Add("INTERNACIONAL");
            pckMarca.Items.Add("ISUZU");
            pckMarca.Items.Add("JOHN DEERE");
            pckMarca.Items.Add("KAMAZ");
            pckMarca.Items.Add("KAWASAKI");
            pckMarca.Items.Add("KHD");
            pckMarca.Items.Add("KIA");
            pckMarca.Items.Add("LADA");
            pckMarca.Items.Add("LAND ROVER");
            pckMarca.Items.Add("LEYLAND");
            pckMarca.Items.Add("LISTER");
            pckMarca.Items.Add("LOMBARDINI");
            pckMarca.Items.Add("M W M");
            pckMarca.Items.Add("MACK");
            pckMarca.Items.Add("MAN");
            pckMarca.Items.Add("MAXION");
            pckMarca.Items.Add("MAZ");
            pckMarca.Items.Add("MAZDA");
            pckMarca.Items.Add("MERCEDES BENZ");
            pckMarca.Items.Add("MITSUBISHI");
            pckMarca.Items.Add("NAVISTAR");
            pckMarca.Items.Add("NISSAN");
            pckMarca.Items.Add("OPEL");
            pckMarca.Items.Add("PEGASO");
            pckMarca.Items.Add("PERKINS");
            pckMarca.Items.Add("PEUGEOT");
            pckMarca.Items.Add("PORSCHE");
            pckMarca.Items.Add("RASTROJERO");
            pckMarca.Items.Add("RENAULT");
            pckMarca.Items.Add("R.V.I");
            pckMarca.Items.Add("ROVER");
            pckMarca.Items.Add("SCANIA VABIS");
            pckMarca.Items.Add("SCHLUTER");
            pckMarca.Items.Add("SEAT");
            pckMarca.Items.Add("SIMCA");
            pckMarca.Items.Add("SKODA");
            pckMarca.Items.Add("STEYR");
            pckMarca.Items.Add("SUBARU");
            pckMarca.Items.Add("SUZUKI");
            pckMarca.Items.Add("TOYOTA");
            pckMarca.Items.Add("UAZ");
            pckMarca.Items.Add("UNIVERSAL");
            pckMarca.Items.Add("UTB");
            pckMarca.Items.Add("VALIANT");
            pckMarca.Items.Add("VALTRA");
            pckMarca.Items.Add("VOLKSWAGEN");
            pckMarca.Items.Add("VOLVO");
            pckMarca.Items.Add("WAUKESHA");
            pckMarca.Items.Add("WILLYS");
            pckMarca.Items.Add("YAMAHA");
            pckMarca.Items.Add("YANMAR");
            pckMarca.Items.Add("ZANELLO");
            pckMarca.Items.Add("ZETOR");
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