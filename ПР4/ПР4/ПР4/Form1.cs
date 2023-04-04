using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПР4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            materials.SelectedIndex = 0;
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComplexNumber num1, num2, result = new ComplexNumber(0, 0);
            if (!ComplexNumber.TryParse(textBox2.Text, out num1) ||
                !ComplexNumber.TryParse(textBox3.Text, out num2)) {
                MessageBox.Show("Неверный формат комплексного числа");
                return;
            }
            switch (comboBox1.SelectedItem)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                case "==":
                    textBox4.Text = (num1 == num2).ToString();
                    return;
                case "!=":
                    textBox4.Text = (num1 != num2).ToString();
                    return;
            }
            textBox4.Text = result.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Material material;
            CrossSection cross_section;
            TestResult test_result;
            material = Task2.material_from_string_builder(materials.SelectedItem.ToString());
            cross_section = Task2.cross_sect_from_string_builder(listBox1.SelectedItem.ToString());
            test_result = Task2.test_res_from_string_builder(listBox2.SelectedItem.ToString());
            textBox1.Text = $"{material} {cross_section} {test_result}";
        }

        private void OnlyValidNumeric(ref KeyPressEventArgs e)
        {
            char pressed = e.KeyChar;
            // 8 - это BackSlash
            //MessageBox.Show($"{textBox1.Text.Contains('1')}");
            if (!Char.IsDigit(pressed) && !((int)pressed == 8))
            {
                e.Handled = true;
            }
        }
        private void textBox_OnlyValid_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidNumeric(ref e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            if (textBox6.Text.Length == 0 || textBox9.Text.Length == 0)
            {
                MessageBox.Show("Поля не должны быть пустыми");
                return;
            }
            int tests = int.Parse(textBox6.Text);
            int pressure = int.Parse(textBox9.Text);
            int passed = 0;
            string[] messages = Task3.DoTests(tests, out passed, in pressure);
            for (int i = 0; i < messages.Length; i++)
                textBox5.Text += $"{i+1}. {messages[i]}\r\n";
            textBox7.Text = passed.ToString();
            textBox8.Text = (tests - passed).ToString();
        }
    }
}
