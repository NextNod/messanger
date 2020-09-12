using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Net.Sockets;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Acc : ContentPage
    {
        private string key;
        private string host = "nextrun.mykeenetic.by";
        private int port = 801;
        
        public Acc(string keys, string ac)
        {
            InitializeComponent();
            key = keys;
            Label.Text = "Welcom back " + ac + "!";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendMessag(key), false);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReadMessage(key), true);
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                SendData(stream, "exit");
                GetData(stream);
                SendData(stream, key);
                GetData(stream);

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