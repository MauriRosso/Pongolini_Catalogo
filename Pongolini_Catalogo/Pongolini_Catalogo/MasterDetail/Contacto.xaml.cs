using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Pongolini_Catalogo.Negocio;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Contacto : ContentPage
	{
		public Contacto ()
		{
			InitializeComponent ();
            CargarTextoLabels();
            CargarCarrito();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //CargarMapa();
        }

        private void CargarTextoLabels()
        {
            if (App.Idioma != "ES")
            {
                Title = "CONTACT";
                lblLeyendaEmpresa.Text = "Since 1955 manufacturing guides and valve seats 300 indy, under the highest standars of quality, for the modern and demanding engines of the international market";
                btnLlamar.Text = "Call the company";
                btnEnviarMail.Text = "Send e-mail";
                lblEmpresa.Text = "COMPANY";
                lblDireccion.Text = "Ramón Casabella 985 - Postcode:2300";
                lblTelefono.Text = "PHONE: ";
                lblHorarios.Text = "SCHEDULES";
                lblHorariosDetalle.Text = "Monday to Friday from 8 a.m. to 6:30 p.m.";
                lblDiasAbiertos.Text = "Saturday and Sunday: closed";
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

        //public async void CargarMapa()
        //{
        //    Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-31.2590372, -61.5120252), Distance.FromKilometers(5)));
        //    try
        //    {
        //        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
        //        if (status != PermissionStatus.Granted)
        //        {
        //            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
        //            {
        //                await DisplayAlert("Se necesita localizacion", "El catálogo necesita la localización.", "OK");
        //            }

        //            var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
        //            status = results[Permission.Location];
        //        }

        //        if (status == PermissionStatus.Granted)
        //        {
        //            Mapa.IsShowingUser = true;
        //        }
        //        else if (status != PermissionStatus.Unknown)
        //        {
        //            await DisplayAlert("Localizacion denegada", "No podrá continuar. Intente nuevamente.", "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //FACEBOOK
        //Device.OpenUri(new Uri("https://www.facebook.com/300-INDY-354674128063671/"));
        //LINKED IN
        //Device.OpenUri(new Uri("https://ar.linkedin.com/company/aberaldo-pongolini-srl"));


        private void btnLlamar_Clicked(object sender, EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            //Seleccionar telefono con cuadro MODAL
            if (phoneDialer.CanMakePhoneCall)
            {
                phoneDialer.MakePhoneCall("+54 3492 504045");
            }
        }

        private void btnEnviarMail_Clicked(object sender, EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                var email = new EmailMessageBuilder()
                    .To("comercial@pongolini.com") //Colocar mail de pongolini.
                    .Subject("Consulta")
                    .Body("Este mensaje fue enviado desde la aplicación móvil de Pongolini Catálogo.")
                    .Build();

                emailMessenger.SendEmail(email);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Facebook
            Device.OpenUri(new Uri("https://www.facebook.com/300-INDY-354674128063671/"));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //LinkedIn
            Device.OpenUri(new Uri("https://ar.linkedin.com/company/aberaldo-pongolini-srl"));
        }

        private void imgCarro_Activated(object sender, EventArgs e)
        {
            App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
        }

        //Falta el MAPA 
    }
}