using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NewChatSednev
{
    /// <summary>
    /// Логика взаимодействия для AthorizationPage.xaml
    /// </summary>
    public partial class AthorizationPage : Window
    {
        public AthorizationPage()
        {
            InitializeComponent();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");
            AppConnect.sedEntities = new NewChatSedEntities();
            ChangeThemes.PropertyThemes = 1;
            LogoPNG.Source = new BitmapImage(new Uri("./Icon/LightThemsLogo.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            IconThemes.Source = new BitmapImage(new Uri("./Icon/LightNew.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {

            //var HttpUser = new HttpClient();

            //var uuser = AppConnect.sedEntities.UserData.Where(p => p.Login == Login.Text && p.Password == Password.Text).FirstOrDefault();
            //{
            //    if (uuser == null)
            //    {
            //        MessageBox.Show("Пользователь не найден, повторите попытку!");
            //    }
            //    else
            //    {
            //        Helper.id_userr = uuser.Id;
            //        MainWindow mainWindow = new MainWindow();
            //        var response = await HttpUser.GetAsync("http://example.com/api/users?id=" + uuser.Id);
            //        mainWindow.Show();
            //        this.Close();
            //    }
            //}
        
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationsPage registrationsPage = new RegistrationsPage();
            registrationsPage.Show();
            this.Close();

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutProgramPage aboutProgramPage = new AboutProgramPage();
            aboutProgramPage.Show();
            this.Close();
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeThemes.PropertyThemes == 1)
            {
                ChangeThemes.LightThemes();
                LogoPNG.Source = new BitmapImage(new Uri("./Icon/DarkThemsLogo.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                IconThemes.Source = new BitmapImage(new Uri("./Icon/DarkNew.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            }
            else
            { 
                ChangeThemes.DarkThemes();
                LogoPNG.Source = new BitmapImage(new Uri("./Icon/LightThemsLogo.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                IconThemes.Source = new BitmapImage(new Uri("./Icon/LightNew.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            }
        }
    }
}
