using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewChatSednev
{
    public static class ChangeThemes
    {
        public static int PropertyThemes { get; set; }

        public static void LightThemes()
        {
            var Uri = new Uri(@"./Themes/lightthemes.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            PropertyThemes = 0;
        }

        public static void DarkThemes()
        {
            var Uri = new Uri(@"./Themes/darkthemes.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(Uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            PropertyThemes = 1;
        }
    }
}
