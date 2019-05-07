using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Pongolini_Catalogo.Negocio
{
    public class PedidoViewModel
    {
        public List<CarroViewModel> Pedidos = new List<CarroViewModel>();

        public Command<CarroViewModel> RemoveCommand
        {
            get
            {
                return new Command<CarroViewModel>((CarroViewModel pedido) =>
                {
                    Pedidos.Remove(Pedidos.Find(x => x.numero_300indy == pedido.numero_300indy));
                });
            }
        }

        public PedidoViewModel()
        {
            
            foreach (var item in App.ListaGlobalProductos)
            {                              
                if (item.producto == "GUÍA")
                {
                    Pedidos.Add(new CarroViewModel { producto = item.producto, numero_300indy = item.numero_300indy, largo = item.largo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, imagen = "Guias_Carrito.jpg", cantidad = item.cantidad });
                }
                else
                {
                    if (item.producto == "ASIENTO")
                    {
                        Pedidos.Add(new CarroViewModel { producto = item.producto, numero_300indy = item.numero_300indy, largo = item.largo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, imagen = "Asientos_Carrito.jpg", cantidad = item.cantidad });
                    }
                    else
                    {
                        Pedidos.Add(new CarroViewModel { producto = "BUJE", numero_300indy = item.numero_300indy, largo = item.largo, diametro_exterior = item.diametro_exterior, diametro_interior = item.diametro_interior, imagen = "img_bujes_recortada.jpg", cantidad = item.cantidad });
                    }
                }
            }
        }
    }
}
