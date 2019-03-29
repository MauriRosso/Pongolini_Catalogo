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
            ListaFabricantes.Add("TRW");
            ListaFabricantes.Add("METELLI");
            ListaFabricantes.Add("RIOSULENSE");
            ListaFabricantes.Add("SM");
        }


        private void ListViewDatos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (productoElegido == "Guías")
            {
                var producto_intercambio = e.Item as IntercambiosViewModel;
                Guias guia_encontrada = App.ListaGlobalGuias.Find(x => x.numero_300indy == producto_intercambio.Producto);             
                Navigation.PushAsync(new DetalleProducto(guia_encontrada, null));
            }
            else
            {
                var producto_intercambio = e.Item as IntercambiosViewModel;
                Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.numero_300indy == producto_intercambio.Producto);
                Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
            }
        }

        public void CargarPickerFabricante()
        {
            pckFabricante.Title = "Seleccione fabricante";
            pckFabricante.Items.Add("[ Todos ]");
            pckFabricante.Items.Add("TRW");
            pckFabricante.Items.Add("METELLI");
            pckFabricante.Items.Add("RIOSULENSE");
            pckFabricante.Items.Add("SM");
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

            bool HayIntercambio = false;
            bool HayIntercambio_Fabricante = false;
            ListaDatosGuias.Clear();
            ListaDatos_Final.Clear();

            //Filtro por fabricante
            if (pckFabricante.SelectedIndex != 0 && txtIntercambio.Text != null) //Si se selecciono algo en el picker y no dejo en "[ Todos ]"..
            {
                foreach (var item in ListaAuxGuias)
                {
                    if (pckFabricante.SelectedItem.ToString() == "TRW")
                    {
                        if (item.codigo_trw.Contains(txtIntercambio.Text) == true || item.codigo_trw == txtIntercambio.Text)
                        {
                            if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = "vip" + item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                HayIntercambio_Fabricante = true;
                            }
                        }
                    }
                    else
                    {
                        if (pckFabricante.SelectedItem.ToString() == "METELLI")
                        {
                            if (item.codigo_metelli.Contains(txtIntercambio.Text) == true || item.codigo_metelli == txtIntercambio.Text)
                            {
                                if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = "vip" + item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                    HayIntercambio_Fabricante = true;
                                }
                            }
                        }
                        else
                        {
                            if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                            {
                                if (item.codigo_riosulense.Contains(txtIntercambio.Text) == true || item.codigo_riosulense == txtIntercambio.Text)
                                {
                                    if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = "vip" + item.codigo_riosulense });
                                        HayIntercambio_Fabricante = true;
                                    }
                                }
                            }
                            else
                            {
                                if (pckFabricante.SelectedItem.ToString() == "SM")
                                {
                                    if (item.codigo_sm.Contains(txtIntercambio.Text) == true || item.codigo_sm == txtIntercambio.Text)
                                    {
                                        if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                        {
                                            ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = "vip" + item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                            HayIntercambio_Fabricante = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (HayIntercambio_Fabricante == false)
                {
                    DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                }
            }
            else
            {
                if (pckFabricante.SelectedIndex != 0) //Solo se eligio por fabricante
                {
                    foreach (var item in ListaAuxGuias)
                    {
                        if (pckFabricante.SelectedItem.ToString() == "TRW")
                        {
                            if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = "vip" + item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                            }
                        }
                        else
                        {
                            if (pckFabricante.SelectedItem.ToString() == "METELLI")
                            {
                                if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = "vip" + item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                }
                            }
                            else
                            {
                                if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                                {
                                    if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = "vip" + item.codigo_riosulense });
                                    }
                                }
                                else
                                {
                                    if (pckFabricante.SelectedItem.ToString() == "SM")
                                    {
                                        if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                        {
                                            ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = "vip" + item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txtIntercambio.Text != null) //Se eligio solo intercambio
                    {
                        foreach (var item in ListaAuxGuias)
                        {
                            if (item.codigo_trw.Contains(txtIntercambio.Text) == true || item.codigo_trw == txtIntercambio.Text)
                            {
                                if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                    HayIntercambio = true;
                                }
                            }
                            else
                            {
                                if (item.codigo_metelli.Contains(txtIntercambio.Text) == true || item.codigo_metelli == txtIntercambio.Text)
                                {
                                    if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                        HayIntercambio = true;
                                    }
                                }
                                else
                                {
                                    if (item.codigo_riosulense.Contains(txtIntercambio.Text) == true || item.codigo_riosulense == txtIntercambio.Text)
                                    {
                                        if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                        {
                                            ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                            HayIntercambio = true;
                                        }
                                    }
                                    else
                                    {
                                        if (item.codigo_sm.Contains(txtIntercambio.Text) == true || item.codigo_sm == txtIntercambio.Text)
                                        {
                                            if (ListaDatosGuias.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                            {
                                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                                                HayIntercambio = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (HayIntercambio == false)
                        {
                            DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
                        }
                    }
                    else //No se eligio ninguno de los dos.
                    {
                        if (pckFabricante.SelectedIndex == 0 && txtIntercambio.Text == null)
                        {
                            //Muestro TODOS los datos.
                            foreach (var item in ListaAuxGuias)
                            {
                                ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                            }
                        }
                    }
                }
            }
            //////// Cargo la informacion filtrada para mostrar en la ListView
            foreach (var item in ListaDatosGuias)
            {
                if (item.codigo_trw != null && item.codigo_trw.Contains("vip") == true)
                {
                    //En este paso deberia quitarle la palabra VIP asi no se ve en la listview.
                    ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "TRW", Producto = item.numero_300indy, Intercambio = item.codigo_trw });
                }
                else
                {
                    if (item.codigo_metelli != null && item.codigo_metelli.Contains("vip") == true)
                    {
                        ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "METELLI", Producto = item.numero_300indy, Intercambio = item.codigo_metelli });
                    }
                    else
                    {
                        if (item.codigo_riosulense != null && item.codigo_riosulense.Contains("vip") == true)
                        {
                            ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.numero_300indy, Intercambio = item.codigo_riosulense });
                        }
                        else
                        {
                            if (item.codigo_sm != null && item.codigo_sm.Contains("vip") == true)
                            {
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "SM", Producto = item.numero_300indy, Intercambio = item.codigo_sm });
                            }
                            else //Ninguno es vip.. muestro todos
                            {
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "TRW", Producto = item.numero_300indy, Intercambio = item.codigo_trw });
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "METELLI", Producto = item.numero_300indy, Intercambio = item.codigo_metelli });
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.numero_300indy, Intercambio = item.codigo_riosulense });
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "SM", Producto = item.numero_300indy, Intercambio = item.codigo_sm });
                            }
                        }
                    }
                }
            }
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