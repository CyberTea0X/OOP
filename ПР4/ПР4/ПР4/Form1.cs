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
    }
}
