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
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

namespace Pongolini_Catalogo.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleProducto : ContentPage
	{
        List<AplicacionesViewModel> ListaDatos_Final = new List<AplicacionesViewModel>(); //Informacion que se muestra en la listview.

        public DetalleProducto (Guias guia, Asientos asiento)
		{
			InitializeComponent ();
            ListaDatos_Final.Clear();
            ///ATRIBUTOS
            if (guia != null) //Viene una guia
            {
                lbl300indy.Text = guia.numero_300indy;
                lblTipoProducto.Text = "GUIA";
                lbladmesc.Text = guia.admision_escape;
                lbldiamext.Text = guia.diametro_exterior.ToString();
                lbldiamint.Text = guia.diametro_interior.ToString();
                lbllargo.Text = guia.largo.ToString();
                lblmaterial_angulo.Text = "Material";
                lblmat_ang.Text = guia.material;
                if (guia.forma != null)
                {
                    switch (guia.forma[0])
                    {
                        case 'A':
                            imgforma.Source = "A.png";
                            break;
                        case 'B':
                            imgforma.Source = "B.png";
                            break;
                        case 'C':
                            imgforma.Source = "C.png";
                            break;
                        case 'E':
                            imgforma.Source = "E.png";
                            break;
                        case 'F':
                            imgforma.Source = "F.png";
                            break;
                        case 'G':
                            imgforma.Source = "G.png";
                            break;
                        case 'L':
                            imgforma.Source = "L.png";
                            break;
                        case 'M':
                            imgforma.Source = "M.png";
                            break;
                        case 'N':
                            imgforma.Source = "N.png";
                            break;
                        case 'P':
                            imgforma.Source = "P.png";
                            break;
                        default:
                            break;
                    }
                }
                //INTERCAMBIOS
                lblinter_Mahle.Text = App.ListaGlobalGuias.Find(x => x.codigo == guia.codigo).codigo_mahle;
                lblinter_Riosulense.Text = App.ListaGlobalGuias.Find(x => x.codigo == guia.codigo).codigo_riosulense;
                ///APLICACIONES
                foreach (var item in App.ListaGlobalGuias)
                {
                    if (item.codigo == guia.codigo)
                    {
                        ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Guía", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor });
                    }
                }
                ListViewAplicaciones.ItemsSource = null;
                ListViewAplicaciones.ItemsSource = ListaDatos_Final;
            }
            else
            {
                lbl300indy.Text = asiento.numero_300indy;
                lblTipoProducto.Text = "ASIENTO";
                lbladmesc.Text = asiento.admision_escape;
                lbldiamext.Text = asiento.diametro_exterior.ToString();
                lbldiamint.Text = asiento.diametro_interior.ToString();
                lbllargo.Text = asiento.largo.ToString();
                lblmaterial_angulo.Text = "Angulo";
                lblmat_ang.Text = asiento.angulo;
                lblforma.IsVisible = false;
                bxUltimo.IsVisible = false;
                //INTERCAMBIOS
                lblinter_Mahle.Text = App.ListaGlobalAsientos.Find(x => x.codigo == asiento.codigo).codigo_mahle;
                lblinter_Riosulense.Text = App.ListaGlobalAsientos.Find(x => x.codigo == asiento.codigo).codigo_riosulense;
                ///APLICACIONES
                foreach (var item in App.ListaGlobalAsientos)
                {
                    if (item.codigo == asiento.codigo)
                    {
                        ListaDatos_Final.Add(new AplicacionesViewModel { producto = "Asiento", numero_300indy = item.numero_300indy, admision_escape = item.admision_escape, marca_modelo = item.marca_modelo, motor = item.motor });
                    }
                }
                ListViewAplicaciones.ItemsSource = null;
                ListViewAplicaciones.ItemsSource = ListaDatos_Final;
            }
        }

        private async void btnRealizarPedido_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupModal(lblTipoProducto.Text, lbl300indy.Text));
            //bool rta = await DisplayAlert("¿Confirmar pedido?", mensaje, "Confirmar", "Cancelar");
            //if (rta == true) //Confirmo pedido
            //{
            //    var emailMessenger = CrossMessaging.Current.EmailMessenger;
            //    if (emailMessenger.CanSendEmail)
            //    {
            //        var email = new EmailMessageBuilder()
            //            .To("maurirosso@hotmail.com") //Colocar mail de pongolini.
            //            .Subject("PEDIDO REALIZADO - Pongolini catálogo app")
            //            .Body("Hola, deseo realizar el siguiente pedido: \n" + mensaje + "\n" + "Espero respuesta para confirmar la compra, saludos.")
            //            .Build();

            //        emailMessenger.SendEmail(email);
            //    }
            //}
        }
    }
}