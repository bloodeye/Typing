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
        public delegate void UpdateWordBackCall(RichTextBox obj);
        List<RichTextBox> words = new List<RichTextBox>();
        List<string> lib = new List<string>();
        int[] pos = new int[15];
        string back = "";
        string front = "";
        char btw = '\0';
        RichTextBox words_box;
        int error, cbt;
        char p_word;
        bool shift_down = false;
        int num_process = -1, scr = 0;
        //ket thuc khai bao bien

        public Game_Screen()
        {

            InitializeComponent();
            lib.Add("Thang");
            lib.Add("I hs");
            lib.Add("aba");

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

        public void resetPosition(RichTextBox obj)
        {
            obj.Margin = new Thickness(0, 0, 0, 0);
        }
        public void UpdatePosition()
        {
            foreach (RichTextBox obj in words)
            {
                if (obj.IsEnabled)
                {
                    setPosition(obj, (int)obj.Margin.Top + 5);
                    if ((int)obj.Margin.Top > 750)
                    {
                        MessageBox.Show("Game Over!");
                        this.Close();
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
            switch (e.Key)
            {
                case Key.None:
                    break;
                case Key.Space:
                    p_word = ' ';
                    GetKeyWords();
                    break;
                case Key.RightShift:
                    shift_down = true;
                    break;
                case Key.LeftShift:
                    shift_down = true;
                    break;
                case Key.OemPeriod:
                    p_word = '.';
                    GetKey_nums();
                    break;
                case Key.OemComma:
                    p_word = ',';
                    GetKey_nums();
                    break;
                case Key.OemQuestion:
                    p_word = '/';
                    GetKey_nums();
                    break;
                case Key.OemQuotes:
                    if (!shift_down)
                    {
                        p_word = "'".ToString()[0];
                    }
                    else p_word = '"';
                    GetKey_nums();
                    break;
                default:
                    if (e.Key > Key.D0 && e.Key < Key.D9)
                    {
                        p_word = e.Key.ToString()[1];
                        GetKey_nums();
                    }
                    else
                    if (e.Key > Key.NumPad0 && e.Key < Key.NumPad9)
                    {
                        p_word = e.Key.ToString()[6];
                        GetKey_nums();
                    }
                    else
                    {
                        p_word = e.Key.ToString()[0];
                        GetKeyWords();
                    }
                    break;
            }
        }

        public void GetKeyWords()
        {
            if (num_process == -1)
            {
                string temp;
                for (int i = 0; i< words.Count; i++)
                {
                    temp = new TextRange(words[i].Document.ContentStart, words[i].Document.ContentEnd).Text;
                    if (shift_down)
                    {
                        if(temp != "")
                        if (p_word == temp[0])
                        {
                            num_process = i;
                            back = temp;
                            front += p_word;
                            btw = back[1];
                            back = back.Remove(0, 2);
                            cbt++;
                            words[i].Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words[i] });
                            break;
                        }
                    }
                    else
                    {
                        p_word = p_word.ToString().ToLower()[0];
                        if(temp != "")
                        if (p_word == temp[0])
                        {
                            num_process = i;
                            back = temp;
                            front += p_word;
                            btw = back[1];
                            back = back.Remove(0, 2);
                            cbt++;
                            words[i].Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words[i] });
                            break;
                        }
                       
                    }

                }
                cbt++;
                error++;
            } else
            if (shift_down)
            {
                if (p_word == btw)
                {
                    front += btw;
                    btw = back[0];
                    back = back.Remove(0, 1);
                    cbt++;
                    words[num_process].Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words[num_process] });
                }
                else
                {
                    cbt++;
                    error++;
                }
            }
            else
            {
                p_word = p_word.ToString().ToLower()[0];
                if (p_word == btw)
                {
                    front += btw;
                    btw = back[0];
                    back = back.Remove(0, 1);
                    cbt++;
                    words[num_process].Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words[num_process] });
                }
                else
                {
                    cbt++;
                    error++;
                }
            }

            if (back.Length == 1)
            {
                drop_word(words[num_process]);
            }
        }

        public void GetKey_nums()
        {
            if (shift_down)
            {
                switch (p_word)
                {
                    case '0':
                        p_word = ')';
                        break;
                    case '1':
                        p_word = '!';
                        break;
                    case '2':
                        p_word = '@';
                        break;
                    case '3':
                        p_word = '#';
                        break;
                    case '4':
                        p_word = '$';
                        break;
                    case '5':
                        p_word = '%';
                        break;
                    case '6':
                        p_word = '^';
                        break;
                    case '7':
                        p_word = '&';
                        break;
                    case '8':
                        p_word = '*';
                        break;
                    case '9':
                        p_word = '(';
                        break;
                    case ',':
                        p_word = '<';
                        break;
                    case '.':
                        p_word = '>';
                        break;
                    case ';':
                        p_word = ':';
                        break;
                    case '/':
                        p_word = '?';
                        break;
                    default:
                        break;
                }
                if (p_word == btw)
                {
                    front += btw;
                    btw = back[0];
                    back = back.Remove(0, 1);
                    cbt++;
                    words[num_process].Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words[num_process] });
                }
                else
                {
                    cbt++;
                    error++;
                }
            }
            else
            {
                if (p_word == btw)
                {
                    front += btw;
                    btw = back[0];
                    back = back.Remove(0, 1);
                    cbt++;
                    words[num_process].Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words[num_process] });
                }
                else
                {
                    cbt++;
                    error++;
                }
            }

        }
        private void event_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftShift:
                    shift_down = false;
                    break;
                case Key.RightShift:
                    shift_down = false;
                    break;
                default:
                    break;
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

        public void ResetWord(RichTextBox obj)
        {
            obj.SelectAll();
            obj.Selection.Text = "";

            TextRange txt_front = new TextRange(obj.Document.ContentEnd, obj.Document.ContentEnd);
            txt_front.Text = front;
            txt_front.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            txt_front.ApplyPropertyValue(TextElement.BackgroundProperty, null);
        }

        public void drop_word(RichTextBox obj)
        {
            back = "";
            front = "";
            btw = '\0';
            resetPosition(obj);
            obj.Dispatcher.Invoke(new UpdateWordBackCall(this.ResetWord), obj);
            obj.IsEnabled = false;
            num_process = -1;
            scr++;
            Dispatcher.Invoke(new UpdateTextBackCall(this.UpdateClock), new object[] { score, scr.ToString() });
        }
    }
}
