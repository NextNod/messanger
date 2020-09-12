using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendMessag : ContentPage
    {
        private string host = "nextrun.mykeenetic.by";
        private int port = 801;
        private string key;
        
        public SendMessag(string k)
        {
            InitializeComponent();
            key = k;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                SendData(stream, "send");
                GetData(stream);
                SendData(stream, key);
                GetData(stream);
                SendData(stream, to.Text);
                GetData(stream);
                SendData(stream, Message.Text);
                GetData(stream);

                stream.Close();
                client.Close();

                await DisplayAlert("Alert", "Message sended!", "Good)");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok(");
            }
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