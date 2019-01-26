using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.Negocio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Intercambios : ContentPage
	{
        List<IntercambiosViewModel> ListaDatos_Final = new List<IntercambiosViewModel>(); //Se usa para mostrar los 3 campos necesarios
        List<Guias_Asientos> ListaDatos = new List<Guias_Asientos>(); //Se usa para almacenar la informacion filtrada.
        List<Guias_Asientos> ListaAux = new List<Guias_Asientos>(); //Me traigo TODOS los datos de apphb.

        string fabricante_elegido = null;
        
        public List<string> ListaFabricantes = new List<string>();

        public Intercambios () //Solamente pasa la primera vez que se abre la pag.
		{
			InitializeComponent ();
            indicador_principal.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
            indicador_principal.BindingContext = ListViewDatos;
            indicador_principal.IsRunning = false;
            CargarPicker();
            CargarListaFabricantes();
            ListViewDatos.ItemTapped += ListViewDatos_ItemTapped;
            //BindingContext = new IntercambiosViewModel();
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
            var elemento = e.Item as Guias_Asientos;
            //Crear una nueva navigation page pasandole como parametro el elemento seleccionado.

            //BUSCAR EL PRODUCTO DE INTERCAMBIO PARA EL PRODUCTO ENTERO.
            Navigation.PushAsync(new DetalleIntercambios(elemento));
        }

        public void CargarPicker()
        {
            pckFabricante.Items.Add("[ Todos ]");
            pckFabricante.Items.Add("TRW");
            pckFabricante.Items.Add("METELLI");
            pckFabricante.Items.Add("RIOSULENSE");
            pckFabricante.Items.Add("SM");
            pckFabricante.SelectedIndex = 0;
        }


        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            indicador_principal.IsRunning = true;
            while (ListaAux.Count() == 0)
            {
                ListaAux = App.ListaGlobal;
            }
            indicador_principal.IsRunning = false;
            indicador_principal.IsVisible = false;

            bool HayIntercambio = false;
            bool HayIntercambio_Fabricante = false;
            ListaDatos.Clear();
            ListaDatos_Final.Clear();

            //Filtro por fabricante
            if (pckFabricante.SelectedIndex != 0 && txtIntercambio.Text != null) //Si se selecciono algo en el picker y no dejo en "[ Todos ]"..
            {
                foreach (var item in ListaAux)
                {
                    if (pckFabricante.SelectedItem.ToString() == "TRW")
                    {
                        if (item.Inter_TRW.Contains(txtIntercambio.Text) == true || item.Inter_TRW == txtIntercambio.Text)
                        {
                            if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = "vip" + item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                                HayIntercambio_Fabricante = true;
                            }
                        } 
                    }
                    else
                    {
                        if (pckFabricante.SelectedItem.ToString() == "METELLI")
                        {
                            if (item.Inter_METELLI.Contains(txtIntercambio.Text) == true || item.Inter_METELLI == txtIntercambio.Text)
                            {
                                if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = "vip" + item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                                    HayIntercambio_Fabricante = true;
                                }
                            }
                        }
                        else
                        {
                            if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                            {
                                if (item.Inter_RIOSULENSE.Contains(txtIntercambio.Text) == true || item.Inter_RIOSULENSE == txtIntercambio.Text)
                                {
                                    if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = "vip" + item.Inter_RIOSULENSE, Producto = item.Producto });
                                        HayIntercambio_Fabricante = true;
                                    }
                                }
                            }
                            else
                            {
                                if (pckFabricante.SelectedItem.ToString() == "SM")
                                {
                                    if (item.Inter_SM.Contains(txtIntercambio.Text) == true || item.Inter_SM == txtIntercambio.Text)
                                    {
                                        if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                        {
                                            ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = "vip" + item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
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
                    foreach (var item in ListaAux)
                    {
                        if (pckFabricante.SelectedItem.ToString() == "TRW")
                        {
                            if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                            {
                                ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = "vip" + item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                            }
                        }
                        else
                        {
                            if (pckFabricante.SelectedItem.ToString() == "METELLI")
                            {
                                if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = "vip" + item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                                }
                            }
                            else
                            {
                                if (pckFabricante.SelectedItem.ToString() == "RIOSULENSE")
                                {
                                    if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = "vip" + item.Inter_RIOSULENSE, Producto = item.Producto });
                                    }
                                }
                                else
                                {
                                    if (pckFabricante.SelectedItem.ToString() == "SM")
                                    {
                                        if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                        {
                                            ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = "vip" + item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
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
                        foreach (var item in ListaAux)
                        {
                            if (item.Inter_TRW.Contains(txtIntercambio.Text) == true || item.Inter_TRW == txtIntercambio.Text)
                            {
                                if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                {
                                    ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                                    HayIntercambio = true;
                                }
                            }
                            else
                            {
                                if (item.Inter_METELLI.Contains(txtIntercambio.Text) == true || item.Inter_METELLI == txtIntercambio.Text)
                                {
                                    if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                    {
                                        ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                                        HayIntercambio = true;
                                    }
                                }
                                else
                                {
                                    if (item.Inter_RIOSULENSE.Contains(txtIntercambio.Text) == true || item.Inter_RIOSULENSE == txtIntercambio.Text)
                                    {
                                        if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                        {
                                            ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                                            HayIntercambio = true;
                                        }
                                    }
                                    else
                                    {
                                        if (item.Inter_SM.Contains(txtIntercambio.Text) == true || item.Inter_SM == txtIntercambio.Text)
                                        {
                                            if (ListaDatos.Contains(item) == false) //Si el item no estaba previamente cargado, lo cargo.
                                            {
                                                ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
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
                            foreach (var item in ListaAux)
                            {
                                ListaDatos.Add(new Guias_Asientos { Id = item.Id, Adm_Esc = item.Adm_Esc, Aplicacion_Motor = item.Aplicacion_Motor, Diam_Ext = item.Diam_Ext, Diam_Int = item.Diam_Int, Forma = item.Forma, Largo = item.Largo, Marca_Modelo = item.Marca_Modelo, Material = item.Material, NumOriginal = item.NumOriginal, Num300Indy = item.Num300Indy, Inter_TRW = item.Inter_TRW, Inter_SM = item.Inter_SM, Inter_METELLI = item.Inter_METELLI, Inter_RIOSULENSE = item.Inter_RIOSULENSE, Producto = item.Producto });
                            }
                        }
                    }
                }
            }
            //////// Cargo la informacion filtrada para mostrar en la ListView
            foreach (var item in ListaDatos)
            {
                if (item.Inter_TRW != null && item.Inter_TRW.Contains("vip") == true)
                {
                    ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "TRW", Producto = item.Num300Indy, Intercambio = item.Inter_TRW });
                }
                else
                {
                    if (item.Inter_METELLI != null && item.Inter_METELLI.Contains("vip") == true)
                    {
                        ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "METELLI", Producto = item.Num300Indy, Intercambio = item.Inter_METELLI });
                    }
                    else
                    {
                        if (item.Inter_RIOSULENSE != null && item.Inter_RIOSULENSE.Contains("vip") == true)
                        {
                            ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.Num300Indy, Intercambio = item.Inter_RIOSULENSE });
                        }
                        else
                        {
                            if (item.Inter_SM != null && item.Inter_SM.Contains("vip") == true)
                            {
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "SM", Producto = item.Num300Indy, Intercambio = item.Inter_SM });
                            }
                            else //Ninguno es vip.. muestro todos
                            {
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "TRW", Producto = item.Num300Indy, Intercambio = item.Inter_TRW });
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "METELLI", Producto = item.Num300Indy, Intercambio = item.Inter_METELLI });
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "RIOSULENSE", Producto = item.Num300Indy, Intercambio = item.Inter_RIOSULENSE });
                                ListaDatos_Final.Add(new IntercambiosViewModel { Fabricante = "SM", Producto = item.Num300Indy, Intercambio = item.Inter_SM });
                            }
                        }
                    }
                }
            }
            //Se actualiza el ListView
            ListViewDatos.ItemsSource = null;
            ListViewDatos.ItemsSource = ListaDatos_Final;
 
        }

    }
}