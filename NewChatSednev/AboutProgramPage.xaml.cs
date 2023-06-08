﻿using System;
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
using System.Windows.Shapes;

namespace NewChatSednev
{
    /// <summary>
    /// Логика взаимодействия для AboutProgramPage.xaml
    /// </summary>
    public partial class AboutProgramPage : Window
    {
        public AboutProgramPage()
        {
            InitializeComponent();
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AthorizationPage athorizationPage = new AthorizationPage();
            athorizationPage.Show();
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
