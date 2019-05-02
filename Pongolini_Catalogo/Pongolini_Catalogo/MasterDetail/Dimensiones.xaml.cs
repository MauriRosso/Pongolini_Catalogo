using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pongolini_Catalogo.Negocio;
using Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dimensiones : ContentPage
    {
        List<DimensionesViewModel> ListaDatos_Final = new List<DimensionesViewModel>(); //Informacion que se muestra en la listview.
        List<Guias> ListaDatosGuias = new List<Guias>(); //Almaceno info filtrada de guias
        List<Asientos> ListaDatosAsientos = new List<Asientos>(); //Almaceno info filtrada de asientos
        string productoElegido = string.Empty;

        public Dimensiones()
        {
            InitializeComponent();
            CargarPickerProducto();
            CargarPickerMaterial();
            btnNuevaBusqueda.IsVisible = false;
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            App.MasterD.Detail = new NavigationPage(new Inicio());
            return true;
        }

        public void CargarPickerProducto()
        {
            pckProductoDim.Items.Add("Guías de válvulas");
            pckProductoDim.Items.Add("Asientos de válvulas");
            pckProductoDim.SelectedIndex = 0;
            lblAnguloDim.IsVisible = false;
            txtAnguloDesdeDim.IsVisible = false;
            HastaAngulo.IsVisible = false;
            txtAnguloHastaDim.IsVisible = false;
            lblAltoDim.IsVisible = false;
            txtAltoDesdeDim.IsVisible = false;
            HastaAlto.IsVisible = false;
            txtAltoHastaDim.IsVisible = false;
        }

        public void CargarPickerMaterial()
        {
            pckMaterialDim.Items.Add("[ Todos ]");
            pckMaterialDim.Items.Add("Fundición gris");
            pckMaterialDim.Items.Add("Bronce");
            pckMaterialDim.SelectedIndex = 0;
        }

        private void pckProductoDim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pckProductoDim.SelectedIndex == 0) //Selecciono Guias
            {
                lblMaterialDim.IsVisible = true;
                pckMaterialDim.IsVisible = true;
                lblLargoDim.IsVisible = true;
                txtLargoDesdeDim.IsVisible = true;
                HastaLargo.IsVisible = true;
                txtLargoHastaDim.IsVisible = true;

                lblAnguloDim.IsVisible = false;
                txtAnguloDesdeDim.IsVisible = false;
                HastaAngulo.IsVisible = false;
                txtAnguloHastaDim.IsVisible = false;
                lblAltoDim.IsVisible = false;
                txtAltoDesdeDim.IsVisible = false;
                HastaAlto.IsVisible = false;
                txtAltoHastaDim.IsVisible = false;
            }
            else //Selecciono Asientos
            {
                if (pckProductoDim.SelectedIndex == 1)
                {
                    lblMaterialDim.IsVisible = false;
                    pckMaterialDim.IsVisible = false;
                    lblLargoDim.IsVisible = false;
                    txtLargoDesdeDim.IsVisible = false;
                    HastaLargo.IsVisible = false;
                    txtLargoHastaDim.IsVisible = false;

                    lblAnguloDim.IsVisible = true;
                    txtAnguloDesdeDim.IsVisible = true;
                    HastaAngulo.IsVisible = true;
                    txtAnguloHastaDim.IsVisible = true;
                    lblAltoDim.IsVisible = true;
                    txtAltoDesdeDim.IsVisible = true;
                    HastaAlto.IsVisible = true;
                    txtAltoHastaDim.IsVisible = true;
                }
            }
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

            ListaDatosGuias.Clear();
            ListaDatos_Final.Clear();
            //filtros
            if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
            {
                BusquedaDiametroExteriorGuias();
            }
            if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
            {
                BusquedaDiametroInteriorGuias();
            }
            if (txtLargoDesdeDim.Text != null && txtLargoDesdeDim.Text != "")
            {
                BusquedaLargo();
            }
            if (pckMaterialDim.SelectedItem.ToString() != "[ Todos ]")
            {
                BusquedaMaterial();
            }

            if ((txtDiamExtDesdeDim.Text == null || txtDiamExtDesdeDim.Text == "") && (txtDiamIntDesdeDim.Text == null || txtDiamIntDesdeDim.Text == "") && (txtLargoDesdeDim.Text == null || txtLargoDesdeDim.Text == "") && pckMaterialDim.SelectedItem.ToString() == "[ Todos ]")
            {
                //Mostrar TODOS los datos.
                foreach (var item in App.ListaGlobalGuias)
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
            else
            {
                CoincidenciasGuias();
            }

            if (ListaDatos_Final.Count() == 0)
            {
                DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
            }
            ListViewDimensiones.ItemsSource = null;
            ListViewDimensiones.ItemsSource = ListaDatos_Final;

        }

        private void BusquedaDiametroExteriorGuias()
        {
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.diametro_exterior >= Convert.ToDouble(txtDiamExtDesdeDim.Text) && item.diametro_exterior <= Convert.ToDouble(txtDiamExtHastaDim.Text))
                {
                    if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaDiametroExteriorAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.diametro_exterior >= Convert.ToDouble(txtDiamExtDesdeDim.Text) && item.diametro_exterior <= Convert.ToDouble(txtDiamExtHastaDim.Text))
                {
                    if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaDiametroInteriorGuias()
        {
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.diametro_interior >= Convert.ToDouble(txtDiamIntDesdeDim.Text) && item.diametro_interior <= Convert.ToDouble(txtDiamIntHastaDim.Text))
                {
                    if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaDiametroInteriorAsientos()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.diametro_interior >= Convert.ToDouble(txtDiamIntDesdeDim.Text) && item.diametro_interior <= Convert.ToDouble(txtDiamIntHastaDim.Text))
                {
                    if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaLargo()
        {
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.largo >= Convert.ToDouble(txtLargoDesdeDim.Text) && item.largo <= Convert.ToDouble(txtLargoHastaDim.Text))
                {
                    if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaAlto()
        {
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.largo >= Convert.ToDouble(txtAltoDesdeDim.Text) && item.largo <= Convert.ToDouble(txtAltoHastaDim.Text))
                {
                    if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaMaterial()
        {
            string mat = string.Empty;
            if (pckMaterialDim.SelectedItem.ToString() == "Fundición gris")
            {
                mat = "FG";
            }
            else
            {
                if (pckMaterialDim.SelectedItem.ToString() == "Bronce")
                {
                    mat = "B";
                }
            }
            foreach (var item in App.ListaGlobalGuias)
            {
                if (item.material == mat)
                {
                    if (ListaDatosGuias.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosGuias.Add(new Guias { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, material = item.material, forma = item.forma, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                    }
                }
            }
        }
        private void BusquedaAngulo()
        {
            double angulo = double.NaN;
            foreach (var item in App.ListaGlobalAsientos)
            {
                if (item.angulo != null && item.angulo != "" && item.angulo != "-") //Angulo en formato ej: 45°
                {
                    angulo = TransformarAngulo(item.angulo);
                    if (angulo >= Convert.ToDouble(txtAnguloDesdeDim.Text) && angulo <= Convert.ToDouble(txtAnguloHastaDim.Text))
                    {
                        if (ListaDatosAsientos.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                        {
                            ListaDatosAsientos.Add(new Asientos { codigo = item.codigo, marca_modelo = item.marca_modelo, motor = item.motor, numero_original = item.numero_original, numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo, angulo = item.angulo, codigo_riosulense = item.codigo_riosulense, codigo_mahle = item.codigo_mahle });
                        }
                    }
                }
            }
        }

        private double TransformarAngulo(string ang)
        {
            double angulo = double.NaN;
            ang = ang.Remove(ang.Length - 1);
            angulo = Convert.ToDouble(ang);
            return angulo;
        }
        private void CoincidenciasGuias()
        {
            foreach (var item in ListaDatosGuias)
            {
                int coincidencias = 0;
                if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
                {
                    if (item.diametro_exterior >= Convert.ToDouble(txtDiamExtDesdeDim.Text) && item.diametro_exterior <= Convert.ToDouble(txtDiamExtHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }
                if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
                {
                    if (item.diametro_interior >= Convert.ToDouble(txtDiamIntDesdeDim.Text) && item.diametro_interior <= Convert.ToDouble(txtDiamIntHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }
                if (txtLargoDesdeDim.Text != null && txtLargoHastaDim.Text != "")
                {
                    if (item.largo >= Convert.ToDouble(txtLargoDesdeDim.Text) && item.largo <= Convert.ToDouble(txtLargoHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }
                if (pckMaterialDim.SelectedItem.ToString() != "[ Todos ]")
                {
                    string mat = string.Empty;
                    if (pckMaterialDim.SelectedItem.ToString() == "Fundición gris")
                    {
                        mat = "FG";
                    }
                    else
                    {
                        if (pckMaterialDim.SelectedItem.ToString() == "Bronce")
                        {
                            mat = "B";
                        }
                    }
                    if (item.material == mat)
                    {
                        coincidencias += 1;
                    }
                }
                int cantPar = CantidadParametrosGuias();
                if (cantPar == coincidencias)
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

        private async void ObtenerAsientos()
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
            else //Si alguna vez ya trajo los datos, simplemente se los asigno.
            {
                SepararAsientosSemiTerminados();
            }
            ListaDatosAsientos.Clear();
            ListaDatos_Final.Clear();
            //filtros

            if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
            {
                BusquedaDiametroExteriorAsientos();
            }
            if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
            {
                BusquedaDiametroInteriorAsientos();
            }
            if (txtAltoDesdeDim.Text != null && txtAltoDesdeDim.Text != "")
            {
                BusquedaAlto();
            }
            if (txtAnguloDesdeDim.Text != null && txtAnguloDesdeDim.Text != "")
            {
                BusquedaAngulo();
            }

            if ((txtDiamExtDesdeDim.Text == null || txtDiamExtDesdeDim.Text == "") && (txtDiamIntDesdeDim.Text == null || txtDiamIntDesdeDim.Text == "") && (txtAltoDesdeDim.Text == null || txtAltoDesdeDim.Text == "") && (txtAnguloDesdeDim.Text == null || txtAnguloDesdeDim.Text == ""))
            {
                //Mostrar TODOS los datos.
                foreach (var item in App.ListaGlobalAsientos)
                {
                    ListaDatos_Final.Add(new DimensionesViewModel { producto = "Asiento", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.angulo, codigo = item.codigo, img_diam_ext = "Diametro_Exterior_Asiento.png", img_diam_int = "Diametro_Interior_Asiento.png", img_largo_alto = "Altura.png", img_angulo_material = "Angulo.png" });
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
            ListViewDimensiones.ItemsSource = null;
            ListViewDimensiones.ItemsSource = ListaDatos_Final;
        }

        private void CoincidenciasAsientos()
        {
            foreach (var item in ListaDatosAsientos)
            {
                int coincidencias = 0;
                if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
                {
                    if (item.diametro_exterior >= Convert.ToDouble(txtDiamExtDesdeDim.Text) && item.diametro_exterior <= Convert.ToDouble(txtDiamExtHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }
                if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
                {
                    if (item.diametro_interior >= Convert.ToDouble(txtDiamIntDesdeDim.Text) && item.diametro_interior <= Convert.ToDouble(txtDiamIntHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }
                if (txtAltoDesdeDim.Text != null && txtAltoHastaDim.Text != "")
                {
                    if (item.largo >= Convert.ToDouble(txtAltoDesdeDim.Text) && item.largo <= Convert.ToDouble(txtAltoHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }

                double angulo = double.NaN;
                if (item.angulo != null && item.angulo != "" && item.angulo != "-") //Angulo en formato ej: 45°
                {
                    angulo = TransformarAngulo(item.angulo);
                    if (angulo >= Convert.ToDouble(txtAnguloDesdeDim.Text) && angulo <= Convert.ToDouble(txtAnguloHastaDim.Text))
                    {
                        coincidencias += 1;
                    }
                }

                int cantPar = CantidadParametrosAsientos();
                if (cantPar == coincidencias)
                {
                    ListaDatos_Final.Add(new DimensionesViewModel { producto = "Asiento", marca_modelo = item.marca_modelo, motor = item.motor, numero_300indy = item.numero_300indy, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo_alto = item.largo, angulo_material = item.angulo, codigo = item.codigo, img_diam_ext = "Diametro_Exterior_Asiento.png", img_diam_int = "Diametro_Interior_Asiento.png", img_largo_alto = "Altura.png", img_angulo_material = "Angulo.png" });
                }
            }
        }

        private int CantidadParametrosGuias()
        {
            int CP = 0;

            if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
            {
                CP += 1;
            }
            if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
            {
                CP += 1;
            }
            if (txtLargoDesdeDim.Text != null && txtLargoDesdeDim.Text != "")
            {
                CP += 1;
            }
            if (pckMaterialDim.SelectedItem.ToString() != "[ Todos ]")
            {
                CP += 1;
            }
            if (txtAnguloDesdeDim.Text != null && txtAnguloDesdeDim.Text != "")
            {
                CP += 1;
            }
            return CP;
        }

        private int CantidadParametrosAsientos()
        {
            int CP = 0;

            if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
            {
                CP += 1;
            }
            if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
            {
                CP += 1;
            }
            if (txtAltoDesdeDim.Text != null && txtAltoDesdeDim.Text != "")
            {
                CP += 1;
            }
            if (txtAnguloDesdeDim.Text != null && txtAnguloDesdeDim.Text != "")
            {
                CP += 1;
            }

            return CP;
        }

        private void btnBuscarDimensiones_Clicked(object sender, EventArgs e)
        {
            //Oculto la busqueda para mostrar con mas espacio la listview
            OcultarCamposDimensiones();
            ListViewDimensiones.ItemsSource = null;
            ListViewDimensiones.ItemsSource = ListaDatos_Final;
            if (pckProductoDim.SelectedItem.ToString() == "Guías de válvulas")
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

        public void OcultarCamposDimensiones()
        {
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = false;
            btnNuevaBusqueda.IsVisible = true;
        }

        private void txtDiamExtDesdeDim_Unfocused(object sender, FocusEventArgs e)
        {
            //Si el campo de la derecha esta vacio o su valor es menor al de la izquierda.. lo cambio.
            if (txtDiamExtHastaDim.Text == null || txtDiamExtHastaDim.Text == "")
            {
                txtDiamExtHastaDim.Text = txtDiamExtDesdeDim.Text;
            }
            else //El hasta es un numero comparable
            {
                txtDiamExtHastaDim.Text = txtDiamExtHastaDim.Text.Replace('.', ',');
                if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")  //El desde es un numero comparable
                {
                    txtDiamExtDesdeDim.Text = txtDiamExtDesdeDim.Text.Replace('.', ',');
                    if (txtDiamExtDesdeDim.Text[txtDiamExtDesdeDim.Text.Length - 1].ToString() == ",")
                    {
                        //Si el ultimo caracter es una ,
                        txtDiamExtDesdeDim.Text = txtDiamExtDesdeDim.Text.Remove(txtDiamExtDesdeDim.Text.Length - 1);
                    }
                    if (Convert.ToDecimal(txtDiamExtHastaDim.Text) < Convert.ToDecimal(txtDiamExtDesdeDim.Text))
                    {
                        txtDiamExtHastaDim.Text = txtDiamExtDesdeDim.Text;
                    }
                }
                else //Dejo el desde vacio
                {
                    txtDiamExtHastaDim.Text = txtDiamExtDesdeDim.Text;
                }
                if (txtDiamExtHastaDim.Text != null && txtDiamExtHastaDim.Text != "")
                {
                    if (txtDiamExtHastaDim.Text[txtDiamExtHastaDim.Text.Length - 1].ToString() == "," || (txtDiamExtHastaDim.Text[txtDiamExtHastaDim.Text.Length - 1]).ToString() == ".")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtDiamExtHastaDim.Text = txtDiamExtHastaDim.Text.Remove(txtDiamExtHastaDim.Text.Length - 1);
                    }
                }
            }
        }

        private void txtDiamExtDesdeDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtDiamExtDesdeDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtDiamExtHastaDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtDiamExtHastaDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtDiamIntDesdeDim_Unfocused(object sender, FocusEventArgs e)
        {
            //Si el campo de la derecha esta vacio o su valor es menor al de la izquierda.. lo cambio.
            if (txtDiamIntHastaDim.Text == null || txtDiamIntHastaDim.Text == "")
            {
                txtDiamIntHastaDim.Text = txtDiamIntDesdeDim.Text;
            }
            else //El hasta es un numero comparable
            {
                txtDiamIntHastaDim.Text = txtDiamIntHastaDim.Text.Replace('.', ',');
                if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")  //El desde es un numero comparable
                {
                    txtDiamIntDesdeDim.Text = txtDiamIntDesdeDim.Text.Replace('.', ',');
                    if (txtDiamIntDesdeDim.Text[txtDiamIntDesdeDim.Text.Length - 1].ToString() == ",")
                    {
                        //Si el ultimo caracter es una ,
                        txtDiamIntDesdeDim.Text = txtDiamIntDesdeDim.Text.Remove(txtDiamIntDesdeDim.Text.Length - 1);
                    }
                    if (Convert.ToDecimal(txtDiamIntHastaDim.Text) < Convert.ToDecimal(txtDiamIntDesdeDim.Text))
                    {
                        txtDiamIntHastaDim.Text = txtDiamIntDesdeDim.Text;
                    }
                }
                else //Dejo el desde vacio
                {
                    txtDiamIntHastaDim.Text = txtDiamIntDesdeDim.Text;
                }
                if (txtDiamIntHastaDim.Text != null && txtDiamIntHastaDim.Text != "")
                {
                    if (txtDiamIntHastaDim.Text[txtDiamIntHastaDim.Text.Length - 1].ToString() == "," || (txtDiamIntHastaDim.Text[txtDiamIntHastaDim.Text.Length - 1]).ToString() == ".")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtDiamIntHastaDim.Text = txtDiamIntHastaDim.Text.Remove(txtDiamIntHastaDim.Text.Length - 1);
                    }
                }
            }
        }

        private void txtDiamIntDesdeDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtDiamIntDesdeDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtDiamIntHastaDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtDiamIntHastaDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtAnguloDesdeDim_Unfocused(object sender, FocusEventArgs e)
        {
            //Si el campo de la derecha esta vacio o su valor es menor al de la izquierda.. lo cambio.
            if (txtAnguloHastaDim.Text == null || txtAnguloHastaDim.Text == "")
            {
                txtAnguloHastaDim.Text = txtAnguloDesdeDim.Text;
            }
            else //El hasta es un numero comparable
            {
                txtAnguloHastaDim.Text = txtAnguloHastaDim.Text.Replace('.', ',');
                if (txtAnguloDesdeDim.Text != null && txtAnguloDesdeDim.Text != "")  //El desde es un numero comparable
                {
                    txtAnguloDesdeDim.Text = txtAnguloDesdeDim.Text.Replace('.', ',');
                    if (txtAnguloDesdeDim.Text[txtAnguloDesdeDim.Text.Length - 1].ToString() == ",")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtAnguloDesdeDim.Text = txtAnguloDesdeDim.Text.Remove(txtAnguloDesdeDim.Text.Length - 1);
                    }
                    if (Convert.ToDecimal(txtAnguloHastaDim.Text) < Convert.ToDecimal(txtAnguloDesdeDim.Text))
                    {
                        txtAnguloHastaDim.Text = txtAnguloDesdeDim.Text;
                    }
                }
                else //Dejo el desde vacio
                {
                    txtAnguloHastaDim.Text = txtAnguloDesdeDim.Text;
                }
                if (txtAnguloHastaDim.Text != null && txtAnguloHastaDim.Text != "")
                {
                    if (txtAnguloHastaDim.Text[txtAnguloHastaDim.Text.Length - 1].ToString() == "," || (txtAnguloHastaDim.Text[txtAnguloHastaDim.Text.Length - 1]).ToString() == ".")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtAnguloHastaDim.Text = txtAnguloHastaDim.Text.Remove(txtAnguloHastaDim.Text.Length - 1);
                    }
                }
            }
        }

        private void txtAnguloDesdeDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtAnguloDesdeDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtAnguloHastaDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtAnguloHastaDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtAltoDesdeDim_Unfocused(object sender, FocusEventArgs e)
        {
            //Si el campo de la derecha esta vacio o su valor es menor al de la izquierda.. lo cambio.
            if (txtAltoHastaDim.Text == null || txtAltoHastaDim.Text == "")
            {
                txtAltoHastaDim.Text = txtAltoDesdeDim.Text;
            }
            else //El hasta es un numero comparable
            {
                txtAltoHastaDim.Text = txtAltoHastaDim.Text.Replace('.', ',');
                if (txtAltoDesdeDim.Text != null && txtAltoDesdeDim.Text != "")  //El desde es un numero comparable
                {
                    txtAltoDesdeDim.Text = txtAltoDesdeDim.Text.Replace('.', ',');
                    if (txtAltoDesdeDim.Text[txtAltoDesdeDim.Text.Length - 1].ToString() == ",")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtAltoDesdeDim.Text = txtAltoDesdeDim.Text.Remove(txtAltoDesdeDim.Text.Length - 1);
                    }
                    if (Convert.ToDecimal(txtAltoHastaDim.Text) < Convert.ToDecimal(txtAltoDesdeDim.Text))
                    {
                        txtAltoHastaDim.Text = txtAltoDesdeDim.Text;
                    }
                }
                else //Dejo el desde vacio
                {
                    txtAltoHastaDim.Text = txtAltoDesdeDim.Text;
                }
                if (txtAltoHastaDim.Text != null && txtAltoHastaDim.Text != "")
                {
                    if (txtAltoHastaDim.Text[txtAltoHastaDim.Text.Length - 1].ToString() == "," || (txtAltoHastaDim.Text[txtAltoHastaDim.Text.Length - 1]).ToString() == ".")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtAltoHastaDim.Text = txtAltoHastaDim.Text.Remove(txtAltoHastaDim.Text.Length - 1);
                    }
                }
            }
        }

        private void txtAltoDesdeDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtAltoDesdeDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtAltoHastaDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtAltoHastaDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtLargoDesdeDim_Unfocused(object sender, FocusEventArgs e)
        {
            //Si el campo de la derecha esta vacio o su valor es menor al de la izquierda.. lo cambio.
            if (txtLargoHastaDim.Text == null || txtLargoHastaDim.Text == "")
            {
                txtLargoHastaDim.Text = txtLargoDesdeDim.Text;
            }
            else //El hasta es un numero comparable
            {
                txtLargoHastaDim.Text = txtLargoHastaDim.Text.Replace('.', ',');
                if (txtLargoDesdeDim.Text != null && txtLargoDesdeDim.Text != "")  //El desde es un numero comparable
                {
                    txtLargoDesdeDim.Text = txtLargoDesdeDim.Text.Replace('.', ',');
                    if (txtLargoDesdeDim.Text[txtLargoDesdeDim.Text.Length - 1].ToString() == ",")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtLargoDesdeDim.Text = txtLargoDesdeDim.Text.Remove(txtLargoDesdeDim.Text.Length - 1);
                    }
                    if (Convert.ToDecimal(txtLargoHastaDim.Text) < Convert.ToDecimal(txtLargoDesdeDim.Text))
                    {
                        txtLargoHastaDim.Text = txtLargoDesdeDim.Text;
                    }
                }
                else //Dejo el desde vacio
                {
                    txtLargoHastaDim.Text = txtLargoDesdeDim.Text;
                }
                if (txtLargoHastaDim.Text != null && txtLargoHastaDim.Text != "")
                {
                    if (txtLargoHastaDim.Text[txtLargoHastaDim.Text.Length - 1].ToString() == "," || (txtLargoHastaDim.Text[txtLargoHastaDim.Text.Length - 1]).ToString() == ".")
                    {
                        //Si el ultimo caracter es un . o una ,
                        txtLargoHastaDim.Text = txtLargoHastaDim.Text.Remove(txtLargoHastaDim.Text.Length - 1);
                    }
                }
            }
        }

        private void txtLargoDesdeDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtLargoDesdeDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void txtLargoHastaDim_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Se controla el caracter de los campos numericos
            string nuevaCadena = e.NewTextValue;
            if (nuevaCadena != null)
            {
                for (int i = 0; i < (nuevaCadena.Length); i++)
                {
                    if (nuevaCadena[i] != '.' && nuevaCadena[i] != ',' && char.IsDigit(nuevaCadena[i]) == false)
                    {
                        //Y tampoco es un . o ,
                        txtLargoHastaDim.Text = e.OldTextValue;
                        break;
                    }
                }
            }
        }

        private void btnNuevaBusqueda_Clicked(object sender, EventArgs e)
        {
            btnNuevaBusqueda.IsVisible = false;
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = true;
        }

        private void btnLimpiarDimensiones_Clicked(object sender, EventArgs e)
        {
            ListaDatos_Final.Clear();
            ListViewDimensiones.ItemsSource = null;
            ListViewDimensiones.ItemsSource = ListaDatos_Final;
            txtDiamExtDesdeDim.Text = string.Empty;
            txtDiamExtHastaDim.Text = string.Empty;
            txtDiamIntDesdeDim.Text = string.Empty;
            txtDiamIntHastaDim.Text = string.Empty;
            txtAnguloDesdeDim.Text = string.Empty;
            txtAnguloHastaDim.Text = string.Empty;
            txtAltoDesdeDim.Text = string.Empty;
            txtAltoHastaDim.Text = string.Empty;
            txtLargoDesdeDim.Text = string.Empty;
            txtLargoHastaDim.Text = string.Empty;
            pckMaterialDim.SelectedIndex = 0;

        }

        private void ListViewDimensiones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
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
                    if (dimension_asiento.marca_modelo == "ADAPTACIONES")
                    {
                        Asientos asiento_encontrado = App.ListaGlobalSerie6000.Find(x => x.codigo == dimension_asiento.codigo);
                        Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
                    }
                    else
                    {
                        Asientos asiento_encontrado = App.ListaGlobalAsientos.Find(x => x.codigo == dimension_asiento.codigo);
                        Navigation.PushAsync(new DetalleProducto(null, asiento_encontrado));
                    }
                }
            }
        }

        private void ListViewSerie6000_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}