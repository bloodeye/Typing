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
using System.Windows.Shapes;

namespace Typing
{
    /// <summary>
    /// Interaction logic for UserChange.xaml
    /// </summary>
    public partial class UserChange : Window
    {
        public UserChange()
        {
            List<string> item = new List<string>();
            InitializeComponent();
            item.Add("User 1");
            item.Add("User 2");
            item.Add("User 3");
            List_User.ItemsSource = item;
        }

        private void metroTile_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
