using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using Rg.Plugins.Popup.Services;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarroDeCompras : ContentPage
	{
        List<CarroViewModel> ListaDatos_Final = new List<CarroViewModel>(); //Informacion que se muestra en la listview.
        //PedidoViewModel pv = new PedidoViewModel();

        public CarroDeCompras ()
		{
			InitializeComponent ();
            //ListViewPedidos.ItemsSource = pv.Pedidos;
            CargarTextoLabels();
            if (App.ListaGlobalProductos.Count > 0)
            {
                CargarListaCarrito();
                btnRealizarPedido.IsVisible = true;
                lblSinPedidos.IsVisible = false;
                btnVaciarCarrito.IsVisible = true;
            }
            else
            {
                CarroVacio();
            }
        }

        private void CargarTextoLabels()
        {
            if (App.Idioma != "ES")
            {
                lblSinPedidos.Text = "You have not made any order yet. You orders will appear here";
                Title = "My orders";
                btnRealizarPedido.Text = "Ask for quote";
                btnVaciarCarrito.Text = "Empty cart";
                
            }
            else
            {
                lblSinPedidos.Text = "Todavía no has realizado ningún pedido. Tus pedidos realizados aparecerán aquí.";
            }
        }

        private void CarroVacio()
        {
            btnRealizarPedido.IsVisible = false;
            lblSinPedidos.IsVisible = true;
            btnVaciarCarrito.IsVisible = false;
        }

        private void CargarListaCarrito()
        {
            ListViewCarrito.ItemsSource = null;
            foreach (CarroViewModel item in App.ListaGlobalProductos)
            {
                if (item.producto == "GUÍA")
                {
                    ListaDatos_Final.Add(new CarroViewModel { producto = item.producto, numero_300indy = item.numero_300indy, largo = item.largo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, imagen = "Guias_Carrito.jpg", cantidad = item.cantidad });
                }
                else
                {
                    if (item.producto == "ASIENTO")
                    {
                        ListaDatos_Final.Add(new CarroViewModel { producto = item.producto, numero_300indy = item.numero_300indy, largo = item.largo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, imagen = "Asientos_Carrito.jpg", cantidad = item.cantidad });
                    }
                    else
                    {
                        ListaDatos_Final.Add(new CarroViewModel { producto = "BUJE", numero_300indy = item.numero_300indy, largo = item.largo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, imagen = "img_bujes_recortada.jpg", cantidad = item.cantidad });
                    }
                }
            }
            ListViewCarrito.ItemsSource = null;
            ListViewCarrito.ItemsSource = ListaDatos_Final;
        }

        protected override bool OnBackButtonPressed()
        {
            App.MasterD.Detail = new NavigationPage(new Inicio());
            return true;
        }

        private async void btnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupModal(App.ListaGlobalProductos)); //Le pido la cantidad             
        }

        private async void btnVaciarCarrito_Clicked(object sender, EventArgs e)
        {
            if (App.Idioma == "ES")
            {
                var respuesta = await DisplayAlert("Vaciar carrito", "¿Está seguro que desea vaciar el carrito de pedidos?", "SI", "NO");
                if (respuesta == true)
                {
                    App.ListaGlobalProductos.Clear();
                    ListaDatos_Final.Clear();
                    ListViewCarrito.ItemsSource = null;
                    ListViewCarrito.ItemsSource = ListaDatos_Final;
                    CarroVacio();
                }
            }
            else
            {
                var respuesta = await DisplayAlert("Empty cart", "Are you sure you want to empty the order cart?", "YES", "NO");
                if (respuesta == true)
                {
                    App.ListaGlobalProductos.Clear();
                    ListaDatos_Final.Clear();
                    ListViewCarrito.ItemsSource = null;
                    ListViewCarrito.ItemsSource = ListaDatos_Final;
                    CarroVacio();
                }
            }            
        }

        private async void btnBorrarProducto_Clicked(object sender, EventArgs e)
        {
            if (App.Idioma == "ES")
            {
                var respuesta = await DisplayAlert("Quitar producto", "¿Está seguro que desea quitar el producto seleccionado del pedido?", "SI", "NO");
                if (respuesta == true)
                {
                    var button = sender as Button;
                    var pedido = button?.BindingContext as CarroViewModel;
                    //Borro el producto

                    ListaDatos_Final.Remove(ListaDatos_Final.Find(x => x.numero_300indy == pedido.numero_300indy));
                    App.ListaGlobalProductos.Remove(App.ListaGlobalProductos.Find(x => x.numero_300indy == pedido.numero_300indy));

                    //var vm = BindingContext as PedidoViewModel;
                    //vm?.RemoveCommand.Execute(pedido);

                    if (App.ListaGlobalProductos.Count == 0)
                    {
                        CarroVacio();
                    }
                    ListViewCarrito.ItemsSource = null;
                    ListViewCarrito.ItemsSource = ListaDatos_Final;
                    //ListViewPedidos.ItemsSource = vm.Pedidos;
                }
            }
            else
            {
                var respuesta = await DisplayAlert("Remove product", "Are you sure you want to remove the selected product from the order?", "YES", "NO");
                if (respuesta == true)
                {
                    var button = sender as Button;
                    var pedido = button?.BindingContext as CarroViewModel;
                    //Borro el producto

                    ListaDatos_Final.Remove(ListaDatos_Final.Find(x => x.numero_300indy == pedido.numero_300indy));
                    App.ListaGlobalProductos.Remove(App.ListaGlobalProductos.Find(x => x.numero_300indy == pedido.numero_300indy));

                    //var vm = BindingContext as PedidoViewModel;
                    //vm?.RemoveCommand.Execute(pedido);

                    if (App.ListaGlobalProductos.Count == 0)
                    {
                        CarroVacio();
                    }
                    ListViewCarrito.ItemsSource = null;
                    ListViewCarrito.ItemsSource = ListaDatos_Final;                    
                    //ListViewPedidos.ItemsSource = vm.Pedidos;
                }
            }           
        }

        //private void ListViewCarrito_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    DisplayAlert("Mensaje", "Item seleccionado", "OK");
        //    //var posicion = App.ListaGlobalProductos.IndexOf((e.SelectedItem as CarroViewModel));
        //}
    }
}