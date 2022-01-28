using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.IO;

using System.Collections.ObjectModel;


namespace NeoPueblo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient client = new TcpClient();

        String mensaje = "";
        String usuarioTrue = "";
        String contraTrue = "";

        public static String usuario;
        private int aux = 1;

        public static string NombreUser = "";

        public MainPage()
        {
            InitializeComponent();
            Conectar();
            
            NavigationPage.SetHasNavigationBar(this, false);
        }

 

        private async void btn_login(object sender, EventArgs e) 
        {
            if (aux > 0)
            {
                streamw.WriteLine(correo.Text);
                usuario = correo.Text;
                
                streamw.Flush();
                streamw.WriteLine(contraseña.Text);
                streamw.Flush();
                
                await Task.Delay(2000);
                bool isEmptyUsr = string.IsNullOrEmpty(correo.Text);
                bool isEmptyPass = string.IsNullOrEmpty(contraseña.Text);
                if (isEmptyUsr || isEmptyPass)
                {
                    
                    await DisplayAlert("Rellena todos los campos", "Existen Campos vacíos", "Aceptar");
                }
                else
                {

                    if (usuarioTrue.Equals("Existe") && contraTrue.Equals("Existe"))
                    {

                        _ = Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        
                        await DisplayAlert("Error", "ESTA CUENTA NO ESTA REGISTRADA /n" + "REVISA LOS DATOS QUE INGRESASTE", "Aceptar");
                    }
                }
            }
            else
            {
                usuario = correo.Text;
                aux++;
            }
        }
        

        void Listen()
        {
            while (client.Connected)
            {
                try
                {
                    mensaje = streamr.ReadLine();
                    if (mensaje.Equals("Existe"))
                    {
                        usuarioTrue = "Existe";

                    }
                    else if (mensaje.Equals("Correcta"))
                    {
                        contraTrue = "Existe";
                    }
                    else
                    {
                        usuarioTrue = "NExiste";
                        contraTrue = "NExiste";
                    }
                }
                catch
                {
                    DisplayAlert("Fail", "Error", "Aceptar");

                }
            }
        }
        void Conectar()
        {

            try
            {
                client.Connect("192.168.0.106", 2579);
                if (client.Connected)
                {
                    
                        Thread t = new Thread(Listen);
                        stream = client.GetStream();
                        streamw = new StreamWriter(stream);
                        streamr = new StreamReader(stream);

                        streamw.WriteLine(usuario);
                        streamw.Flush();

                        t.Start();
                }
                else
                {
                    DisplayAlert("No se ha podido conectar al servidor", "Error", "ora");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("No se ha podido conectar al servidor", "Error:" + ex, "Aceptar");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new Registro());
        }
    }
}
    

