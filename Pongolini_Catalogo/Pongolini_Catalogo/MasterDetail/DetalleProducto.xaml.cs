using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.Negocio;
using Plugin.Messaging;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Negocio;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleProducto : ContentPage
	{
        string mensaje = "";
        public DetalleProducto (Guias guia, Asientos asiento)
		{
			InitializeComponent ();
            if (guia != null) //Viene una guia
            {
                lbl300indy.Text = guia.numero_300indy;
                lblTipoProducto.Text = "GUIA";
                this.FindByName<Label>("lbladmesc").Text = guia.admision_escape;
                this.FindByName<Label>("lbldiamext").Text = guia.diametro_exterior.ToString();
                this.FindByName<Label>("lbldiamint").Text = guia.diametro_interior.ToString();
                this.FindByName<Label>("lbllargo").Text = guia.largo.ToString();
                this.FindByName<Label>("lblmaterial").Text = guia.material;
                this.FindByName<Label>("lblsobremedida").Text = guia.sobremedida.ToString();
                
            }
            else
            {
                
            }

            mensaje = "Producto: " + lblTipoProducto.Text + "\n" + "N° 300INDY: " + lbl300indy.Text + "\n";
        }

        private async void btnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            bool rta = await DisplayAlert("¿Confirmar pedido?", mensaje, "Confirmar", "Cancelar");
            if (rta == true) //Confirmo pedido
            {
                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {
                    var email = new EmailMessageBuilder()
                        .To("maurirosso@hotmail.com") //Colocar mail de pongolini.
                        .Subject("PEDIDO REALIZADO - Pongolini catálogo app")
                        .Body("Hola, deseo realizar el siguiente pedido: \n" + mensaje + "\n" + "Espero respuesta para confirmar la compra, saludos.")
                        .Build();

                    emailMessenger.SendEmail(email);
                }
            }
        }
    }
}