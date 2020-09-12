using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadMessage : ContentPage
    {
        private string host = "nextrun.mykeenetic.by";
        private int port = 801;

        public ReadMessage(string key)
        {
            try
            {
                InitializeComponent();

                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                SendData(stream, "messages");
                GetData(stream);
                SendData(stream, key);
                getMessages.Text = GetData(stream);

                stream.Close();
                client.Close();
            }
            catch (Exception ex) 
            {
                DisplayAlert("Error", ex.Message, "Ok(");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public void SendData(NetworkStream stream, string data)
        {
            byte[] temp = new byte[255];
            temp = Encoding.UTF8.GetBytes(data);
            stream.Write(temp, 0, temp.Length);
        }

        public string GetData(NetworkStream stream)
        {
            byte[] temp = new byte[255];
            stream.Read(temp, 0, temp.Length);
            return Encoding.UTF8.GetString(temp);
        }
    }
}