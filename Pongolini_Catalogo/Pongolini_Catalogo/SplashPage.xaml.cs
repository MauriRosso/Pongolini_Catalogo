using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.MasterDetail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
        
        public SplashPage()
        {
            InitializeComponent();
            this.BackgroundColor = Color.DarkBlue;
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SplashImage.Opacity = 0;
            SplashName.Opacity = 0;

            SplashImage.FadeTo(1, 1000);
            SplashName.FadeTo(1, 1000);
            SplashImage.TranslateTo(0, -50, 1500);
            await SplashName.TranslateTo(0, 50, 1500);

            //await Navigation.PushAsync(new HomePage());
            Application.Current.MainPage = new HomePage();
        }
    }
}