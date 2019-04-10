﻿using System;
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

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Contacto : ContentPage
	{
		public Contacto ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarMapa();
        }

        public async void CargarMapa()
        {
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-31.2590372, -61.5120252), Distance.FromKilometers(5)));
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Se necesita localizacion", "El catálogo necesita la localización.", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    Mapa.IsShowingUser = true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Localizacion denegada", "No podrá continuar. Intente nuevamente.", "OK");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
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