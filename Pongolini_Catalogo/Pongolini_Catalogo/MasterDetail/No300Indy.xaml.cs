using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using Negocio;
using Plugin.Connectivity;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class No300Indy : ContentPage
    {
        List<DimensionesViewModel> ListaDatos_Final = new List<DimensionesViewModel>(); //Informacion que se muestra en la listview.
        string productoElegido = string.Empty;

        public No300Indy()
        {
            InitializeComponent();
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
            CargarCarrito();
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

        private async void ObtenerGuias()
        {
            if (App.ListaGlobalGuias.Count == 0) //Si es la primera vez que trae datos de guias..
            {
                Cargando.IsVisible = true;
                lblCargando.IsVisible = true;
                RestClient client = new RestClient();
                App.ListaGlobalGuias = await client.Get<Guias>("http://serviciowebpongolini.apphb.com/api/GuiasApi");//URL de la api.
                Cargando.IsVisible = false;
                lblCargando.IsVisible = false;
            }

            ListaDatos_Final.Clear();
            //filtros
            if (txtNO300Indy.Text != null && txtNO300Indy.Text != "")
            {
                Busqueda300indyGuias();
            }

            if (ListaDatos_Final.Count() == 0)
            {
                DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
            }
            ListView300indy.ItemsSource = null;
            ListView300indy.ItemsSource = ListaDatos_Final;

        }

        private async void ObtenerAsientos()
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
            //filtros


            if (txtNO300Indy.Text != null && txtNO300Indy.Text != "")
            {
                Busqueda300indyAsientos();
            }

            if (ListaDatos_Final.Count() == 0)
            {
                DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
            }
            ListView300indy.ItemsSource = null;
            ListView300indy.ItemsSource = ListaDatos_Final;
        }

        private void Busqueda300indyGuias()
        {
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.numero_300indy == txtNO300Indy.Text)
                {
                    if (item.material != null)
                    {
                        ListaDatos_Final.Add(new DimensionesViewModel { producto = "Guía", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.material, codigo = item.codigo, img_diam_int = "Diametro_Interior_Guia.png", img_diam_ext = "Diametro_Exterior_Guia.png", img_largo_alto = "Largo.png", img_angulo_material = (item.material == "FG") ? "Fundicion_Gris.png" : "Bronce.png" });
                    }
                    else
                    {
                        ListaDatos_Final.Add(new DimensionesViewModel { producto = "Guía", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.material, codigo = item.codigo, img_diam_int = "Diametro_Interior_Guia.png", img_diam_ext = "Diametro_Exterior_Guia.png", img_largo_alto = "Largo.png" });
                    }
                }
            }
        }

        private void Busqueda300indyAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.numero_300indy == txtNO300Indy.Text)
                {
                    ListaDatos_Final.Add(new DimensionesViewModel { producto = "Asiento", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.angulo, codigo = item.codigo, img_diam_ext = "Diametro_Exterior_Asiento.png", img_diam_int = "Diametro_Interior_Asiento.png", img_largo_alto = "Altura.png", img_angulo_material = "Angulo.png" });
                }
            }
        }

        private void ListView300indy_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (productoElegido == null) //Significa que cargo la listview con todos los datos (guias y asientos)
            {
                var prod = e.Item as DimensionesViewModel;
                productoElegido = prod.producto;
            }
            if (productoElegido == "Guías")
            {
                var dimension_guia = e.Item as DimensionesViewModel;
                Guias guia_encontrada = App.ListaGlobalGuias.Find(x => x.codigo == dimension_guia.codigo);
                Navigation.PushAsync(new DetalleProducto(guia_encontrada, null));
            }
            else
            {
                if (productoElegido == "Asientos")
                {
                    var dimension_asiento = e.Item as DimensionesViewModel;
                    Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.codigo == dimension_asiento.codigo);
                    Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
                }
            }
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

        private async void btnBuscar300Indy_Clicked(object sender, EventArgs e)
        {                   
            if (!ConexionRevisada()) //El dispositivo no soporta el plugin, no puedo controlarlo.
            {
                await DisplayAlert("Estado de la conexión", "No hemos podido comprobar el estado de tu conexión a internet. Por favor, asegúrate de estar conectado a la red antes de realizar una búsqueda.", "OK");
            }
            if (TieneConexion())
            {
                ListaDatos_Final.Clear();
                ListView300indy.ItemsSource = null;
                ListView300indy.ItemsSource = ListaDatos_Final;
                productoElegido = null;
                if (txtNO300Indy.Text != null && txtNO300Indy.Text != "")
                {
                    txtNO300Indy.Text = txtNO300Indy.Text.ToUpper();
                    if (txtNO300Indy.Text[0] == 'G')
                    {
                        productoElegido = "Guías";
                        ObtenerGuias();
                    }
                    else
                    {
                        if (txtNO300Indy.Text[0] == 'A')
                        {
                            productoElegido = "Asientos";
                            ObtenerAsientos();
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Por favor, busque el producto ingresando el código de 300 Indy.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error de conexión", "No estás conectado a internet o tu señal es muy debil. Por favor, reintenta más tarde.", "OK");
            }
        }

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }
    }
}