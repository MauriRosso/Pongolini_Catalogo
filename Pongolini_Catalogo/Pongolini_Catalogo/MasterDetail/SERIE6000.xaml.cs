using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Negocio;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;
using Plugin.Connectivity;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SERIE6000 : ContentPage
	{
        List<DimensionesViewModel> ListaDatos_Final = new List<DimensionesViewModel>(); //Informacion que se muestra en la listview.
        public SERIE6000 ()
		{
			InitializeComponent ();
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
            CargarCarrito();
            CargarTextoLabels();
        }
        private void CargarTextoLabels()
        {
            if (App.Idioma != "ES")
            {
                Title = "SERIES 6000";
                lblAsientosSemiTerminados.Text = "SEMI-FINISHED SEATS";
                lblCargando.Text = "Loading..";
            }
        }
        protected override void OnAppearing()
        {
            if (!ConexionRevisada()) //El dispositivo no soporta el plugin, no puedo controlarlo.
            {
                Application.Current.MainPage.DisplayAlert("Estado de la conexión", "No hemos podido comprobar el estado de tu conexión a internet. Por favor, asegúrate de estar conectado a la red antes de realizar una búsqueda.", "OK");
            }
            if (TieneConexion())
            {
                ObtenerAsientos();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error de conexión", "No estás conectado a internet o tu señal es muy debil. Por favor, reintenta más tarde.", "OK");
            }
            base.OnAppearing();
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
            App.MasterD.Detail = new NavigationPage(new Inicio());
            return true;
        }

        private bool TieneConexion()
        {
            if (CrossConnectivity.Current.IsConnected == false)
            {
                return false;
            }
            return true;
        }

        private bool ConexionRevisada()
        {
            if (CrossConnectivity.IsSupported)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void ObtenerAsientos()
        {
            if (App.ListaGlobalAsientos.Count == 0) //Si es la primera vez que trae datos de guias..
            {
                Cargando.IsVisible = true;
                lblCargando.IsVisible = true;
                RestClient client = new RestClient();
                App.ListaGlobalAsientos = await client.Get<Asientos>("http://serviciowebpongolini.apphb.com/api/AsientosApi");//URL de la api.
                Cargando.IsVisible = false;
                lblCargando.IsVisible = false;
            }
            
            ListaDatos_Final.Clear();
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.marca_modelo == "ADAPTACIONES")
                {
                    ListaDatos_Final.Add(new DimensionesViewModel { producto = "Asiento", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.angulo, codigo = item.codigo, img_diam_ext = "Diametro_Exterior_Asiento.png", img_diam_int = "Diametro_Interior_Asiento.png", img_largo_alto = "Altura.png", img_angulo_material = "Angulo.png" });
                }
            }
            //Se actualiza el ListView
            ListViewSerie6000.ItemsSource = null;
            ListViewSerie6000.ItemsSource = ListaDatos_Final;
        }

        private void ListViewSerie6000_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dimension_asiento = e.Item as DimensionesViewModel;
            Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.codigo == dimension_asiento.codigo);
            Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
        }

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Presiona el frame de +INFO
            await PopupNavigation.Instance.PushAsync(new PopupModalSERIE6000()); //Le pido la cantidad             
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //Presiona el label de +INFO
            await PopupNavigation.Instance.PushAsync(new PopupModalSERIE6000()); //Le pido la cantidad             
        }
    }
}