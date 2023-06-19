using System.ComponentModel;

namespace Singleton
{
    public partial class Form1 : Form
    {
        BackgroundWorker blinky;  
        public Form1()
        {
            InitializeComponent();
            new SettingsSingleton("CyberTea", 2, 1);
            var settings = new SettingsSingleton();
            textBoxPlayerName.Text = settings.PlayerName;
            PlayerNameLabel.Text = settings.PlayerName;
            textBoxMaxFps.Text = settings.MaxFps.ToString();
            textBoxMinFps.Text = settings.MinFps.ToString();
            blinky = new BackgroundWorker();
            blinky.WorkerReportsProgress = true;
            blinky.WorkerSupportsCancellation = true;
            blinky.DoWork += (s, e) =>
            {
                while (true)
                {
                    Random random = new Random();
                    switch ((ImageColor)random.Next(0, 3))
                    {
                        case ImageColor.Red:
                            PlayerNameLabel.BackColor = Color.Red; break;
                        case ImageColor.Green:
                            PlayerNameLabel.BackColor = Color.Green; break;
                        case ImageColor.Blue:
                            PlayerNameLabel.BackColor = Color.Blue; break;
                    }
                    var settings = new SettingsSingleton();

                    if (settings.MinFps == null || settings.MaxFps == null)
                    {
                        return;
                    }
                    Thread.Sleep(1000 / random.Next((int)settings.MinFps, (int)(settings.MaxFps + 1)));
                    blinky.ReportProgress(0);
                }
            };
            blinky.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBoxPlayerName.Text;
            var maxFps = int.Parse(textBoxMaxFps.Text);
            var minFps = int.Parse(textBoxMinFps.Text);
            new SettingsSingleton(textBoxPlayerName.Text, maxFps, minFps);
            MessageBox.Show(new SettingsSingleton().MinFps.ToString());
        }
    }
}