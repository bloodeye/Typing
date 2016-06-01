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
        public delegate void UpdateWordBackCall(RichTextBox obj);
        string back;
        string front = "";
        char btw;
        int error, cbt;
        char p_word;
        bool shift_down = false;

        public Typing_Screen()
        {
            
            InitializeComponent();
            TextRange txt = new TextRange(words_box.Document.ContentEnd, words_box.Document.ContentEnd);
            txt.Text = "The neck pencils the predictable continental near the caffeine.";
            back = new TextRange(words_box.Document.ContentStart, words_box.Document.ContentEnd).Text;
            btw = back[0];
            back = back.Remove(0, 1);
            Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), words_box);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    }else
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
            if (shift_down)
            {
                if (p_word == btw)
                {
                    front += btw;
                    btw = back[0];
                    back = back.Remove(0, 1);
                    cbt++;
                    words_box.Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words_box });
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
                    words_box.Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words_box });
                }
                else
                {
                    cbt++;
                    error++;
                }
            }
            if (back.Length == 1)
            {
                MessageBox.Show("Endgame!");
                this.Close();
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
                    words_box.Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words_box });
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
                    words_box.Dispatcher.Invoke(new UpdateWordBackCall(this.UpdateWord), new object[] { words_box });
                }
                else
                {
                    cbt++;
                    error++;
                }
            }
            if (back.Length == 1)
            {
                MessageBox.Show("Endgame!");
                this.Close();
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

    }

}
