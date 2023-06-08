using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LessProject
{
    public class Program
    {
        private TcpListener listener;
        private readonly Dictionary<TcpClient, string> connectedClients = new Dictionary<TcpClient, string>();
        private static readonly string connectionString = "Server=LAPTOP-U9HFGMNP;Database=MessengerDb;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        public void Start()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Thread clientThread = new Thread(() =>
                    {
                        HandleClient(client);
                    });
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при запуске сервера: " + ex.Message);
            }
            finally
            {
                listener.Stop();
            }
        }

        private void HandleClient(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.Unicode.GetString(buffer, 0, bytesRead);

                if (message.StartsWith("REGISTER|"))
                {
                    string[] parts = message.Split('|');
                    if (parts.Length == 3)
                    {
                        string username = parts[1];
                        string password = parts[2];

                        if (RegisterUser(username, password))
                        {
                            connectedClients.Add(tcpClient, username);
                            SendConfirmationMessage(stream, "REGISTRATION_SUCCESS");
                        }
                        else
                        {
                            SendErrorMessage(stream, "REGISTRATION_FAILED");
                        }
                    }
                    else
                    {
                        SendErrorMessage(stream, "Invalid message format.");
                    }
                }
                else if (message.StartsWith("LOGIN|"))
                {
                    string[] parts = message.Split('|');
                    if (parts.Length == 3)
                    {
                        string username = parts[1];
                        string password = parts[2];

                        if (LoginUser(username, password))
                        {
                            connectedClients.Add(tcpClient, username);
                            SendConfirmationMessage(stream, "LOGIN_SUCCESS");
                            Console.WriteLine("User logged in: " + username);
                        }
                        else
                        {
                            SendErrorMessage(stream, "LOGIN_FAILED");
                        }
                    }
                    else
                    {
                        SendErrorMessage(stream, "Invalid message format.");
                    }
                }
                else
                {
                    foreach (var client in connectedClients.Keys)
                    {
                        NetworkStream clientStream = client.GetStream();
                        clientStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private bool RegisterUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка регистрации пользователя: " + ex.Message);
                    return false;
                }
            }
        }

        private bool LoginUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int result = (int)command.ExecuteScalar();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка авторизации пользователя: " + ex.Message);
                    return false;
                }
            }
        }

        private void SendConfirmationMessage(NetworkStream stream, string message)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void SendErrorMessage(NetworkStream stream, string errorMessage)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(errorMessage);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}