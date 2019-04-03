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
        List<AplicacionesViewModel> ListaDatos_Final = new List<AplicacionesViewModel>(); //Informacion que se muestra en la listview.
        List<Guias> ListaDatosGuias = new List<Guias>(); //Almaceno info filtrada de guias
        List<Guias> ListaAuxGuias = new List<Guias>(); //Traigo TODAS las guias de apphb
        List<Asientos> ListaDatosAsientos = new List<Asientos>(); //Almaceno info filtrada de asientos
        List<Asientos> ListaAuxAsientos = new List<Asientos>(); //Traigo TODAS los asientos de apphb
        string productoElegido = string.Empty;

        public Aplicaciones()
        {
            InitializeComponent();
            CargarPickerProducto();
            CargarPickerMarca();
            CargarPickerTipoAplicacion();
            btnNuevaBusqueda.IsVisible = false;
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
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
            pckMarca.Items.Add("PONTIAC");
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
            pckMarca.Items.Add("TORNADO");
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
            //filtros
            if (txtNOriginal.Text != null)
            {
                BusquedaNOriginalGuias();
            }
            if (txtMotor.Text != null)
            {
                BusquedaMotorGuias();
            }
            if (pckMarca.SelectedItem.ToString() != "[ Todos ]")
            {
                BusquedaMarcaGuias();
            }

            if (txtNOriginal.Text == null && txtMotor.Text == null && pckMarca.SelectedItem.ToString() == "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() == "[ Todos ]" && txtCilindrada.Text == null)
            {
                //Mostrar TODOS los datos.
                foreach (var item in App.ListaGlobalGuias)
                {
                    ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Guía", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor});
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
            //filtros
            if (txtNOriginal.Text != null)
            {
                BusquedaNOriginalAsientos();
            }
            if (txtMotor.Text != null)
            {
                BusquedaMotorAsientos();
            }
            if (pckMarca.SelectedItem.ToString() != "[ Todos ]")
            {
                BusquedaMarcaAsientos();
            }

            if (txtNOriginal.Text == null && txtMotor.Text == null && pckMarca.SelectedItem.ToString() == "[ Todos ]" && pckTipoAplicacion.SelectedItem.ToString() == "[ Todos ]" && txtCilindrada.Text == null)
            {
                //Mostrar TODOS los datos.
                foreach (var item in App.ListaGlobalAsientos)
                {
                    ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Asiento", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor});
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
            ListViewAplicaciones.ItemsSource = null;
            ListViewAplicaciones.ItemsSource = ListaDatos_Final;


        }

        private void btnBuscarAplicaciones_Clicked(object sender, EventArgs e)
        {
            //Oculto la busqueda para mostrar con mas espacio la listview
            OcultarCamposAplicaciones();

            if (pckProducto.SelectedItem.ToString() == "Guías")
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
                if (txtNOriginal.Text == item.numero_original)
                {
                    if (ListaDatosGuias.Contains(item) == false)
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }

        public void BusquedaNOriginalAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (txtNOriginal.Text == item.numero_original)
                {
                    if (ListaDatosAsientos.Contains(item) == false)
                    {
                        //ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, angulo = item.angulo, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                    }
                }
            }
        }

        public void BusquedaMotorGuias()
        {

            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.marca_modelo.Contains(txtMotor.Text) == true) //Si el campo marca_modelo contiene el motor
                {
                    if (ListaDatosGuias.Contains(item) == false)
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }

        }

        public void BusquedaMotorAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.marca_modelo.Contains(txtMotor.Text) == true)
                {
                    if (ListaDatosAsientos.Contains(item) == false)
                    {
                        //ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, angulo = item.angulo, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                    }
                }
            }
        }

        public void BusquedaMarcaGuias()
        {
            string marca_final = string.Empty;
            //Resuelvo conflictos de marcas mal escritas
            switch (pckMarca.SelectedItem.ToString())
            {
                case "VOLKSWAGEN":
                    marca_final = "VW";
                    break;
                case "MERCEDES BENZ":
                    marca_final = "M. BENZ";
                    break;
                case "PEUGEOT":
                    marca_final = "PEUG";
                    break;
                case "SCANIA VABIS":
                    marca_final = "SCANIA";
                    break;
                case "CHEVROLET":
                    marca_final = "CHEVROL";
                    break;
                case "BMW":
                    marca_final = "B.M.W";
                    break;
                case "CONTINENTAL":
                    marca_final = "CONTINEN";
                    break;
                default:
                    marca_final = pckMarca.SelectedItem.ToString();
                    break;
            }

            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.marca_modelo.Contains(marca_final) == true)
                {
                    if (ListaDatosGuias.Contains(item) == false)
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }

        public void BusquedaMarcaAsientos()
        {
            string marca_final = string.Empty;
            //Resuelvo conflictos de marcas mal escritas
            switch (pckMarca.SelectedItem.ToString())
            {
                case "VOLKSWAGEN":
                    marca_final = "VW";
                    break;
                case "MERCEDES BENZ":
                    marca_final = "M. BENZ";
                    break;
                case "PEUGEOT":
                    marca_final = "PEUG";
                    break;
                case "SCANIA VABIS":
                    marca_final = "SCANIA";
                    break;
                case "CHEVROLET":
                    marca_final = "CHEVROL";
                    break;
                case "BMW":
                    marca_final = "B.M.W";
                    break;
                case "CONTINENTAL":
                    marca_final = "CONTINEN";
                    break;
                default:
                    marca_final = pckMarca.SelectedItem.ToString();
                    break;
            }

            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.marca_modelo.Contains(marca_final) == true)
                {
                    if (ListaDatosAsientos.Contains(item) == false)
                    {
                        //ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, tipo_aplicacion = item.tipo_aplicacion, tipo_alimentacion = item.tipo_alimentacion, cilindros = item.cilindros, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, angulo = item.angulo, codigo_trw = item.codigo_trw, codigo_sm = item.codigo_sm, codigo_metelli = item.codigo_metelli, codigo_riosulense = item.codigo_riosulense });
                    }
                }
            }
        }

        private int CantidadParametros()
        {
            int CP = 0;

            if (txtNOriginal.Text != null)
            {
                CP += 1;
            }
            if (txtMotor.Text != null)
            {
                CP += 1;
            }
            if (pckMarca.SelectedItem.ToString() != "[ Todos ]")
            {
                CP += 1;
            }
            //if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]")
            //{
            //    CP += 1;
            //}
            if (txtCilindrada.Text != null)
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
                if (txtNOriginal.Text != null)
                {
                    if (txtNOriginal.Text == item.numero_original)
                    {
                        coincidencias += 1;
                    }
                }
                if (txtMotor.Text != null)
                {
                    if (item.marca_modelo.Contains(txtMotor.Text) == true) //Si el campo marca_modelo contiene el motor
                    {
                        coincidencias += 1;
                    }
                }
                if (pckMarca.SelectedItem.ToString() != "[ Todos ]")
                {
                    string marca_final = string.Empty;
                    //Resuelvo conflictos de marcas mal escritas
                    switch (pckMarca.SelectedItem.ToString())
                    {
                        case "VOLKSWAGEN":
                            marca_final = "VW";
                            break;
                        case "MERCEDES BENZ":
                            marca_final = "M. BENZ";
                            break;
                        case "PEUGEOT":
                            marca_final = "PEUG";
                            break;
                        case "SCANIA VABIS":
                            marca_final = "SCANIA";
                            break;
                        case "CHEVROLET":
                            marca_final = "CHEVROL";
                            break;
                        case "BMW":
                            marca_final = "B.M.W";
                            break;
                        case "CONTINENTAL":
                            marca_final = "CONTINEN";
                            break;
                        default:
                            marca_final = pckMarca.SelectedItem.ToString();
                            break;
                    }
                    if (item.marca_modelo.Contains(marca_final) == true)
                    {
                        coincidencias += 1;
                    }
                }

                int cantPar = CantidadParametros();
                if (cantPar == coincidencias)
                {
                    ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Guía", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor});
                }
            }
        }

        public void CoincidenciasAsientos()
        {
            foreach (var item in ListaDatosAsientos)
            {
                int coincidencias = 0;
                if (txtNOriginal.Text != null)
                {
                    if (txtNOriginal.Text == item.numero_original)
                    {
                        coincidencias += 1;
                    }
                }
                if (txtMotor.Text != null)
                {
                    if (item.marca_modelo.Contains(txtMotor.Text) == true) //Si el campo marca_modelo contiene el motor
                    {
                        coincidencias += 1;
                    }
                }
                if (pckMarca.SelectedItem.ToString() != "[ Todos ]")
                {
                    string marca_final = string.Empty;
                    //Resuelvo conflictos de marcas mal escritas
                    switch (pckMarca.SelectedItem.ToString())
                    {
                        case "VOLKSWAGEN":
                            marca_final = "VW";
                            break;
                        case "MERCEDES BENZ":
                            marca_final = "M. BENZ";
                            break;
                        case "PEUGEOT":
                            marca_final = "PEUG";
                            break;
                        case "SCANIA VABIS":
                            marca_final = "SCANIA";
                            break;
                        case "CHEVROLET":
                            marca_final = "CHEVROL";
                            break;
                        case "BMW":
                            marca_final = "B.M.W";
                            break;
                        case "CONTINENTAL":
                            marca_final = "CONTINEN";
                            break;
                        default:
                            marca_final = pckMarca.SelectedItem.ToString();
                            break;
                    }
                    if (item.marca_modelo.Contains(marca_final) == true)
                    {
                        coincidencias += 1;
                    }
                }
                //if (pckTipoAplicacion.SelectedItem.ToString() != "[ Todos ]")
                //{

                //}

                int cantPar = CantidadParametros();
                if (cantPar == coincidencias)
                {
                    ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Asiento", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor});
                }
            }
        }

        private void ListViewAplicaciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (productoElegido == "Guías")
            {
                var aplicacion_guia = e.Item as AplicacionesViewModel;
                Guias guia_encontrada = App.ListaGlobalGuias.Find(x => x.numero_300indy == aplicacion_guia.numero_300indy);
                Navigation.PushAsync(new DetalleAplicacion(guia_encontrada, null));
            }
            else
            {
                var aplicacion_asiento = e.Item as AplicacionesViewModel;
                Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.numero_300indy == aplicacion_asiento.numero_300indy);
                Navigation.PushAsync(new DetalleAplicacion(null, asiento_encontrado));
            }
        }

        private void btnLimpiarAplicaciones_Clicked(object sender, EventArgs e)
        {
            ListaDatos_Final.Clear();
            ListViewAplicaciones.ItemsSource = null;
            ListViewAplicaciones.ItemsSource = ListaDatos_Final;
        }
    }
}