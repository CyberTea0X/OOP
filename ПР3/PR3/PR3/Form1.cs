using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR3
{
    public partial class Form1 : Form
    {
        private Switch reactor_switch = new Switch();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string error;
            // DisconnectPowerGenerator
            if (Task1.TryDisconnectPowerGenerator(reactor_switch, out error))
            {
                textBox1.Text = "Disconnect Power Generator: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text = error;
                }
                else
                {
                    textBox1.Text = "Disconnect Power Generator: FAILED";
                }
            }
            // VerifyBackupCoolantSystem
            if (Task1.TryVerifyBackupCoolantSystem(reactor_switch, out error))
            {
                textBox1.Text += "\r\nVerify Backup Coolant System: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;
                    
                }
                else
                {
                    textBox1.Text += "\r\nVerify Backup Coolant System: FAILED";
                }
            }
            // GetCoreTemperature
            double temperature;
            if (Task1.TryGetCoreTemperature(reactor_switch, out temperature, out error)) {
                textBox1.Text += $"\r\nCore temperature: {temperature}";
            }
            else
            {
                textBox1.Text += $"\r\n" + error.ToString();
            }
            // InsertRodCluster
            if (Task1.TryInsertRodCluster(reactor_switch, out error))
            {
                textBox1.Text += $"\r\nInsert Rod Cluster: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;

                }
                else
                {
                    textBox1.Text += "\r\nInsert Rod Cluster: FAILED";
                }
            }
            // GetRadiationLevel
            double radiation;
            if (Task1.TryGetRadiationLevel(reactor_switch, out radiation, out error))
            {
                textBox1.Text += $"\r\nRadiation Level: {radiation}";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;
                }
                else
                {
                    textBox1.Text += "\r\nGet Radiation Level: FAILED";
                }
            }
            // SignalShutdownComplete
            if (Task1.TrySignalShutdownComplete(reactor_switch, out error))
            {
                textBox1.Text += "\r\nSignal Shutdown Complete: SUCCESS";
            }
            else
            {
                if (error.Length > 0)
                {
                    textBox1.Text += "\r\n" + error;
                }
                else 
                { 
                    textBox1.Text += "\r\nSignal Shutdown Complete: FAILED";
                }
            }
        }
    }
}
