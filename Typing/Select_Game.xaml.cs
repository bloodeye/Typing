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
    /// Interaction logic for Select_Game.xaml
    /// </summary>
    public partial class Select_Game : Window
    {
        public Select_Game()
        {
            InitializeComponent();
        }

        private void metroTile_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
