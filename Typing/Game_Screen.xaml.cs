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
        bool flag = true;
        public const int time_limit = 20;
        public delegate void UpdateTextBackCall(TextBlock obj, string s);
        public delegate void UpdateBackCall();
        List<RichTextBox> words = new List<RichTextBox>();
        List<string> lib = new List<string>();
        int[] pos = new int[15];

        //ket thuc khai bao bien

        public Game_Screen()
        {
            char iword;

            InitializeComponent();
            for (int i = 97; i < 122; i++)
            {
                iword = (char)i;
                lib.Add(iword.ToString());
            }

            makePlaceWords();
            Thread clock = new Thread(clock_event);
            Thread game = new Thread(game_run);
            clock.Start();
            game.Start();
            
        }

        public void game_run()
        {
            int iword;
            iword = (new Random().Next(0, 550));
            while (flag)
            {
                Dispatcher.Invoke(new UpdateBackCall(this.UpdatePosition));
                Thread.Sleep(100);
            }
        }

        public void clock_event()
        {
            for (int i = 0; i < time_limit; i++)
            {
                game_clock.Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateClock), new object[] { game_clock,(i+1).ToString() });
                Dispatcher.Invoke(new UpdateBackCall(this.CheckandSet));
                Thread.Sleep(1000);
            }
            MessageBox.Show("End Game!");
            
        }
        public void UpdateClock(TextBlock obj,string clock)
        {
            obj.Text = clock;
        }
        public void makePlaceWords()
        {
            words.Add(word1);
            words.Add(word2);
            words.Add(word3);
            words.Add(word4);
            words.Add(word5);
            words.Add(word6);
            words.Add(word7);
            words.Add(word8);
            words.Add(word8);
            words.Add(word9);
            words.Add(word10);
            words.Add(word11);
            words.Add(word12);
            words.Add(word13);
            words.Add(word14);
            words.Add(word15);
            foreach (RichTextBox word in words)
            {
                word.IsEnabled = false;
                word.IsReadOnly = true;
                word.FontSize = 40;
            }
        }

        public void setWord(RichTextBox obj)
        {
            TextRange txt = new TextRange(obj.Document.ContentEnd, obj.Document.ContentEnd);
            txt.Text = lib[new Random().Next(0,lib.Count - 1)];
            obj.Margin = new Thickness(new Random().Next(0, 500), 0, 0, 0);

        }
        public void setPosition(RichTextBox obj, int top)
        {
            obj.Margin = new Thickness( obj.Margin.Left, top, 0, 0);
        }
        public void UpdatePosition()
        {
            for (int i = 0; i < 15; i++)
            {
                if (words[i].IsEnabled)
                {
                    pos[i] += 5;
                    setPosition(words[i], pos[i]);
                    if (pos[i] > 768)
                    {
                        flag = false;
                        MessageBox.Show("Game Over!");
                        break;
                    }
                }
            }
        }
        public void CheckandSet()
        {
            foreach (RichTextBox word in words)
            {
                if (!word.IsEnabled)
                {
                    word.IsEnabled = true;
                    setWord(word);
                    break;
                }
            }
        }
    }
}
