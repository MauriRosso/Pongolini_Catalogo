using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Negocio;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BujesPage : ContentPage
    {
        List<Bujes> ListaDatosBujes = new List<Bujes>(); //Almaceno info filtrada de bujes
        List<BujesViewModel> ListaDatos_Final = new List<BujesViewModel>(); //Informacion que se muestra en la listview.

        public BujesPage()
        {
            InitializeComponent();
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
                lblTubosParaEncasquillar.Text = "CASTING BUSHINGS";
                Title = "BUSHINGS";
                lblCargando.Text = "Loading..";
            }
        }
        protected override void OnAppearing()
        {
            if (!ConexionRevisada()) //El dispositivo no soporta el plugin, no puedo controlarlo.
            {
                if (App.Idioma == "ES")
                {
                    Application.Current.MainPage.DisplayAlert("Estado de la conexión", "No hemos podido comprobar el estado de tu conexión a internet. Por favor, asegúrate de estar conectado a la red antes de realizar una búsqueda.", "OK");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Connection state", "We have not been able to check the status of your internet connection. Please make sure you are connected to the network before performing a search.", "OK");
                }
            }
            if (TieneConexion())
            {
                ObtenerBujes();
            }
            else
            {
                if (App.Idioma == "ES")
                {
                    Application.Current.MainPage.DisplayAlert("Error de conexión", "No estás conectado a internet o tu señal es muy débil. Por favor, inténtalo más tarde.", "OK");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Failed to connect", "You are not connected to the internet or your signal is very weak. Please ty again later.", "OK");
                }
            }
            base.OnAppearing();
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

        public async void ObtenerBujes()
        {
            if (App.ListaGlobalBujes.Count == 0) //Si es la primera vez que trae datos de guias..
            {
                Cargando.IsVisible = true;
                lblCargando.IsVisible = true;
                RestClient client = new RestClient();
                App.ListaGlobalBujes = await client.Get<Bujes>("http://serviciowebpongolini.apphb.com/api/BujesApi");//URL de la api.
                Cargando.IsVisible = false;
                lblCargando.IsVisible = false;
            }
            ListaDatosBujes.Clear();
            ListaDatos_Final.Clear();

            //Mostrar TODOS los datos.
            foreach (var item in App.ListaGlobalBujes)
            {
                if (item.codigo[1].ToString() == "T")
                {
                    ListaDatos_Final.Add(new BujesViewModel { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, img_diam_ext = "Diametro_Exterior_Guia.png", img_diam_int = "Diametro_Interior_Guia.png", img_largo = "Largo.png", aplicacion = "DIESEL-GNC" });
                }
                else
                {
                    ListaDatos_Final.Add(new BujesViewModel { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, img_diam_ext = "Diametro_Exterior_Guia.png", img_diam_int = "Diametro_Interior_Guia.png", img_largo = "Largo.png", aplicacion = "NAFTERO" });
                }
            }

            ListViewBujes.ItemsSource = null;
            ListViewBujes.ItemsSource = ListaDatos_Final;
        }
           
        private void ListViewBujes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var buje = e.Item as BujesViewModel;
            Bujes buje_encontrado = App.ListaGlobalBujes.Find(x => x.codigo == buje.codigo);
            Navigation.PushAsync(new DetalleBuje(buje_encontrado));
        }      

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }
    }
}