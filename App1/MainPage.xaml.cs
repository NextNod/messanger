using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string key;
        private string host = "nextrun.mykeenetic.by";
        private int port = 801;


        public MainPage()
        {
            InitializeComponent();

            NavigationPage navigation = new NavigationPage();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string login = Login.Text;
            string pass = Password.Text;
            try
            {
                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                SendData(stream, "log");
                GetData(stream);
                SendData(stream, login);
                GetData(stream);
                SendData(stream, pass);
                key = GetData(stream);
                Label.Text = "Key: " + key;

                await Navigation.PushAsync(new Acc(key, login));

                stream.Close();
                client.Close();
            }
            catch(Exception ex)
            {
                Label.Text = ex.Message;
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

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            string login = Login.Text;
            string pass = Password.Text;
            string email = Email.Text;

            try
            {
                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                SendData(stream, "reg");
                GetData(stream);
                SendData(stream, login);
                GetData(stream);
                SendData(stream, pass);
                GetData(stream);
                SendData(stream, email);
                GetData(stream);

                stream.Close();
                client.Close();

                client = new TcpClient(host, port);
                stream = client.GetStream();

                SendData(stream, "log");
                GetData(stream);
                SendData(stream, login);
                GetData(stream);
                SendData(stream, pass);
                key = GetData(stream);
                Label.Text = "Key: " + key;

                await Navigation.PushAsync(new Acc(key, login));

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Label.Text = ex.Message;
            }
        }
    }
}
