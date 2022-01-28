using BaseLib.Graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace NeoPueblo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient client = new TcpClient();
        String mensaje;
        public Registro()
        {
            InitializeComponent();
            
            NavigationPage.SetHasNavigationBar(this, false);
        }
        /*void Listen()
       {
           while (client.Connected)
           {
               try
               {
                   mensaje = streamr.ReadLine();
               }
               catch
               {
                   DisplayAlert("Fail", "Error", "Aceptar");

               }
           }
       }*/
        private async void btn_ingresar_resgistro_Clicked(object sender, EventArgs e)
        {
            
            if (await ValidarFormulario())
            {
                streamw.WriteLine(Nombres.Text);
                streamw.Flush();
                streamw.WriteLine(Apellidos.Text);
                streamw.Flush();
                streamw.WriteLine(Curp.Text);
                streamw.Flush();
                streamw.WriteLine(Correo.Text);
                streamw.Flush();
                streamw.WriteLine(sexo.Text);
                streamw.Flush();
                streamw.WriteLine(localidad.Text);
                streamw.Flush();
                streamw.WriteLine(Contraseña_Confirmada.Text);
                streamw.Flush();
                client.Close();

                await DisplayAlert("Exito", "Todos tus campos cumplieron las validaciones.", "OK");
                await Navigation.PushAsync(new HomePage());
            }
        }
        void Conectar()
        {
            try
            {
                client.Connect("192.168.0.109", 2582);
                if (client.Connected)
                {

                    stream = client.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);
                    DisplayAlert("BIENVENIDO AL AREA DE REGISTRO", "FAVOR DE LLENAR TODOS LOS CAMPOS SOLICITADOS", "ESTA BIEN");

                }
                else
                {
                    DisplayAlert("No se ha podido conectar al servidor", "Error", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", "NO SE HA PODIDO CONECTAR AL SERVIDOR " + ex, "ORA");
            }
        }

        private async Task<bool> ValidarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(Nombres.Text) || String.IsNullOrWhiteSpace(Apellidos.Text) || String.IsNullOrWhiteSpace( Curp.Text) || String.IsNullOrWhiteSpace(Correo.Text) || String.IsNullOrWhiteSpace(sexo.Text) || String.IsNullOrWhiteSpace(localidad.Text) || String.IsNullOrWhiteSpace(Contraseña.Text))
            {
                await this.DisplayAlert("***ADVERTENCIA***", "TIENES CAMPOS VACIOS. FAVOR DE LLENARLOS TODOS", "ESTA BIEN");
                return false;
            }
            if (Contraseña.Text != Contraseña_Confirmada.Text)
            {
                await this.DisplayAlert("***MUCHO OJO***", "LAS CONTRASEÑA NO COICIDEN. FAVOR DE REVISAR", "ESTA BIEN");
                return false;
            }
            if (Curp.Text.Length != 18)
            {
                await this.DisplayAlert("***ADVERTENCIA***", "FALTAN DIGITOS, FAVOR DE INGRESAR SUS 18 DIGITOS DE CURP.", "ESTA BIEN");
                return false;
            }
            if (Contraseña.Text.Length < 8)
            {
                await this.DisplayAlert("***ADVERTENCIA***", "UNA CONTRASEÑA SEGURA CONTIENE MÁS DE 8 DE DIGITOS.", "ESTA BIEN");
                return false;
            }
            return true;
        }
    }
}