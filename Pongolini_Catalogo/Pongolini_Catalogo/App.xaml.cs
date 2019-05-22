using Pongolini_Catalogo.MasterDetail;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using System.Collections.Generic;
using Negocio;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pongolini_Catalogo
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterD { get; set; }
        public static List<Guias> ListaGlobalGuias = new List<Guias>();
        public static List<Asientos> ListaGlobalAsientos = new List<Asientos>();
        public static List<Bujes> ListaGlobalBujes = new List<Bujes>();
        public static List<CarroViewModel> ListaGlobalProductos = new List<CarroViewModel>();
        public static string Idioma { get; set; }

        public App()
        {
            InitializeComponent();           
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
