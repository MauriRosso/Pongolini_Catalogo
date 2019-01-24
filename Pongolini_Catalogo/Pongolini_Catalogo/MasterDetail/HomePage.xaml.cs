using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pongolini_Catalogo.Negocio;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pongolini_Catalogo.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {

        public HomePage()
        {
            InitializeComponent();
            App.MasterD = this;
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;           
        }


        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as HomePageMenuItem;
        //    if (item == null)
        //        return;

        //    //var page = (Page)Activator.CreateInstance(item.TargetType);
        //    //page.Title = item.Title;

        //    switch (item.Id)
        //    {
        //        case 0:
        //            Detail = new NavigationPage(new Inicio());
        //            break;
        //        case 1:
        //            Detail = new NavigationPage(new Aplicaciones());
        //            break;
        //        case 2:
        //            Detail = new NavigationPage(new Intercambios());
        //            break;
        //        case 3:
        //            Detail = new NavigationPage(new Pongolini());
        //            break;
        //        case 4:
        //            Detail = new NavigationPage(new Dimensiones());
        //            break;
        //        case 5:
        //            Detail = new NavigationPage(new NuevosDesarrollos());
        //            break;
        //        default:
        //            break;
        //    }

        //    //Detail = new NavigationPage(page);
        //    IsPresented = false;

        //    MasterPage.ListView.SelectedItem = null;
        //}
    }
}