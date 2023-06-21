using System.Linq;
using System.Text.RegularExpressions;

namespace Strategy
{
    public partial class Form1 : Form
    {
        private SortArrayStrategy<int> sortingStrategy = new SortArrayStrategy<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            // Удаляем всё что не запятая, пробел или число.
            string filteredText = new string(text.Where(c => char.IsDigit(c) || c == ',' || c == ' ').ToArray());

            // Удаляем все повторяющиеся запятые
            filteredText = Regex.Replace(filteredText, ",[ ,]*,", ",");

            // Обновляем текст
            textBox1.Text = filteredText;

            // Устанавливаем позицию выделения в конец текста
            textBox1.SelectionStart = filteredText.Length;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = textBox1.Text.Replace(" ", "").Split(',').Select(int.Parse).ToArray();
            sortingStrategy.Sort(ref nums);
            textBox1.Text = String.Join(", ", nums);
        }
    }
}