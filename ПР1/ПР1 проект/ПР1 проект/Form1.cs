using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПР1_проект
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ImplicitCasting()
        {
            int firstValue = 25;
            int secondValue = 26;
            long result = firstValue + secondValue;
            /*Из типа  | В тип
            |Sbyte     |short, int, long, float, double, decimal
            |Byte      |short, ushort, int, uint, long, ulong, float, double, decimal
            |Short     |int, long, float, double, decimal
            |Ushort    |int, uint, long, ulong, float, double, decimal
            |Int       |long, float, double, decimal
            |Uint      |long, ulong, float, double, decimal
            |Long      |float, double, decimal
            |Ulong     |float, double, decimal
            |Float     |double
            |Char      |ushort, int, uint, long, ulong, float, double, decimal*/
            /*
            ref long a = ref result;
            result = a + result;*/
            MessageBox.Show(result.ToString()); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ImplicitCasting();
            //MessageBox.Show(result.ToString()); 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Hello, Windows Forms!";
            Console.WriteLine("Hello, windows forms!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
