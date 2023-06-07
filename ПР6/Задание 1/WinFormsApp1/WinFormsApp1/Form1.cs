using UnitsEnumeration;
using MeasureMassDeviceNS;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        MeasureLengthDevice? device1;
        MeasureMassDevice? device2;
        Units unitsToUse1 = Units.Metric;
        Units unitsToUse2 = Units.Metric;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            device1 = new MeasureLengthDevice(unitsToUse1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            device1?.StartCollecting();
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
                return;
            }
            listBox1.Items.Clear();
            foreach (int el in device1.GetRawData())
            {
                listBox1.Items.Add(el);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            device1?.StopCollecting();
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(device1?.MetricValue().ToString());
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(device1?.ImperialValue().ToString());
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            device2 = new MeasureMassDevice(unitsToUse2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            device2?.StartCollecting();
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
                return;
            }
            listBox2.Items.Clear();
            foreach (int el in device2.GetRawData())
            {
                listBox2.Items.Add(el);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            device2?.StopCollecting();
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(device2?.MetricValue().ToString());
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(device2?.ImperialValue().ToString());
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }
    }
}