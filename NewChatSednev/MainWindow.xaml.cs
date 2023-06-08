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

namespace NewChatSednev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            AppConnect.sedEntities = new NewChatSedEntities();
            ListProfile.ItemsSource = AppConnect.sedEntities.UserData.Where(p => p.Id == Helper.id_userr).ToList();
            ListContact.ItemsSource = AppConnect.sedEntities.ContatsUser.Where(c => c.IdUser == Helper.id_userr)
                .Join(AppConnect.sedEntities.UserData,c => c.IdFriend,ud => ud.Id,(c, ud) => ud).ToList();
            AppFrame.frames = FrameMain;
            ChangeThemes.PropertyThemes = 1;
            LogoPNG.Source = new BitmapImage(new Uri("./Icon/LightThemsLogo.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            IconThemes.Source = new BitmapImage(new Uri("./Icon/LightNew.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            ExitThemes.Source = new BitmapImage(new Uri("./Icon/WhiteDoor.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            EditProfileThemes.Source = new BitmapImage(new Uri("./Icon/ProfileWhite.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            AboutAppThemes.Source = new BitmapImage(new Uri("./Icon/AboutWhite.png",UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState= WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (ChangeThemes.PropertyThemes == 1)
            {
                ChangeThemes.LightThemes();
                LogoPNG.Source = new BitmapImage(new Uri("./Icon/DarkThemsLogo.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                IconThemes.Source = new BitmapImage(new Uri("./Icon/DarkNew.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                ExitThemes.Source = new BitmapImage(new Uri("./Icon/BlackDoor.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                EditProfileThemes.Source = new BitmapImage(new Uri("./Icon/ProfileBlack.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                AboutAppThemes.Source = new BitmapImage(new Uri("./Icon/AboutBlack.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            }
            else
            {
                ChangeThemes.DarkThemes();
                LogoPNG.Source = new BitmapImage(new Uri("./Icon/LightThemsLogo.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                IconThemes.Source = new BitmapImage(new Uri("./Icon/LightNew.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                ExitThemes.Source = new BitmapImage(new Uri("./Icon/WhiteDoor.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                EditProfileThemes.Source = new BitmapImage(new Uri("./Icon/ProfileWhite.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
                AboutAppThemes.Source = new BitmapImage(new Uri("./Icon/AboutWhite.png", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            }
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frames.Navigate(new ListContacts());
        }

        private void ListContact_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AppFrame.frames.Navigate(new ChatPage());
            if (ListContact.SelectedItem != null)
            { 
                UserData SelectedUser = (UserData)ListContact.SelectedItem;
                Helper.UserDataTransfer.SelectedUser = SelectedUser;
            }
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListContact.ItemsSource = AppConnect.sedEntities.ContatsUser.Where(c => c.IdUser == Helper.id_userr)
                .Join(AppConnect.sedEntities.UserData, c => c.IdFriend, ud => ud.Id, (c, ud) => ud)
                .Where(ud => ud.UserName.Contains(SearchTxtBox.Text)).ToList();
        }
    }
}
