using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Contacto : ContentPage
	{
		public Contacto ()
		{
			InitializeComponent ();
		}

        private void btnLlamar_Clicked(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
            {
                phoneDialer.MakePhoneCall("3492212689");
            }
        }

        private void btnEnviarMail_Clicked(object sender, EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                    .To("maurirosso@hotmail.com") //Colocar mail de pongolini.
                    .Subject("Consulta")
                    .Body("Este mensaje fue enviado desde la aplicación móvil de Pongolini.")
                    .Build();

                emailMessenger.SendEmail(email);
            }
        }

        //Falta el MAPA 
    }
}