using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Задание3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void only_valid_integers(in TextBox textbox, ref TextCompositionEventArgs e)
        {
            if (e is null) {
                return;
            }
            char pressed = e.Text[0];
            // 8 - это BackSlash
            //MessageBox.Show($"{pressed}");
            if (!Char.IsDigit(pressed))
            {
                e.Handled = true;
            }
        }

        private void textbox_text_changed(object sender, TextCompositionEventArgs e)
        {
            only_valid_integers(textbox1, ref e);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // если пробел, отклоняем ввод
            }
        }
        private bool convert_to_dec(string text, uint from, out long result)
        {
            int _;
            result = 0;
            if (from > 10 || !int.TryParse(text, out _))
            {
                return false;
            }
            uint pos = (uint)text.Length-1;

            foreach (char ch in text) {
                result += (int)(uint.Parse($"{ch}") * Math.Pow(from, pos));
                pos -= 1;
            }
            return true;
        }

        private bool convert_bases(string text, int from, int to, out string result)
        {
            result = string.Empty;
            long decimalNumber;
            if (!convert_to_dec(text, (uint)from, out decimalNumber))
            {
                return false;
            }
            int remainder = 0;
            while (decimalNumber > 0)
            {
                remainder = (int)(decimalNumber % to);
                decimalNumber /= to;
                result = remainder.ToString() + result;
            }
            return true;
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            if (textbox1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Введите число");
                return;
            }
            try
            {
                int fromBase = int.Parse(textBoxFromBase.Text.ToString().Trim());
                int toBase = int.Parse(textBoxIntoBase.Text.ToString().Trim());
                string result = string.Empty;
                if (fromBase <= 10 && toBase <= 10)
                {
                    convert_bases(textbox1.Text.ToString(), fromBase, toBase, out result);
                }
                else
                {
                    result = Convert.ToString(Convert.ToInt64(textbox1.Text.ToString(), fromBase), toBase);
                }
                output.Text = result;
            }
            catch (System.OverflowException) {
                MessageBox.Show("Слишком большое число");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Неверный формат записи числа или конвертация невозможна");
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Неверное основание");
            }
        }

        private void textboxFromBaseGotFocus(object sender, RoutedEventArgs e)
        {
            textbox1.Text = "";
        }

        private void textbox1_text_changed(object sender, TextCompositionEventArgs e)
        {
            if (e is null)
            {
                return;
            }
            char pressed = Char.ToLower(e.Text[0]);

            int num_base;
            if (!int.TryParse(textBoxFromBase.Text, out num_base))
            {
                MessageBox.Show("Неверное основание");
                return;
            }
            if (num_base <= 10 && (97 <= pressed && pressed <= 102))
            {
                e.Handled = true;
            }
            if (!(48 <= pressed && pressed <= 57) && !(97 <= pressed && pressed <= 102)) {
                e.Handled = true;
            }
            if (Char.IsDigit(pressed) && (pressed - '0' > num_base - 1)) {
                e.Handled = true;
            }
            if (num_base > 10)
            {
                if (pressed - 'a' > (num_base - 11))
                {
                    e.Handled = true;
                }
            }
        }
    }
}