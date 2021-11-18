using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceChat;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMyContractCallback
    {
        private int Id;
        private bool isConnected = false;
        private MyContractClient client;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectionClic(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                UserDisconnect();
            }
            else
            {
                UserConnect();
            }
        }

        void UserConnect()
        {
            if (!isConnected)
            { 
                client = new MyContractClient(new System.ServiceModel.InstanceContext(this));

                bConnection.Content = "Disconnect";
                isConnected = true;

                tbUserName.IsEnabled = false;

                Id = client.Connect(tbUserName.Text);
            }
        }

        void UserDisconnect()
        {
            if (isConnected)
            {
                bConnection.Content = "Connect";
                isConnected = false;

                tbUserName.IsEnabled = true;

                client.Disconnect(Id);
                client = null;
            }
        }

        

        public void MsgCallback(string message)
        {
            lbChat.Items.Add(message);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserDisconnect();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(Id, tbMessage.Text);
                    tbMessage.Text = String.Empty;
                }
            }
        }
    }
}
