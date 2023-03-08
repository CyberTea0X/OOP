using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Задание2
{
    public partial class Form1 : Form
    {
        decimal guess;
        decimal delta = Convert.ToDecimal(Math.Pow(10, -28));
        public Form1()
        {
            InitializeComponent();
        }

        private void OnlyValidDecimal(in TextBox textbox, ref KeyPressEventArgs e)
        {
            char pressed = e.KeyChar;
            // 8 - это BackSlash
            //MessageBox.Show($"{textBox1.Text.Contains('1')}");
            if (!Char.IsDigit(pressed) && !((int)pressed == 8))
            {
                if ((pressed != '.') && (pressed != ','))
                {
                    e.Handled = true;
                }
                if (textBox1.Text.Contains('.') || textBox1.Text.Contains(',')) e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidDecimal(in textBox1,ref e);
            clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string entered_value = textBox1.Text.ToString();
            double parsed_value;
            string error_message;
            if (!parse_double_value(in entered_value, out parsed_value, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            label1.Text = $"{Math.Sqrt(parsed_value)}";
        }

        private bool parse_double_value(in string to_parse, out double parsed, out string error_message)
        {
            error_message = "";
            if (!double.TryParse("0" + to_parse.Replace('.', ','), out parsed))
            {
                error_message = "Пожалуйста, введите дробное число";
                return false;
            }
            if (parsed <= 0.0)
            {
                error_message = "Пожалуйста, введите положительное число, не равное нулю";
                return false;
            }
            return true;
        }

        private bool parse_decimal_value(in string to_parse, out decimal parsed, out string error_message)
        {
            double parsed_double;
            parsed = 0;
            if (!parse_double_value(in to_parse, out parsed_double, out error_message)) {
                return false;
            }
            try
            {
                parsed = (decimal)parsed_double;
            }
            catch (System.OverflowException)
            {
                error_message = "Слишком большое число";
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            string entered_value = textBox1.Text.ToString();
            decimal number_decimal;
            string error_message;
            if (!parse_decimal_value(in entered_value, out number_decimal, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            decimal result = number_decimal / 2;
            while (Math.Abs(result - this.guess) > this.delta)
            {
                do_newton_iter(in number_decimal, ref result, ref this.guess);
            }
            decimal change = this.guess - result;
            label9.Text = $"{Math.Abs(change)}";
            decimal error = Math.Abs(result - this.guess);
            label10.Text = $"{error}";
            label2.Text = $"{result}";
        }
        private void do_newton_iter(in decimal number, ref decimal result, ref decimal guess)
        {
            guess = result;
            result = ((number / guess) + guess) / 2;
            label6.Text = $"{int.Parse(label6.Text.ToString()) + 1}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string entered_value = textBox1.Text.ToString();
            decimal number_decimal;
            string error_message;
            if (!parse_decimal_value(in entered_value, out number_decimal, out error_message))
            {
                MessageBox.Show(error_message);
                return;
            }
            decimal result;
            entered_value = label2.Text.ToString();
            if (!parse_decimal_value(entered_value, out result, out error_message))
            {
                result = number_decimal / 2;
            }
            do_newton_iter(in number_decimal, ref result, ref this.guess);
            decimal change = Math.Abs(this.guess - result);
            label9.Text = $"{change}";
            decimal error = Math.Abs(result - this.guess);
            label10.Text = $"{error}";
            label2.Text = $"{result}";
        }
        private void clear()
        {
            label2.Text = "0.00";
            label6.Text = "0";
            label9.Text = "0";
            label10.Text = "0";
        }
    }
}
