using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.MasterDetail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.Negocio;
using Negocio;

namespace Pongolini_Catalogo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
        bool ES_Activado = true;
        public SplashPage()
        {
            InitializeComponent();
            this.BackgroundColor = Color.FromHex("#1A237E");
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            imgEn.IsVisible = false;
            imgEs.IsVisible = false;            
            switchEN.IsVisible = false;
            switchES.IsVisible = false;
            btnAcceder.IsVisible = false;
            lblSeleccioneIdioma.IsVisible = false;
            lblEspanol.IsVisible = false;
            lblEnglish.IsVisible = false;

            SplashImage.Opacity = 0;
            SplashName.Opacity = 0;

            SplashImage.FadeTo(1, 1000);
            SplashName.FadeTo(1, 1000);
            SplashImage.TranslateTo(0, -25, 2000);
            await SplashName.TranslateTo(0, 15, 2000);
            SplashImage.TranslateTo(0, -75, 1200);
            SplashName.TranslateTo(0, -60, 1200);

            lblSeleccioneIdioma.IsVisible = true;
            imgEn.IsVisible = true;
            imgEs.IsVisible = true;
            switchEN.IsVisible = true;
            switchES.IsVisible = true;
            btnAcceder.IsVisible = true;
            lblEspanol.IsVisible = true;
            lblEnglish.IsVisible = true;
            slIdiomas.FadeTo(1, 2000);

            //slIdiomas.TranslateTo(0, 100, 1000);

            //await lblSeleccioneIdioma.TranslateTo(0, 50, 0);
            //imgEn.TranslateTo(0, 100, 0);
            //imgEs.TranslateTo(0, 100, 0);
            //switchEN.TranslateTo(0, 100, 0);
            //switchES.TranslateTo(0, 100, 0);
            //lblEspanol.TranslateTo(0, 100, 0);
            //lblEnglish.TranslateTo(0, 100, 0);
            //btnAcceder.TranslateTo(0, 100, 0);



        }

        private void btnAcceder_Clicked(object sender, EventArgs e)
        {
            if (ES_Activado)
            {
                App.Idioma = "ES";
            }
            else
            {
                App.Idioma = "EN";
            }
            Application.Current.MainPage = new HomePage();
        }

        private void Switch_Idioma(object sender, EventArgs e)
        {
            if (ES_Activado)
            {
                switchES.Source = "ic_cross.png";
                switchEN.Source = "ic_tick.png";
                ES_Activado = false;
            }
            else
            {
                switchES.Source = "ic_tick.png";
                switchEN.Source = "ic_cross.png";
                ES_Activado = true;
            }
        }
    }
}