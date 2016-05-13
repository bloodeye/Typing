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
using System.Threading;


namespace Typing
{
    /// <summary>
    /// Interaction logic for Game_Screen.xaml
    /// </summary>
    public partial class Game_Screen : Window
    {
        //khai bao bien
        public const int time_limit = 30;
        public delegate void UpdateTextBackCall(string clock);
        public delegate void UpdatePostionBackCall(int W, int H);
        public delegate void RemoveObjBackCall(object obj);

        //ket thuc khai bao bien

        public Game_Screen()
        {
            InitializeComponent();
            Thread clock = new Thread(clock_event);
            clock.Start();
            //Thread word = new Thread(game_run);
            //word.Start();

            while (true)
            {
                new Thread(game_run).Start();
                Thread.Sleep(1000);
            }

        }

        public void game_run()
        {
           
            int iword = (new Random().Next(97, 122));
            char a = (char)iword;
            TextBlock obj = word_block;
            //word_block.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateWord), new object[] { a.ToString() });
            //iword = (new Random().Next(0, 500));
            //a = (char)iword;

            obj.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateWord), new object[] { a.ToString() });


            iword = (new Random().Next(0, 500));
            for (int i = 0; i < 35; i++)
            {
                //word_block.Dispatcher.Invoke(new UpdatePostionBackCall(this.UpdatePosition), new object[] {iword , i*20});
                obj.Dispatcher.Invoke(new UpdatePostionBackCall(this.UpdatePosition), new object[] { iword + 100, i * 20 });

                Thread.Sleep(100);

            }
            this.Dispatcher.Invoke(new RemoveObjBackCall(this.Removeobj), new object[] { obj });
        }

        public void clock_event()
        {
            for (int i = 0; i < time_limit; i++)
            {
                game_clock.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateClock), new object[] { i.ToString() });
                Thread.Sleep(1000);
            }
            MessageBox.Show("End Game!");
        }

        public void UpdateWord(string word)
        {
            word_block.Text = word;
        }
        public void UpdateClock(string clock)
        {
            game_clock.Text = clock;
        }

        public void UpdatePosition(int W, int H)
        {
            Thickness obj = word_block.Margin;
            obj.Left = W;
            obj.Top = H;
            word_block.Margin = obj;
        }

        public void Removeobj(object obj)
        {
            obj = null;
        }
        
    }
}
