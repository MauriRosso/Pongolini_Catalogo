using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Negocio;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            ObtenerAsientos();
        }

        protected override bool OnBackButtonPressed()
        {
            App.MasterD.Detail = new NavigationPage(new Inicio());
            return true;
        }

        public void SepararAsientosSemiTerminados()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.marca_modelo == "ADAPTACIONES")
                {
                    App.ListaGlobalSerie6000.Add(item);
                }
            }
            //Remuevo todas las adaptaciones de la ListaGlobalAsientos
            App.ListaGlobalAsientos.RemoveAll(x => x.marca_modelo == "ADAPTACIONES");
        }

        public async void ObtenerAsientos()
        {
            if (App.ListaGlobalAsientos.Count == 0) //Si es la primera vez que trae datos de guias..
            {
                Cargando.IsVisible = true;
                lblCargando.IsVisible = true;
                RestClient client = new RestClient();
                App.ListaGlobalAsientos = await client.Get<Asientos>("http://serviciowebpongolini.apphb.com/api/AsientosApi");//URL de la api.
                SepararAsientosSemiTerminados();
                Cargando.IsVisible = false;
                lblCargando.IsVisible = false;
            }
            else
            {
                SepararAsientosSemiTerminados();
            }
            ListaDatos_Final.Clear();
            foreach (var item in App.ListaGlobalSerie6000)
            {
                ListaDatos_Final.Add(new DimensionesViewModel { producto = "Asiento", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.angulo, codigo = item.codigo, img_diam_ext = "Diametro_Exterior_Asiento.png", img_diam_int = "Diametro_Interior_Asiento.png", img_largo_alto = "Altura.png", img_angulo_material = "Angulo.png" });
            }
            //Se actualiza el ListView
            ListViewSerie6000.ItemsSource = null;
            ListViewSerie6000.ItemsSource = ListaDatos_Final;
        }

        private void ListViewSerie6000_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dimension_asiento = e.Item as DimensionesViewModel;
            Asientos asiento_encontrado = App.ListaGlobalSerie6000.Find(x => x.codigo == dimension_asiento.codigo);
            Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
        }
    }
}