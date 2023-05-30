using DeviceControllerNS;
using DeviceTypeNS;
using MeasuringDevice;
using UnitsEnumeration;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        MeasureLengthDevice? device;
        Units unitsToUse = Units.Metric;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            device = new MeasureLengthDevice(unitsToUse);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            device?.StartCollecting();
            if (device == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (device == null)
            {
                MessageBox.Show("Устройство ещё не создано");
                return;
            }
            listBox1.Items.Clear();
            foreach (int el in device.GetRawData())
            {
                listBox1.Items.Add(el);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            device?.StopCollecting();
            if (device == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(device?.MetricValue().ToString());
            if (device == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(device?.ImperialValue().ToString());
            if (device == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }
    }
}