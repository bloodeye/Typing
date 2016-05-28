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
        int inword=-1;
        public const int time_limit = 20;
        public delegate void UpdateTextBackCall(TextBlock obj, string s);
        public delegate void UpdateBackCall();
        public delegate void UpdateWordBackCall(RichTextBox obj);
        List<RichTextBox> words = new List<RichTextBox>();
        List<string> lib = new List<string>();
        int[] pos = new int[15];
        string back = "";
        string front = "";
        string btw;
        RichTextBox flag_word;
        int error, cbt;
        //ket thuc khai bao bien

        public Game_Screen()
        {

            InitializeComponent();
            lib.Add("FUCK YOU");
            lib.Add("HELL NO");
            lib.Add("AMAZING HAHA");

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
            txt.Text = lib[new Random().Next(0,lib.Count)];
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

        private void event_KeyDown(object sender, KeyEventArgs e)
        {
            if (inword == -1)
            {
                btw = "";
                string text_word;
                foreach (RichTextBox word in words)
                {
                    text_word = new TextRange(word.Document.ContentStart, word.Document.ContentEnd).Text;
                    inword++;
                    if (e.Key.ToString()[0] == text_word[0])
                    {                        
                        flag_word = word;
                        back = text_word;
                        btw  += back[1];                        
                        front += back[0];
                        back = back.Remove(0, 2);
                        cbt++;
                        Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), flag_word);
                        break;
                    }
                }
            }
            else if (back == "")
            {
                if (e.Key.ToString()[0] == btw[0] || (e.Key == Key.Space && (int)btw[0] == 32))
                {
                    front += btw;
                    btw = btw.Remove(0, 1);
                    cbt++;
                    Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), flag_word);
                    front = "";
                    flag_word.IsEnabled.Equals(false);
                    pos[inword] = 0;
                    inword = -1;
                }
                else
                {
                    cbt++;
                    error++;
                }
            } else
            {
                if (e.Key.ToString()[0] == btw[0] || (e.Key == Key.Space && (int)btw[0] == 32))
                {
                    front += btw;
                    btw = btw.Remove(0, 1);
                    btw += back[0];
                    back = back.Remove(0, 1);
                    cbt++;
                    Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), flag_word);
                }
                else
                {
                    cbt++;
                    error++;
                }
            }
        }

        public void UpdateWord(RichTextBox obj)
        {
            obj.SelectAll();
            obj.Selection.Text = "";

            TextRange txt_front = new TextRange(obj.Document.ContentEnd, obj.Document.ContentEnd);
            txt_front.Text = front;
            txt_front.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);

            TextRange txt_btw = new TextRange(obj.Document.ContentEnd, obj.Document.ContentEnd);
            txt_btw.Text = btw.ToString();
            txt_btw.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Blue);

            TextRange txt_back = new TextRange(obj.Document.ContentEnd, obj.Document.ContentEnd);
            txt_back.Text = back;
            txt_back.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            txt_back.ApplyPropertyValue(TextElement.BackgroundProperty, null);
        }
    }
}
