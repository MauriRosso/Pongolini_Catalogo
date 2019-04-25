using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;

namespace Pongolini_Catalogo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupModal : PopupPage
	{
        string producto = null;
        string numero300indy = null;
		public PopupModal (string prod, string codigo)
		{
			InitializeComponent ();
            producto = prod;
            numero300indy = codigo; //puede ser un num 300indy o un codigo de buje
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (producto != "GUÍA" && producto != "ASIENTO")
            {
                if (emailMessenger.CanSendEmail)
                {
                    var email = new EmailMessageBuilder()
                        .To("maurirosso@hotmail.com") //Colocar mail de pongolini.
                        .Subject("PEDIDO REALIZADO - Pongolini catálogo app")
                        .Body("Hola, deseo realizar el siguiente pedido: \n" +
                               "PRODUCTO: " + producto + "\n" +
                               "CÓDIGO: " + numero300indy + "\n" +
                               "UNIDADES SOLICITADAS: " + txtCantUni.Text + "\n" +
                               "------------------------------" + "\n" +
                               "DATOS DEL SOLICITANTE" + "\n" +
                               "Nombre: " + txtNombre.Text + "\n" +
                               "Apellido: " + txtApellido.Text + "\n" +
                               "Teléfono: " + txtTelefono.Text + "\n" +
                               "Dirección: " + txtDireccion.Text + "\n" +
                               "Código postal: " + txtCodigoPostal.Text + "\n" +
                               "------------------------------" + "\n" +
                               "Espero respuesta para confirmar la compra, saludos.")
                        .Build();

                    emailMessenger.SendEmail(email);
                }
            }
            else
            {
                if (emailMessenger.CanSendEmail)
                {
                    var email = new EmailMessageBuilder()
                        .To("maurirosso@hotmail.com") //Colocar mail de pongolini.
                        .Subject("PEDIDO REALIZADO - Pongolini catálogo app")
                        .Body("Hola, deseo realizar el siguiente pedido: \n" +
                               "PRODUCTO: " + producto + "\n" +
                               "N° 300INDY: " + numero300indy + "\n" +
                               "UNIDADES SOLICITADAS: " + txtCantUni.Text + "\n" +
                               "------------------------------" + "\n" +
                               "DATOS DEL SOLICITANTE" + "\n" +
                               "Nombre: " + txtNombre.Text + "\n" +
                               "Apellido: " + txtApellido.Text + "\n" +
                               "Teléfono: " + txtTelefono.Text + "\n" +
                               "Dirección: " + txtDireccion.Text + "\n" +
                               "Código postal: " + txtCodigoPostal.Text + "\n" +
                               "------------------------------" + "\n" +
                               "Espero respuesta para confirmar la compra, saludos.")
                        .Build();

                    emailMessenger.SendEmail(email);
                }
            }          
        }
    }
}