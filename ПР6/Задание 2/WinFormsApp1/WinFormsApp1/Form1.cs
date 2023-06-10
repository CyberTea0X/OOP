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
        EventHandler newMeasurementTaken;


        public Form1()
        {
            InitializeComponent();
        }

        private void createDevice1_Click(object sender, EventArgs e)
        {
            device1?.StopCollecting();
            device1 = new MeasureLengthDevice(unitsToUse1);
        }

        private void startCollecting1_Click(object sender, EventArgs e)
        {
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
                return;
            }
            newMeasurementTaken = new EventHandler(device1_NewMeasurementTaken);
            device1.NewMeasurementTaken += newMeasurementTaken;
            device1.StartCollecting();
        }

        private void device1_NewMeasurementTaken(object? sender, EventArgs e)
        {
            if (device1!= null)
            {
                metricValue1.Text = device1.MetricValue().ToString();
                ImperialValue1.Text = device1.ImperialValue().ToString();
                listBox1.Items.Clear();
                foreach (int el in device1.GetRawData())
                {
                    listBox1.Items.Add(el);
                }
            }
        }

        private void StopCollecting1_Click(object sender, EventArgs e)
        {
            device1?.StopCollecting();
            if (device1 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }

        private void createDevice2_Click(object sender, EventArgs e)
        {
            device2?.StopCollecting();
            device2 = new MeasureMassDevice(unitsToUse2);
        }

        private void startCollecting2_Click(object sender, EventArgs e)
        {
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
                return;
            }
            newMeasurementTaken = new EventHandler(device2_NewMeasurementTaken);
            device2.NewMeasurementTaken += newMeasurementTaken;
            device2.StartCollecting();
        }

        private void device2_NewMeasurementTaken(object? sender, EventArgs e)
        {
            if (device2 != null)
            {
                metricValue2.Text = device2.MetricValue().ToString();
                ImperialValue2.Text = device2.ImperialValue().ToString();
                listBox2.Items.Clear();
                foreach (int el in device2.GetRawData())
                {
                    listBox2.Items.Add(el);
                }
            }
        }

        private void StopCollecting2_Click(object sender, EventArgs e)
        {
            device2?.StopCollecting();
            if (device2 == null)
            {
                MessageBox.Show("Устройство ещё не создано");
            }
        }
    }
}