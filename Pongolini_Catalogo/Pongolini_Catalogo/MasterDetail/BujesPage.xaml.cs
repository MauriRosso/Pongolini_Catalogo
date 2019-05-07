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
            CargarTipoBujes();
            btnNuevaBusqueda.IsVisible = false;
            Cargando.IsRunning = true;
            Cargando.IsVisible = false;
            lblCargando.IsVisible = false;
            CargarCarrito();
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

        public void CargarTipoBujes()
        {
            pckTipoBujes.Title = "Seleccione tipo";
            pckTipoBujes.Items.Add("[ Todos ]");
            pckTipoBujes.Items.Add("Fundición gris aleada");
            pckTipoBujes.Items.Add("Templado");
            pckTipoBujes.SelectedIndex = 0;
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
            //filtros
            if (txtDiamExtDesdeDim.Text != null && txtDiamExtDesdeDim.Text != "")
            {
                BusquedaDiametroExteriorBujes();
            }
            if (txtDiamIntDesdeDim.Text != null && txtDiamIntDesdeDim.Text != "")
            {
                BusquedaDiametroInteriorBujes();
            }
            if (txtLargoDesdeDim.Text != null && txtLargoDesdeDim.Text != "")
            {
                BusquedaLargo();
            }
            if (pckTipoBujes.SelectedItem.ToString() != "[ Todos ]")
            {
                BusquedaMaterial();
            }

            if ((txtDiamExtDesdeDim.Text == null || txtDiamExtDesdeDim.Text == "") && (txtDiamIntDesdeDim.Text == null || txtDiamIntDesdeDim.Text == "") && (txtLargoDesdeDim.Text == null || txtLargoDesdeDim.Text == "") && (pckTipoBujes.SelectedItem.ToString() == "[ Todos ]"))
            {
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
            }
            else
            {
                CoincidenciasBujes();
            }

            if (ListaDatos_Final.Count() == 0)
            {
                DisplayAlert("Error", "No se encontró ningún elemento con los parámetros especificados.", "OK");
            }
            ListViewBujes.ItemsSource = null;
            ListViewBujes.ItemsSource = ListaDatos_Final;

        }

        private void BusquedaDiametroExteriorBujes()
        {
            foreach (var item in App.ListaGlobalBujes)
            {
                if (item.diametro_exterior >= Convert.ToDouble(txtDiamExtDesdeDim.Text) && item.diametro_exterior <= Convert.ToDouble(txtDiamExtHastaDim.Text))
                {
                    if (ListaDatosBujes.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosBujes.Add(new Bujes { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo });
                    }
                }
            }
        }

        private void BusquedaDiametroInteriorBujes()
        {
            foreach (var item in App.ListaGlobalBujes)
            {
                if (item.diametro_interior >= Convert.ToDouble(txtDiamIntDesdeDim.Text) && item.diametro_interior <= Convert.ToDouble(txtDiamIntHastaDim.Text))
                {
                    if (ListaDatosBujes.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosBujes.Add(new Bujes { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo });
                    }
                }
            }
        }

        private void BusquedaLargo()
        {
            foreach (var item in App.ListaGlobalBujes)
            {
                if (item.largo >= Convert.ToDouble(txtLargoDesdeDim.Text) && item.largo <= Convert.ToDouble(txtLargoHastaDim.Text))
                {
                    if (ListaDatosBujes.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosBujes.Add(new Bujes { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo });
                    }
                }
            }
        }

        private void BusquedaMaterial()
        {
            string mat_elegido = string.Empty;

            if (pckTipoBujes.SelectedItem.ToString() == "Fundición gris aleada")
            {
                mat_elegido = "B";
            }
            else
            {
                if (pckTipoBujes.SelectedItem.ToString() == "Templado")
                {
                    mat_elegido = "BT";
                }
            }
            foreach (var item in App.ListaGlobalBujes)
            {
                string linea_aux = item.codigo;
                string linea = linea_aux.Remove(2); //remueve todos los caracteres a partir del 2.
                if (linea == mat_elegido)
                {
                    if (ListaDatosBujes.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                    {
                        ListaDatosBujes.Add(new Bujes { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo });
                    }
                }
                else
                {
                    if (mat_elegido == "B") //Como me quedaria un B4, B5.. lo comparo de otra forma (siempre y cuando sea un FGris)
                    {
                        if (linea != "BT")
                        {
                            if (ListaDatosBujes.Exists(x => x.codigo == item.codigo) == false) //Si NO existe el elemento.. lo agrego.
                            {
                                ListaDatosBujes.Add(new Bujes { codigo = item.codigo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, largo = item.largo });
                            }
                        }
                    }
                }
            }
        }

        private void CoincidenciasBujes()
        {
            foreach (var item in ListaDatosBujes)
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
                if (pckTipoBujes.SelectedItem.ToString() != "[ Todos ]")
                {
                    string mat_elegido = string.Empty;

                    if (pckTipoBujes.SelectedItem.ToString() == "Fundición gris aleada")
                    {
                        mat_elegido = "B";
                    }
                    else
                    {
                        if (pckTipoBujes.SelectedItem.ToString() == "Templado")
                        {
                            mat_elegido = "BT";
                        }
                    }

                    string linea_aux = item.codigo;
                    string linea = linea_aux.Remove(2); //remueve todos los caracteres a partir del 2.
                    if (linea == mat_elegido)
                    {
                        coincidencias += 1;
                    }
                    else
                    {
                        if (mat_elegido == "B") //Como me quedaria un B4, B5.. lo comparo de otra forma (siempre y cuando sea un FGris)
                        {
                            if (linea != "BT")
                            {
                                coincidencias += 1;
                            }
                        }
                    }

                }
                int cantPar = CantidadParametrosBujes();
                if (cantPar == coincidencias)
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
            }
        }

        private int CantidadParametrosBujes()
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
            if (pckTipoBujes.SelectedItem.ToString() != "[ Todos ]")
            {
                CP += 1;
            }
            return CP;
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

        private void ListViewBujes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var buje = e.Item as BujesViewModel;
            Bujes buje_encontrado = App.ListaGlobalBujes.Find(x => x.codigo == buje.codigo);
            Navigation.PushAsync(new DetalleBuje(buje_encontrado));
        }

        private async void btnBuscarBujes_Clicked(object sender, EventArgs e)
        {
            if (!ConexionRevisada()) //El dispositivo no soporta el plugin, no puedo controlarlo.
            {
                await DisplayAlert("Estado de la conexión", "No hemos podido comprobar el estado de tu conexión a internet. Por favor, asegúrate de estar conectado a la red antes de realizar una búsqueda.", "OK");
            }                     
            if (TieneConexion())
            {
                //Oculto la busqueda para mostrar con mas espacio la listview
                OcultarCamposBujes();
                ListViewBujes.ItemsSource = null;
                ListViewBujes.ItemsSource = ListaDatos_Final;
                ObtenerBujes();
            }
            else
            {
                await DisplayAlert("Error de conexión", "No estás conectado a internet o tu señal es muy debil. Por favor, reintenta más tarde.", "OK");
            }
        }

        private void btnLimpiarBujes_Clicked(object sender, EventArgs e)
        {
            pckTipoBujes.SelectedIndex = 0;
            txtDiamExtDesdeDim.Text = null;
            txtDiamExtHastaDim.Text = null;
            txtDiamIntDesdeDim.Text = null;
            txtDiamIntHastaDim.Text = null;
            txtLargoDesdeDim.Text = null;
            txtLargoHastaDim.Text = null;
            ListaDatos_Final.Clear();
            ListViewBujes.ItemsSource = null;
            ListViewBujes.ItemsSource = ListaDatos_Final;
        }

        private void btnNuevaBusqueda_Clicked(object sender, EventArgs e)
        {
            btnNuevaBusqueda.IsVisible = false;
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = true;
        }

        public void OcultarCamposBujes()
        {
            var stackBusqueda = this.FindByName<StackLayout>("StackCamposBusqueda");
            stackBusqueda.IsVisible = false;
            btnNuevaBusqueda.IsVisible = true;
        }

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }
    }
}