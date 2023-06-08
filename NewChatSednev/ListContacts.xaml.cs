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
    /// Логика взаимодействия для ListContacts.xaml
    /// </summary>
    public partial class ListContacts : Page
    {
        public ListContacts()
        {
            InitializeComponent();
            AppConnect.sedEntities = new NewChatSedEntities();
            ListSpisok.ItemsSource = AppConnect.sedEntities.UserData.Where(p => p.Id != Helper.id_userr).ToList();
        }

        private void ListSpisok_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var select = ListSpisok.SelectedItem as UserData;

            ContatsUser contatsUser = new ContatsUser
            { 
                IdUser = Helper.id_userr,
                IdFriend = select.Id,
            };

            Helper.id_friend = (int)contatsUser.IdFriend;
            AppConnect.sedEntities.ContatsUser.Add(contatsUser);
            AppConnect.sedEntities.SaveChanges();
        }
    }
}
