namespace AbstractFactory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            spawnWidgets();
        }

        private void spawnWidgets()
        {
            var random = new Random();
            AbstractWidgetFactory factory;
            if (random.Next(0, 2) == 0)
            {
                 factory = new CoolFactory();
            } else
            {
                factory = new GreenFactory();
            }
            AbstractWidget button = factory.MakeButton("Hi", new Size(100, 50), () => { });
            button.spawn(new Point(10, 20), this);
            AbstractWidget label = factory.MakeLabel("I'm label!", new Size(80, 30));
            label.spawn(new Point(10, 80), this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}