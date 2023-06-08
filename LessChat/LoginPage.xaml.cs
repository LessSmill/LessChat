using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using LessProject;

namespace LessChat
{
    public partial class LoginPage : Window
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            try
            {
                if (AuthenticateUser(username, password))
                {
                    
                    using (TcpClient client = new TcpClient("localhost", 8888))
                    using (NetworkStream stream = client.GetStream())
                    {
                        string message = "LOGIN|" + username + "|" + password;
                        byte[] buffer = Encoding.Unicode.GetBytes(message);
                        stream.Write(buffer, 0, buffer.Length);

                        buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string response = Encoding.Unicode.GetString(buffer, 0, bytesRead);

                        if (response == "LOGIN_SUCCESS")
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid username or password" + ex.Message);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int result = (int)command.ExecuteScalar();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error authenticating user: " + ex.Message);
                    return false;
                }
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.Show();
            Close();
        }
    }
}