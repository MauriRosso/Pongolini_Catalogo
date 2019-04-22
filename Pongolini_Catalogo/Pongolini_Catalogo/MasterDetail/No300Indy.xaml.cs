using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using Negocio;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class No300Indy : ContentPage
    {
        List<DimensionesViewModel> ListaDatos_Final = new List<DimensionesViewModel>(); //Informacion que se muestra en la listview.
        List<Guias> ListaAuxGuias = new List<Guias>(); //Traigo TODAS las guias de apphb
        List<Asientos> ListaAuxAsientos = new List<Asientos>(); //Traigo TODAS los asientos de apphb
        string productoElegido = string.Empty;

        public No300Indy()
        {
            InitializeComponent();
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
        }

        private async void ObtenerGuias()
        {
            if (App.ListaGlobalGuias.Count == 0) //Si es la primera vez que trae datos de guias..
            {
                Cargando.IsVisible = true;
                lblCargando.IsVisible = true;
                RestClient client = new RestClient();
                ListaAuxGuias = await client.Get<Guias>("http://serviciowebpongolini.apphb.com/api/GuiasApi");//URL de la api.
                App.ListaGlobalGuias = ListaAuxGuias;
                Cargando.IsVisible = false;
                lblCargando.IsVisible = false;
            }
            else //Si alguna vez ya trajo los datos, simplemente se los asigno.
            {
                ListaAuxGuias = App.ListaGlobalGuias;
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
                ListaAuxAsientos = await client.Get<Asientos>("http://serviciowebpongolini.apphb.com/api/AsientosApi");//URL de la api.
                App.ListaGlobalAsientos = ListaAuxAsientos;
                Cargando.IsVisible = false;
                lblCargando.IsVisible = false;
            }
            else //Si alguna vez ya trajo los datos, simplemente se los asigno.
            {
                ListaAuxAsientos = App.ListaGlobalAsientos;
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
                var dimension_asiento = e.Item as DimensionesViewModel;
                Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.codigo == dimension_asiento.codigo);
                Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
            }
        }       

        private void btnBuscar300Indy_Clicked(object sender, EventArgs e)
        {
            ListaDatos_Final.Clear();
            ListView300indy.ItemsSource = null;
            ListView300indy.ItemsSource = ListaDatos_Final;
            productoElegido = null;
            if (txtNO300Indy.Text != null && txtNO300Indy.Text != "")
            {
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
                        DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                    }
                }
            }
            else
            {
                DisplayAlert("Error", "Por favor, busque el producto ingresando el código de 300 Indy.", "OK");
            }
        }
    }
}