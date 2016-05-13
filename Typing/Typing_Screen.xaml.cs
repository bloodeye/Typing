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
    /// Interaction logic for Typing_Screen.xaml
    /// </summary>
    public partial class Typing_Screen : Window
    {
        string s = "bla bla bla bla!";
        public Typing_Screen()
        {
            InitializeComponent();
            textBox.Text = s;
            
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }     
    }

}
