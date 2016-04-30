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

namespace Typing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string user = "xzfdaagagaa";
            InitializeComponent();
            Username.Text = user;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PassRequire pass = new PassRequire();
            pass.Show();
        }

        private void metroTile3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Change_User_Click(object sender, RoutedEventArgs e)
        {
            UserChange user_change = new UserChange();
            user_change.Show();
        }

        private void Intro_Click(object sender, RoutedEventArgs e)
        {
            Select_Intro Intro = new Select_Intro();
            Intro.Show();
        }

        private void Exercise_click(object sender, RoutedEventArgs e)
        {
            Select_Excercise Exercise = new Select_Excercise();
            Exercise.Show();
        }

        private void metroTile5_Click(object sender, RoutedEventArgs e)
        {
            Select_Game game = new Select_Game();
            game.Show();
        }
    }
}
