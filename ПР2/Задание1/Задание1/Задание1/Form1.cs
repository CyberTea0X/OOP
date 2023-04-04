using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Xml;
using Задание1;

namespace Task1
{
    public partial class Form1 : Form
    {
        internal int[,] matrix1;
        internal int[,] matrix2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length == 0) || (textBox1.Text.Length == 0))
            {
                MessageBox.Show("Поля число1 и число2 не могут быть пустыми");
                return;
            }
            int num1, num2;
            if (!int.TryParse(textBox1.Text, out num1))
            {
                MessageBox.Show("Число в поле 1 слишком большое");
            }
            if (!int.TryParse(textBox2.Text, out num2))
            {
                MessageBox.Show("Число в поле 2 слишком большое");
            }
            int result = Task1.FindGCDEuclid(num1, num2);
            textBox3.Text = result.ToString();

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
                if (textbox.Text.Contains('.') || textbox.Text.Contains(',')) e.Handled = true;
            }
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidNumeric(ref e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidNumeric(ref e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidNumeric(ref e);
            if (e.KeyChar == ',' || e.KeyChar == ' ')
            {
                e.Handled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] input = textBox4.Text.Replace(" ", "").Split(',');
            try
            {
                textBox5.Text = Task2.FindGCDEuclid(input).ToString();
            } 
            catch (System.FormatException) {
                MessageBox.Show("Входная строка имела неправильный формат");
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("Числа слишком большие!");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            string[] input = textBox6.Text.Replace(" ", "").Split(',');
            try
            {
                stopwatch1.Start();
                textBox7.Text = Task3.FindGCDEuclid(input).ToString();
                stopwatch1.Stop();
                stopwatch2.Start();
                textBox8.Text = Task3.FindGCDStein(input).ToString();
                stopwatch2.Stop();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Входная строка имела неправильный формат");
                return;
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("Числа слишком большие!");
                return;
            }
            long elapsedNanoSeconds1 = stopwatch1.ElapsedTicks * 1000000000 / Stopwatch.Frequency;
            long elapsedNanoSeconds2 = stopwatch2.ElapsedTicks * 1000000000 / Stopwatch.Frequency;
            textBox9.Text = elapsedNanoSeconds1.ToString();
            textBox10.Text = elapsedNanoSeconds2.ToString();
            textBox11.Text = Math.Abs(elapsedNanoSeconds1 - elapsedNanoSeconds2).ToString();
        }

        private void textBox_OnlyValid_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyValidNumeric(ref e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0 ) {
                panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
            }
            // Создаем матрицу
            int columns, rows;
            try
            {
                (columns, rows) = (int.Parse(textBox12.Text), int.Parse(textBox13.Text));
            }
            catch (FormatException) {
                MessageBox.Show("Значения столбцов или строк имеют неверный формат!");
                return;
            }
            matrix1 = new int[rows, columns];
            Array.Clear(matrix1, 0, matrix1.Length);
            try
            {
                Task4.ShowMatrix(panel1, in matrix1);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Слишком большая матрица");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (panel2.Controls.Count > 0)
            {
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
            }
            // Создаем матрицу
            int columns, rows;
            try
            {
                (columns, rows) = (int.Parse(textBox15.Text), int.Parse(textBox14.Text));
            }
            catch (FormatException)
            {
                MessageBox.Show("Значения столбцов или строк имеют неверный формат!");
                return;
            }
            matrix2 = new int[rows, columns];
            Array.Clear(matrix2, 0, matrix2.Length);
            try
            {
                Task4.ShowMatrix(panel2, in matrix2);
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Слишком большая матрица");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (matrix1 == null || matrix2 == null)
            {
                MessageBox.Show("Сначала нужно создать матрицы");
                return;
            }
            var (rows1, columns1) = (matrix1.GetLength(0), matrix1.GetLength(1));
            var (rows2, columns2) = (matrix2.GetLength(0), matrix2.GetLength(1));
            if (columns1 != rows2)
            {
                MessageBox.Show("Умножение матриц возможно только в том случае, " +
                    "\nесли число столбцов первой матрицы равно числу строк второй матрицы!");
                return;
            }
            int[,] matrix3 = Task4.MultiplyMatrices(matrix1, matrix2);
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(panel3.Controls.Count - 1);
            }
            Task4.ShowMatrix(panel3, in matrix3);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (matrix1 != null)
            {
                Task4.RandomMatrix(ref matrix1);
                panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
                Task4.ShowMatrix(panel1, matrix1);
            }
            if (matrix2 != null)
            {
                Task4.RandomMatrix(ref matrix2);
                panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                Task4.ShowMatrix(panel2, matrix2);
            }
        }
    }
}
