using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
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

namespace LessChat
{
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];

        public MainWindow()
        {
            InitializeComponent();
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient();
                client.Connect("localhost", 8888);
                stream = client.GetStream();
                stream.BeginRead(buffer, 0, buffer.Length, ReceiveMessage, null);
                Console.WriteLine("Подключено к серверу.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка подключения к серверу: " + ex.Message);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;

            // Проверка формата сообщения
            if (!string.IsNullOrWhiteSpace(message) && message.Contains("|"))
            {
                // Отправка сообщения на сервер
                SendMessage(message);
                messageTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Неверный формат сообщения. Сообщение должно содержать символ '|'.");
            }
        }

        private void ReceiveMessage(IAsyncResult ar)
        {
            int bytesRead = stream.EndRead(ar);
            if (bytesRead > 0)
            {
                string message = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Dispatcher.Invoke(() =>
                {
                    chatListBox.Items.Add(message);
                    chatListBox.ScrollIntoView(chatListBox.Items[chatListBox.Items.Count - 1]);
                });
                stream.BeginRead(buffer, 0, buffer.Length, ReceiveMessage, null);
            }
        }

        private void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.Unicode.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            stream.Close();
            client.Close();
        }
    }
}

