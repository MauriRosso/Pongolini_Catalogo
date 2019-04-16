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
    public partial class Intercambios : ContentPage
    {
        List<IntercambiosViewModel> ListaDatos_Final = new List<IntercambiosViewModel>(); //Se usa para mostrar los 3 campos necesarios
        List<Guias> ListaDatosGuias = new List<Guias>(); //Se usa para almacenar la informacion filtrada (guias).
        List<Guias> ListaAuxGuias = new List<Guias>(); //Me traigo TODAS las guias de apphb 
        List<Asientos> ListaDatosAsientos = new List<Asientos>(); //Se usa para almacenar la informacion filtrada (asientos).
        List<Asientos> ListaAuxAsientos = new List<Asientos>(); //Me traigo TODOS los asientos de apphb.

        string productoElegido = null;
        string fabricanteElegido = null;
        bool encontre_intercambio = false;
        bool encontre_fabricante = false;
        public List<string> ListaFabricantes = new List<string>();

        public Intercambios() //Solamente pasa la primera vez que se abre la pag.
        {
            InitializeComponent();
            CargarPickerFabricante();
            CargarListaFabricantes();
            CargarPickerTipoProducto();
            ListViewDatos.ItemTapped += ListViewDatos_ItemTapped;
            //BindingContext = new IntercambiosViewModel();
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;

        }

        public void CargarListaFabricantes()
        {
            ListaFabricantes.Add("RIOSULENSE");
            ListaFabricantes.Add("MAHLE");
        }


        private void ListViewDatos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (productoElegido == "Guías")
            {
                var producto_intercambio = e.Item as IntercambiosViewModel;
                Guias guia_encontrada = App.ListaGlobalGuias.Find(x => x.codigo == producto_intercambio.codigo);             
                Navigation.PushAsync(new DetalleProducto(guia_encontrada, null));
            }
            else
            {
                var producto_intercambio = e.Item as IntercambiosViewModel;
                Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.codigo == producto_intercambio.codigo);
                Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
            }
        }

        public void CargarPickerFabricante()
        {
            pckFabricante.Title = "Seleccione fabricante";
            pckFabricante.Items.Add("[ Todos ]");
            pckFabricante.Items.Add("RIOSULENSE");
            pckFabricante.Items.Add("MAHLE");
            pckFabricante.SelectedIndex = 0;
        }

        public void CargarPickerTipoProducto()
        {
            pckTipoProducto.Title = "Seleccione producto";
            pckTipoProducto.Items.Add("Guías");
            pckTipoProducto.Items.Add("Asientos");
            pckTipoProducto.SelectedIndex = 0;
        }

        public async void ObtenerAsientos()
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

            ListaDatosAsientos.Clear();
            ListaDatos_Final.Clear();
            fabricanteElegido = null;
            encontre_intercambio = false;
            encontre_fabricante = false;

            /////////-->FILTROS<--//////////

            if (pckFabricante.SelectedIndex != 0 && txtIntercambio.Text != null && txtIntercambio.Text != "") //Si se selecciono algo en el picker y en intercambios..
            {
                foreach (var item in App.ListaGlobalAsientos)
                {
                    if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                    {
                        if (item.codigo_riosulense != null && item.codigo_riosulense.Contains(txtIntercambio.Text) == true)
                        {
                            if (ListaDatosAsientos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                encontre_fabricante = true;
                                fabricanteElegido = "RIOSULENSE";
                            }
                        }
                    }
                    else
                    {
                        if (pckFabricante.SelectedItem.ToString() == "MAHLE")
                        {
                            if (item.codigo_mahle != null && item.codigo_mahle.Contains(txtIntercambio.Text) == true)
                            {
                                if (ListaDatosAsientos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                    encontre_fabricante = true;
                                    fabricanteElegido = "MAHLE";
                                }
                            }
                        }
                    }
                }
                if (encontre_fabricante == false)
                {
                   DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                }
            }
            else
            {
                if (pckFabricante.SelectedIndex != 0) //Solo se eligio por fabricante
                {
                    foreach (var item in App.ListaGlobalAsientos)
                    {
                        if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                        {
                            if (ListaDatosAsientos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                fabricanteElegido = "RIOSULENSE";
                            }
                        }
                        else
                        {
                            if (pckFabricante.SelectedItem.ToString() == "MAHLE")
                            {
                                if (ListaDatosAsientos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                    fabricanteElegido = "MAHLE";
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txtIntercambio.Text != null && txtIntercambio.Text != "") //Se eligio solo intercambio
                    {
                        foreach (var item in App.ListaGlobalAsientos)
                        {
                            if (item.codigo_riosulense != null && item.codigo_riosulense.Contains(txtIntercambio.Text) == true)
                            {
                                if (ListaDatosAsientos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                    encontre_intercambio = true;
                                }
                            }
                            else
                            {
                                if (item.codigo_mahle != null && item.codigo_mahle.Contains(txtIntercambio.Text) == true)
                                {
                                    if (ListaDatosAsientos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                        encontre_intercambio = true;
                                    }
                                }
                            }
                        }
                        if (encontre_intercambio == false)
                        {
                           DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                        }
                    }
                    else //No se eligio ninguno de los dos.
                    {
                        if (pckFabricante.SelectedIndex == 0 && txtIntercambio.Text == null || txtIntercambio.Text == "")
                        {
                            //Muestro TODOS los datos.
                            foreach (var item in App.ListaGlobalAsientos)
                            {
                                ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                            }
                        }
                    }
                }
            }
            //////// Cargo la informacion filtrada para mostrar en la ListView
            foreach (var item in ListaDatosAsientos)
            {
                if (fabricanteElegido != null)
                {
                    if (fabricanteElegido == "RIOSULENSE")
                    {
                        ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.numero_300indy, Intercambio = item.codigo_riosulense, codigo = item.codigo});
                    }
                    else //Son todos mahle
                    {
                        ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "MAHLE", Producto = item.numero_300indy, Intercambio = item.codigo_mahle, codigo = item.codigo });
                    }
                }
                else
                {
                    ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.numero_300indy, Intercambio = item.codigo_riosulense, codigo = item.codigo });
                    ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "MAHLE", Producto = item.numero_300indy, Intercambio = item.codigo_mahle, codigo = item.codigo });
                }
            }
            ///
            ////////////////

            //Se actualiza el ListView
            ListViewDatos.ItemsSource = null;
            ListViewDatos.ItemsSource = ListaDatos_Final;
        }

        public async void ObtenerGuias()
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

            ListaDatosGuias.Clear();
            ListaDatos_Final.Clear();
            fabricanteElegido = null;
            encontre_intercambio = false;
            encontre_fabricante = false;

            /////////-->FILTROS<--//////////
           
            if (pckFabricante.SelectedIndex != 0 && txtIntercambio.Text != null && txtIntercambio.Text != "") //Si se selecciono algo en el picker y en intercambios..
            {
                foreach (var item in App.ListaGlobalGuias)
                {
                    if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                    {
                        if (item.codigo_riosulense != null && item.codigo_riosulense.Contains(txtIntercambio.Text) == true)
                        {
                            if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                encontre_fabricante = true;
                                fabricanteElegido = "RIOSULENSE";
                            }
                        }
                    }
                    else
                    {
                        if (pckFabricante.SelectedItem.ToString() == "MAHLE")
                        {
                            if (item.codigo_mahle != null && item.codigo_mahle.Contains(txtIntercambio.Text) == true)
                            {
                                if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                    encontre_fabricante = true;
                                    fabricanteElegido = "MAHLE";
                                }
                            }
                        }
                    }
                }
                if (encontre_fabricante == false)
                {
                   DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                }
            }
            else
            {
                if (pckFabricante.SelectedIndex != 0) //Solo se eligio por fabricante
                {
                    foreach (var item in App.ListaGlobalGuias)
                    {
                        if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                        {
                            if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                fabricanteElegido = "RIOSULENSE";
                            }
                        }
                        else
                        {
                            if (pckFabricante.SelectedItem.ToString() == "MAHLE")
                            {
                                if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                    fabricanteElegido = "MAHLE";
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txtIntercambio.Text != null && txtIntercambio.Text != "") //Se eligio solo intercambio
                    {
                        foreach (var item in App.ListaGlobalGuias)
                        {
                            if (item.codigo_riosulense != null && item.codigo_riosulense.Contains(txtIntercambio.Text) == true)
                            {
                                if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                    encontre_intercambio = true;
                                }
                            }
                            else
                            {
                                if (item.codigo_mahle != null && item.codigo_mahle.Contains(txtIntercambio.Text) == true)
                                {
                                    if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                                        encontre_intercambio = true;
                                    }
                                }
                            }
                        }
                        if (encontre_intercambio == false)
                        {
                           DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                        }
                    }
                    else //No se eligio ninguno de los dos.
                    {
                        if (pckFabricante.SelectedIndex == 0 && txtIntercambio.Text == null || txtIntercambio.Text == "")
                        {
                            //Muestro TODOS los datos.
                            foreach (var item in App.ListaGlobalGuias)
                            {
                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                            }
                        }
                    }
                }
            }
            //////// Cargo la informacion filtrada para mostrar en la ListView
            foreach (var item in ListaDatosGuias)
            {
                if (fabricanteElegido != null)
                {
                    if (fabricanteElegido == "RIOSULENSE")
                    {
                        ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.numero_300indy, Intercambio = item.codigo_riosulense, codigo = item.codigo });
                    }
                    else //Son todos mahle
                    {
                        ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "MAHLE", Producto = item.numero_300indy, Intercambio = item.codigo_mahle, codigo = item.codigo });
                    }
                }
                else
                {
                    ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.numero_300indy, Intercambio = item.codigo_riosulense, codigo = item.codigo });
                    ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "MAHLE", Producto = item.numero_300indy, Intercambio = item.codigo_mahle, codigo = item.codigo });
                }           
            }
            ///
            ////////////////

            //Se actualiza el ListView
            ListViewDatos.ItemsSource = null;
            ListViewDatos.ItemsSource = ListaDatos_Final;
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            if (pckTipoProducto.SelectedIndex == 0) //Eligio guia
            {
                productoElegido = "Guías";
                ObtenerGuias();
            }
            else
            {
                productoElegido = "Asientos";
                ObtenerAsientos();
            }
        }
    }
}