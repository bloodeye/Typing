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
        public const int time_limit = 10;
        public delegate void UpdateTextBackCall( TextBlock obj,string text);
        public delegate void UpdatePostionBackCall(TextBlock obj,int W, int H);
        public delegate void RemoveObjBackCall(object obj);
        public delegate void GetTextBlockBackCall(object obj);
        //ket thuc khai bao bien

        public Game_Screen()
        {
            InitializeComponent();
            Thread clock = new Thread(clock_event);
            clock.Start();
            Thread word = new Thread(game_run);
            word.SetApartmentState(ApartmentState.STA);
            word.Start();

            //TextBlock a = (TextBlock) this.Resources["word"];
            //a.Text = "abc";
        }

        public void game_run()
        {
           
            int iword = (new Random().Next(97, 122));
            char a = (char)iword;
            TextBlock obj = new TextBlock();
            //word_block.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateWord), new object[] { a.ToString() });
            //iword = (new Random().Next(0, 500));
            //a = (char)iword;

            //viet chu cho word_block
            obj.Dispatcher.Invoke(new GetTextBlockBackCall(this.GetTextBlock), new object[] { obj });

            obj.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateWord), new object[] { obj,a.ToString()});


            iword = (new Random().Next(0, 550));
            for (int i = 0; i < 35; i++)
            {
                //word_block.Dispatcher.Invoke(new UpdatePostionBackCall(this.UpdatePosition), new object[] {iword , i*20});
                obj.Dispatcher.Invoke(new UpdatePostionBackCall(this.UpdatePosition), new object[] { obj,iword, i * 20 });

                Thread.Sleep(100);

            }

        }

        public void clock_event()
        {
            for (int i = 0; i < time_limit; i++)
            {
                game_clock.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateClock), new object[] { game_clock,(i+1).ToString() });
                Thread.Sleep(1000);
            }
            MessageBox.Show("End Game!");
        }

        public void UpdateWord(TextBlock obj,string word)
        {
            obj.Text = word;
        }
        public void UpdateClock(TextBlock obj,string clock)
        {
            obj.Text = clock;
        }

        public void UpdatePosition(TextBlock obj,int W, int H)
        {
            Thickness temp = obj.Margin;
            temp.Left = W;
            temp.Top = H;
            obj.Margin = temp;
        }

        public void GetTextBlock(object obj)
        {
            obj = (TextBlock)this.Resources["word"];
        }
        
    }
}
