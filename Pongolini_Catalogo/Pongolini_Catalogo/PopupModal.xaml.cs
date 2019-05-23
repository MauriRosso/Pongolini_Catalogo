using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pongolini_Catalogo.MasterDetail;
using Plugin.Messaging;
using Pongolini_Catalogo.Negocio;
using Rg.Plugins.Popup.Services;

namespace Pongolini_Catalogo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupModal : PopupPage
    {
        bool es_envio = false;
        string Mensaje = "";

        string producto_carrito = "";
        string num300indy_carrito = "";
        string largo_carrito = "";
        string diamext_carrito = "";
        string diamint_carrito = "";

        public PopupModal(List<CarroViewModel> listaCarro)
        {
            es_envio = true;
            InitializeComponent();
            if (App.Idioma == "ES")
            {
                foreach (var item in listaCarro)
                {
                    Mensaje += "PRODUCTO: " + item.producto + "\n" +
                               "N° 300INDY: " + item.numero_300indy + "\n" +
                               "UNIDADES SOLICITADAS: " + item.cantidad + "\n" + "________________________________" + "\n";

                }
                lblTitulo.Text = "Solicitud de cotización";
                btnConfirmar.Text = "SOLICITAR";
            }
            else
            {
                foreach (var item in listaCarro)
                {
                    Mensaje += "PRODUCT: " + item.producto + "\n" +
                               "300INDY NUMBER: " + item.numero_300indy + "\n" +
                               "REQUESTED UNITS: " + item.cantidad + "\n" + "________________________________" + "\n";

                }
                lblTitulo.Text = "Request for quotation";
                btnConfirmar.Text = "REQUEST";
            }
            lblNombre.IsVisible = true;
            txtNombre.IsVisible = true;
            lblApellido.IsVisible = true;
            txtApellido.IsVisible = true;
            lblTelefono.IsVisible = true;
            txtTelefono.IsVisible = true;
            lblDireccion.IsVisible = true;
            txtDireccion.IsVisible = true;
            lblCodigoPostal.IsVisible = true;
            txtCodigoPostal.IsVisible = true;
            lblCantUni.IsVisible = false;
            txtCantUni.IsVisible = false;
        }

        public PopupModal(string prod, string num300indy, string largo, string diamext, string diamint)
        {
            es_envio = false;
            InitializeComponent();
            producto_carrito = prod;
            num300indy_carrito = num300indy;
            largo_carrito = largo;
            diamext_carrito = diamext;
            diamint_carrito = diamint;
            lblNombre.IsVisible = false;
            txtNombre.IsVisible = false;
            lblApellido.IsVisible = false;
            txtApellido.IsVisible = false;
            lblTelefono.IsVisible = false;
            txtTelefono.IsVisible = false;
            lblDireccion.IsVisible = false;
            txtDireccion.IsVisible = false;
            lblCodigoPostal.IsVisible = false;
            txtCodigoPostal.IsVisible = false;
            lblCantUni.IsVisible = true;
            txtCantUni.IsVisible = true;
            if (App.Idioma == "ES")
            {
                lblTitulo.Text = "Unidades solicitadas";
                btnConfirmar.Text = "AGREGAR AL CARRITO";
            }
            else
            {
                lblTitulo.Text = "Requested units";
                btnConfirmar.Text = "ADD TO CART";
            }
        }

        private async void EnviarElMail()
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtCodigoPostal.Text)) //Controlar que todos los campos hayan sidos RELLENADOS.
            {
                if (App.Idioma == "ES")
                {
                    DisplayAlert("Error", "Rellene todos los campos antes de continuar.", "OK");
                }
                else
                {
                    DisplayAlert("Error", "Fill in all the fields before continuing", "OK");
                }
            }
            else
            {
                if (App.Idioma == "ES")
                {
                    await DisplayAlert("Realizar pedido", "Serás redirigido a tu aplicación de correo para finalizar el pedido.", "OK");
                }
                else
                {
                    await DisplayAlert("Request quote", "You will be redirected to your email application to finalize the order", "OK");
                }
                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {
                    if (App.Idioma == "ES")
                    {
                        var email = new EmailMessageBuilder()
                        .To("comercial@pongolini.com") //Colocar mail de pongolini.
                        .Subject("PEDIDO REALIZADO - Pongolini catálogo app")
                        .Body("Hola, deseo realizar el siguiente pedido: \n" +
                        "------------------------------------" + "\n" +
                           "DATOS DEL SOLICITANTE" + "\n" +
                           "Nombre: " + txtNombre.Text + "\n" +
                           "Apellido: " + txtApellido.Text + "\n" +
                           "Teléfono: " + txtTelefono.Text + "\n" +
                           "Dirección: " + txtDireccion.Text + "\n" +
                           "Código postal: " + txtCodigoPostal.Text + "\n" +
                           "------------------------------------" + "\n" +
                              Mensaje + "\n" + "\n" +
                           "Espero respuesta para confirmar la compra, saludos.")
                        .Build();
                        emailMessenger.SendEmail(email);
                    }
                    else
                    {
                        var email = new EmailMessageBuilder()
                        .To("comercial@pongolini.com") //Colocar mail de pongolini.
                        .Subject("ORDER PLACED - Pongolini catálogo app")
                        .Body("Hi, I wish to make the next order: \n" +
                        "------------------------------------" + "\n" +
                           "APPLICANT DETAILS" + "\n" +
                           "Name: " + txtNombre.Text + "\n" +
                           "Surname: " + txtApellido.Text + "\n" +
                           "Phone: " + txtTelefono.Text + "\n" +
                           "Address: " + txtDireccion.Text + "\n" +
                           "Postal code: " + txtCodigoPostal.Text + "\n" +
                           "------------------------------------" + "\n" +
                              Mensaje + "\n" + "\n" +
                           "I wait for an answer to confirm the purchase.")
                        .Build();
                        emailMessenger.SendEmail(email);

                    }
                }
                else
                {
                    if (App.Idioma == "ES")
                    {
                        DisplayAlert("Error", "No puedes enviar e-mails en este momento. Por favor, intente más tarde.", "OK");
                    }
                    else
                    {
                        DisplayAlert("Error", "You can not send emails at this time. Please try later.", "OK");
                    }
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (es_envio)
            {
                EnviarElMail();
            }
            else //Agrega unidades de un producto al carrito
            {
                if (txtCantUni.Text != null && txtCantUni.Text != "")
                {
                    App.ListaGlobalProductos.Add(new CarroViewModel { producto = producto_carrito, numero_300indy = num300indy_carrito, largo = Convert.ToDouble(largo_carrito), diametro_exterior = Convert.ToDouble(diamext_carrito), diametro_interior = Convert.ToDouble(diamint_carrito), cantidad = Convert.ToInt32(txtCantUni.Text) });
                    await PopupNavigation.Instance.PopAsync(); //Cierro el popup
                    App.MasterD.Detail = new NavigationPage(new CarroDeCompras());
                }
                else
                {
                    if (App.Idioma == "ES")
                    {
                        DisplayAlert("Error", "Rellene el campo para continuar.", "OK");
                    }
                    else
                    {
                        DisplayAlert("Error", "Fill in the field to continue.", "OK");
                    }
                }
            }
        }
    }
}