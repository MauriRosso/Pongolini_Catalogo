using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Negocio;
using Plugin.Connectivity;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Aplicaciones : ContentPage
    {
        List<AplicacionesViewModel> ListaDatos_Final = new List<AplicacionesViewModel>(); //Informacion que se muestra en la listview.
        List<Guias> ListaDatosGuias = new List<Guias>(); //Almaceno info filtrada de guias
        List<Asientos> ListaDatosAsientos = new List<Asientos>(); //Almaceno info filtrada de asientos
        string productoElegido = string.Empty;

        public Aplicaciones()
        {
            InitializeComponent();
            CargarPickerProducto();
            CargarPickerMarca();
            CargarPickerTipoAplicacion();
            CargarTextoLabels();
            btnNuevaBusqueda.IsVisible = false;
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
            CargarCarrito();
        }

        private void CargarTextoLabels()
        {
            if (App.Idioma != "ES")
            {
                this.Title = "Applications";
                lblProducto.Text = "Product";
                pckProducto.Title = "Select product";
                pckProducto.Items.Clear();
                pckProducto.Items.Add("Valve guides");
                pckProducto.Items.Add("Valve seats");
                pckProducto.SelectedIndex = 0;
                lblMotor.Text = "Engine";
                lblMarca.Text = "Brand";
                lblTipoAplicacion.Text = "Application type";
                pckTipoAplicacion.Title = "Select application type";
                pckTipoAplicacion.Items.Clear();
                pckTipoAplicacion.Items.Add("[ All ]");
                pckTipoAplicacion.Items.Add("Admission");
                pckTipoAplicacion.Items.Add("Escape");
                pckTipoAplicacion.Items.Add("Admission and escape");
                pckTipoAplicacion.SelectedIndex = 0;
                btnBuscarAplicaciones.Text = "Search";
                btnLimpiarAplicaciones.Text = "Clear all";
                btnNuevaBusqueda.Text = "New search";
                var Cargando = this.FindByName<Label>("lblCargando");
                Cargando.Text = "Loading..";
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

        public void CargarPickerMarca()
        {
            if (App.Idioma == "ES")
            {
                pckMarca.Items.Add("[ Todos ]");
            }
            else
            {
                pckMarca.Items.Add("[ All ]");
            }
            pckMarca.Items.Add("AGRALE");
            pckMarca.Items.Add("ALCO");
            pckMarca.Items.Add("ALFA ROMEO");
            pckMarca.Items.Add("AUDI");
            pckMarca.Items.Add("AUSTIN");
            pckMarca.Items.Add("BAJAJ");
            pckMarca.Items.Add("BEDFORD");
            pckMarca.Items.Add("BERLIET");
            pckMarca.Items.Add("BIELARUS");
            pckMarca.Items.Add("BMW");
            pckMarca.Items.Add("BORGWARD");
            pckMarca.Items.Add("BOUNUS");
            pckMarca.Items.Add("CASE");
            pckMarca.Items.Add("CATERPILLAR");
            pckMarca.Items.Add("CAVALIER");
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
            pckMarca.Items.Add("DI TELLA");
            pckMarca.Items.Add("DIAR");
            pckMarca.Items.Add("DODGE");
            pckMarca.Items.Add("FIAT");
            pckMarca.Items.Add("FORD");
            pckMarca.Items.Add("GARDNER");
            pckMarca.Items.Add("GM");
            pckMarca.Items.Add("GUASCOR");
            pckMarca.Items.Add("HANOMAG");
            pckMarca.Items.Add("HINO");
            pckMarca.Items.Add("HONDA");
            pckMarca.Items.Add("HYUNDAI");
            pckMarca.Items.Add("IKA");
            pckMarca.Items.Add("INDENOR");
            pckMarca.Items.Add("INTERNACIONAL");
            pckMarca.Items.Add("ISUZU");
            pckMarca.Items.Add("JEEP");
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
            pckMarca.Items.Add("M.W.M");
            pckMarca.Items.Add("MACK");
            pckMarca.Items.Add("MAN");
            pckMarca.Items.Add("MAXION");
            pckMarca.Items.Add("MAZ");
            pckMarca.Items.Add("MAZDA");
            pckMarca.Items.Add("M BENZ");
            pckMarca.Items.Add("MITSUBISHI");
            pckMarca.Items.Add("NAVISTAR");
            pckMarca.Items.Add("NISSAN");
            pckMarca.Items.Add("OPEL");
            pckMarca.Items.Add("PEGASO");
            pckMarca.Items.Add("PERKINS");
            pckMarca.Items.Add("PEUGEOT");
            pckMarca.Items.Add("PONTIAC");
            pckMarca.Items.Add("PORSCHE");
            pckMarca.Items.Add("RASTROJERO");
            pckMarca.Items.Add("RENAULT");
            pckMarca.Items.Add("R.V.I");
            pckMarca.Items.Add("ROVER");
            pckMarca.Items.Add("SCANIA");
            pckMarca.Items.Add("SCANIA VABIS");
            pckMarca.Items.Add("SCHLUTER");
            pckMarca.Items.Add("SEAT");
            pckMarca.Items.Add("SIMCA");
            pckMarca.Items.Add("SKODA");
            pckMarca.Items.Add("STEYR");
            pckMarca.Items.Add("SUBARU");
            pckMarca.Items.Add("SUZUKI");
            pckMarca.Items.Add("TORNADO");
            pckMarca.Items.Add("TOYOTA");
            pckMarca.Items.Add("UAZ");
            pckMarca.Items.Add("UNIVERSAL");
            pckMarca.Items.Add("UTB");
            pckMarca.Items.Add("VALIANT");
            pckMarca.Items.Add("VALTRA");
            pckMarca.Items.Add("VILLA");
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
            pckProducto.Items.Add("Guías de válvulas");
            pckProducto.Items.Add("Asientos de válvulas");
            pckProducto.SelectedIndex = 0;
        }

        public void CargarPickerTipoAplicacion()
        {
            pckTipoAplicacion.Items.Add("[ Todos ]");
            pckTipoAplicacion.Items.Add("Admisión");
            pckTipoAplicacion.Items.Add("Escape");
            pckTipoAplicacion.Items.Add("Admisión y escape");
            pckTipoAplicacion.SelectedIndex = 0;
        }

        public void OcultarCamposAplicaciones()
        {
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = false;
            //Reactivo el button de nueva busqueda.
            btnNuevaBusqueda.IsVisible = true;
        }

        public async void ObtenerGuias()
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

            ListaDatosGuias.Clear();
            ListaDatos_Final.Clear();
            //filtros
            if (txtNOriginal.Text != null && txtNOriginal.Text != "")
            {
                BusquedaNOriginalGuias();
            }
            if (txtMotor.Text != null && txtMotor.Text != "")
            {
                BusquedaMotorGuias();
            }
            if (pckMarca.SelectedItem.ToString() != "[ Todos ]" && pckMarca.SelectedItem.ToString() != "[ All ]")
            {
                BusquedaMarcaGuias();
            }
            if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() != "[ All ]")
            {
                BusquedaTipoAplicacionGuias();
            }

            if ((txtNOriginal.Text == null || txtNOriginal.Text == "") && (txtMotor.Text == null || txtMotor.Text == "") && (pckMarca.SelectedItem.ToString() == "[ Todos ]" || pckMarca.SelectedItem.ToString() == "[ All ]") && (pckTipoAplicacion.SelectedItem.ToString() == "[ Todos ]" || pckTipoAplicacion.SelectedItem.ToString() == "[ All ]"))
            {
                //Mostrar TODOS los datos.
                foreach (var item in App.ListaGlobalGuias)
                {
                    ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Guía", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor, codigo = item.codigo });
                }
            }
            else
            {
                CoincidenciasGuias();
            }

            if (ListaDatos_Final.Count() == 0)
            {
                DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
            }
            else
            {
                //var ProductoGrilla = this.FindByName<Label>("lblProductoGrilla");
                //ProductoGrilla.Text = "Product";
                //var MotorGrilla = this.FindByName<Label>("lblMotorGrilla");
                //MotorGrilla.Text = "Engine";
            }
            ListViewAplicaciones.ItemsSource = null;
            ListViewAplicaciones.ItemsSource = ListaDatos_Final;
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

            ListaDatosAsientos.Clear();
            ListaDatos_Final.Clear();
            //filtros
            if (txtNOriginal.Text != null && txtNOriginal.Text != "")
            {
                BusquedaNOriginalAsientos();
            }
            if (txtMotor.Text != null && txtMotor.Text != "")
            {
                BusquedaMotorAsientos();
            }
            if (pckMarca.SelectedItem.ToString() != "[ Todos ]" && pckMarca.SelectedItem.ToString() != "[ All ]")
            {
                BusquedaMarcaAsientos();
            }
            if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() != "[ All ]")
            {
                BusquedaTipoAplicacionAsientos();
            }

            if ((txtNOriginal.Text == null || txtNOriginal.Text == "") && (txtMotor.Text == null || txtMotor.Text == "") && (pckMarca.SelectedItem.ToString() == "[ Todos ]"  || pckMarca.SelectedItem.ToString() == "[ All ]") && (pckTipoAplicacion.SelectedItem.ToString() == "[ Todos ]" || pckTipoAplicacion.SelectedItem.ToString() == "[ All ]"))
            {
                //Mostrar TODOS los datos.
                foreach (var item in App.ListaGlobalAsientos)
                {
                    if (item.marca_modelo != "ADAPTACIONES")
                    {
                        ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Asiento", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor, codigo = item.codigo });
                    }
                }
            }
            else
            {
                CoincidenciasAsientos();
            }

            if (ListaDatos_Final.Count() == 0)
            {
                DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
            }
            else
            {
                //var ProductoGrilla = this.FindByName<Label>("lblProductoGrilla");
                //ProductoGrilla.Text = "Product";
                //var MotorGrilla = this.FindByName<Label>("lblMotorGrilla");
                //MotorGrilla.Text = "Engine";
            }
            ListViewAplicaciones.ItemsSource = null;
            ListViewAplicaciones.ItemsSource = ListaDatos_Final;


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

        private async void btnBuscarAplicaciones_Clicked(object sender, EventArgs e)
        {
            if (!ConexionRevisada()) //El dispositivo no soporta el plugin, no puedo controlarlo.
            {
                if (App.Idioma == "ES")
                {
                    await DisplayAlert("Estado de la conexión", "No hemos podido comprobar el estado de tu conexión a internet. Por favor, asegúrate de estar conectado a la red antes de realizar una búsqueda.", "OK");
                }
                else
                {
                    await DisplayAlert("Connection state", "We have not been able to check the status of your internet connection. Please make sure you are connected to the network before performing a search.", "OK");
                }
            }
            if (TieneConexion())
            {
                //Oculto la busqueda para mostrar con mas espacio la listview
                OcultarCamposAplicaciones();
                ListViewAplicaciones.ItemsSource = null;
                ListViewAplicaciones.ItemsSource = ListaDatos_Final;
                if (pckProducto.SelectedItem.ToString() == "Guías de válvulas" || pckProducto.SelectedItem.ToString() == "Valve guides")
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
            else
            {
                if (App.Idioma == "ES")
                {
                    await DisplayAlert("Error de conexión", "No estás conectado a internet o tu señal es muy débil. Por favor, inténtalo más tarde.", "OK");
                }
                else
                {
                    await DisplayAlert("Failed to connect", "You are not connected to the internet or your signal is very weak. Please ty again later.", "OK");
                }
            }
        }

        private void btnNuevaBusqueda_Clicked(object sender, EventArgs e)
        {
            btnNuevaBusqueda.IsVisible = false;
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = true;
        }

        public void BusquedaNOriginalGuias()
        {
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.numero_original != null)
                {
                    if (item.numero_original.Contains(txtNOriginal.Text))
                    {
                        if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                        {
                            ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                        }
                    }
                }
            }
        }

        public void BusquedaNOriginalAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.numero_original != null)
                {
                    if (item.numero_original.Contains(txtNOriginal.Text))
                    {
                        if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                        {
                            ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                        }
                    }
                }
            }
        }

        public void BusquedaMotorGuias()
        {

            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.motor != null)
                {
                    if (item.motor.Contains(txtMotor.Text) == true)
                    {
                        if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                        {
                            ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                        }
                    }
                }
            }

        }

        public void BusquedaMotorAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.motor != null)
                {
                    if (item.motor.Contains(txtMotor.Text) == true)
                    {
                        if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                        {
                            ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                        }
                    }
                }
            }
        }

        public void BusquedaMarcaGuias()
        {
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.marca_modelo == pckMarca.SelectedItem.ToString())
                {
                    if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }

        public void BusquedaMarcaAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.marca_modelo == pckMarca.SelectedItem.ToString())
                {
                    if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }

        public void BusquedaTipoAplicacionAsientos()
        {
            string adm_esc = string.Empty;
            if (pckTipoAplicacion.SelectedItem.ToString() == "Admisión" || pckTipoAplicacion.SelectedItem.ToString() == "Admission")
            {
                adm_esc = "ADM";
            }
            else
            {
                if (pckTipoAplicacion.SelectedItem.ToString() == "Escape")
                {
                    adm_esc = "ESC";
                }
                else
                {
                    adm_esc = "A-E";
                }
            }
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.admision_escape == adm_esc)
                {
                    if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }

        public void BusquedaTipoAplicacionGuias()
        {
            string adm_esc = string.Empty;
            if (pckTipoAplicacion.SelectedItem.ToString() == "Admisión" || pckTipoAplicacion.SelectedItem.ToString() == "Admission")
            {
                adm_esc = "ADM";
            }
            else
            {
                if (pckTipoAplicacion.SelectedItem.ToString() == "Escape")
                {
                    adm_esc = "ESC";
                }
                else
                {
                    adm_esc = "A-E";
                }
            }
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.admision_escape == adm_esc)
                {
                    if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private int CantidadParametros()
        {
            int CP = 0;

            if (txtNOriginal.Text != null && txtNOriginal.Text != "")
            {
                CP += 1;
            }
            if (txtMotor.Text != null && txtMotor.Text != "")
            {
                CP += 1;
            }
            if (pckMarca.SelectedItem.ToString() != "[ Todos ]" && pckMarca.SelectedItem.ToString() != "[ All ]")
            {
                CP += 1;
            }
            if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() != "[ All ]")
            {
                CP += 1;
            }

            return CP;
        }

        public void CoincidenciasGuias()
        {
            foreach (var item in ListaDatosGuias)
            {
                int coincidencias = 0;
                if (txtNOriginal.Text != null && txtNOriginal.Text != "")
                {
                    if (item.numero_original != null)
                    {
                        if (item.numero_original.Contains(txtNOriginal.Text))
                        {
                            coincidencias += 1;
                        }
                    }
                }
                if (txtMotor.Text != null && txtMotor.Text != "")
                {
                    if (item.motor != null)
                    {
                        if (item.motor.Contains(txtMotor.Text) == true)
                        {
                            coincidencias += 1;
                        }
                    }
                }
                if (pckMarca.SelectedItem.ToString() != "[ Todos ]" && pckMarca.SelectedItem.ToString() != "[ All ]")
                {
                    if (item.marca_modelo == pckMarca.SelectedItem.ToString())
                    {
                        coincidencias += 1;
                    }
                }
                if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() != "[ All ]")
                {
                    string adm_esc = string.Empty;
                    if (pckTipoAplicacion.SelectedItem.ToString() == "Admisión" || pckTipoAplicacion.SelectedItem.ToString() == "Admission")
                    {
                        adm_esc = "ADM";
                    }
                    else
                    {
                        if (pckTipoAplicacion.SelectedItem.ToString() == "Escape")
                        {
                            adm_esc = "ESC";
                        }
                        else
                        {
                            adm_esc = "A-E";
                        }
                    }
                    if (item.admision_escape == adm_esc)
                    {
                        coincidencias += 1;
                    }
                }

                int cantPar = CantidadParametros();
                if (cantPar == coincidencias)
                {
                    ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Guía", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor, codigo = item.codigo });
                }
            }
        }

        public void CoincidenciasAsientos()
        {
            foreach (var item in ListaDatosAsientos)
            {
                int coincidencias = 0;
                if (txtNOriginal.Text != null && txtNOriginal.Text != "")
                {
                    if (item.numero_original != null)
                    {
                        if (item.numero_original.Contains(txtNOriginal.Text))
                        {
                            coincidencias += 1;
                        }
                    }
                }
                if (txtMotor.Text != null && txtMotor.Text != "")
                {
                    if (item.motor != null)
                    {
                        if (item.motor.Contains(txtMotor.Text) == true)
                        {
                            coincidencias += 1;
                        }
                    }
                }
                if (pckMarca.SelectedItem.ToString() != "[ Todos ]" && pckMarca.SelectedItem.ToString() != "[ All ]")
                {
                    if (item.marca_modelo == pckMarca.SelectedItem.ToString())
                    {
                        coincidencias += 1;
                    }
                }
                if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() != "[ All ]")
                {
                    string adm_esc = string.Empty;
                    if (pckTipoAplicacion.SelectedItem.ToString() == "Admisión" || pckTipoAplicacion.SelectedItem.ToString() == "Admission")
                    {
                        adm_esc = "ADM";
                    }
                    else
                    {
                        if (pckTipoAplicacion.SelectedItem.ToString() == "Escape")
                        {
                            adm_esc = "ESC";
                        }
                        else
                        {
                            adm_esc = "A-E";
                        }
                    }
                    if (item.admision_escape == adm_esc)
                    {
                        coincidencias += 1;
                    }
                }
                int cantPar = CantidadParametros();
                if (cantPar == coincidencias)
                {
                    if (item.marca_modelo != "ADAPTACIONES")
                    {
                        ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Asiento", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor, codigo = item.codigo });
                    }
                }
            }
        }

        private void ListViewAplicaciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (productoElegido == "Guías")
            {
                var dimension_guia = e.Item as AplicacionesViewModel;
                Guias guia_encontrada = App.ListaGlobalGuias.Find(x => x.codigo == dimension_guia.codigo);
                Navigation.PushAsync(new DetalleProducto(guia_encontrada, null));
            }
            else
            {
                if (productoElegido == "Asientos")
                {
                    var dimension_asiento = e.Item as AplicacionesViewModel;
                    Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.codigo == dimension_asiento.codigo);
                    Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
                }
            }
        }

        private void btnLimpiarAplicaciones_Clicked(object sender, EventArgs e)
        {
            txtNOriginal.Text = null;
            txtMotor.Text = null;
            pckMarca.SelectedIndex = 0;
            pckTipoAplicacion.SelectedIndex = 0;
            ListaDatos_Final.Clear();
            ListViewAplicaciones.ItemsSource = null;
            ListViewAplicaciones.ItemsSource = ListaDatos_Final;
        }

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }
    }
}