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
    /// Interaction logic for Select_Excercise.xaml
    /// </summary>
    public partial class Select_Excercise : Window
    {
        public Select_Excercise()
        {
            InitializeComponent();
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            Typing_Screen tp = new Typing_Screen();
            tp.Show();
            this.Close();
            
        }
    }
}
