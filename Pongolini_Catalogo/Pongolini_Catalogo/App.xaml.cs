using Pongolini_Catalogo.MasterDetail;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pongolini_Catalogo
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }

        public static List<Guias_Asientos> ListaGlobal = new List<Guias_Asientos>();

        public App()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient client = new RestClient();
                ListaGlobal = await client.Get<Guias_Asientos>("http://serviciowebpongolini.apphb.com/api/Guias_AsientosApi");//URL de la api.
            });

            MainPage = new NavigationPage(new SplashPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
