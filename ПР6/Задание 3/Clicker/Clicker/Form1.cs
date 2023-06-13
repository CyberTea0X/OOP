using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Clicker
{
    public partial class Form1 : Form
    {
        private Game game;
        public Form1()
        {
            InitializeComponent();
            game = new Game(0, 0, 1000, 1, 1);

            var baker = new IncomeProduct(10, 1.1, 1);
            var bakery = new IncomeProduct(100, 1.2, 5);
            var factory = new IncomeProduct(1000, 1.5, 30);
            var country = new IncomeProduct(100000, 2, 1000);
            var planet = new IncomeProduct(99999999, 3, 1000);
            var clickUpdate = new TechProduct(100, 25);
            var cookieUpdate = new TechProduct(1000, 25);

            game.addIncomeProduct("baker", baker);
            game.addIncomeProduct("bakery", bakery);
            game.addIncomeProduct("factory", factory);
            game.addIncomeProduct("country", country);
            game.addIncomeProduct("planet", planet);

            game.addTechProduct("clickUpdate", clickUpdate);
            game.addTechProduct("cookieUpdate", cookieUpdate);

            game.MoneyChanged += new EventHandler((object? sender, EventArgs e) =>
            {
                Money.Text = game.money.ToString();
                Cookies.Text = game.cookies.ToString();
            });

            game.StartGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Cookie_Click(object sender, EventArgs e)
        {
            game.OnCookieClick();
            Cookies.Text = game.cookies.ToString();
            Money.Text = game.money.ToString();
        }

        private void OnBuyIncomeProduct(string name, TextBox counter, Button button)
        {
            IncomeProduct? prod = game.getIncomeProduct(name);
            if (prod == null)
            {
                return;
            }
            if (game.money >= prod?.getPrice())
            {
                prod.Buy();
                counter.Text = prod.getBougthCount().ToString();
                button.Text = button.Text.Replace($"[{prod.PreviousPrice()}]", $"[{prod.getPrice()}]");
                Income.Text = game.currentIncome.ToString();
            }
        }

        private void Baker_Click(object sender, EventArgs e)
        {
            OnBuyIncomeProduct("baker", BakerCount, Baker);
        }

        private void Bakery_Click(object sender, EventArgs e)
        {
            OnBuyIncomeProduct("bakery", BakeryCount, Bakery);
        }

        private void Factory_Click(object sender, EventArgs e)
        {
            OnBuyIncomeProduct("factory", FactoryCount, Factory);
        }

        private void Country_Click(object sender, EventArgs e)
        {
            OnBuyIncomeProduct("country", CountryCount, Country);
        }

        private void Planet_Click(object sender, EventArgs e)
        {
            OnBuyIncomeProduct("planet", PlanetCount, Planet);
        }

        private void UpdateClick_Click(object sender, EventArgs e)
        {
            TechProduct? prod = game.getTechProduct("clickUpdate");
            if (prod == null)
            {
                return;
            }
            if (game.money >= prod?.getPrice())
            {
                prod.Buy();
                game.clickPrice *= 10;
                UpdateClick.Text = UpdateClick.Text.Replace($"[{prod.PreviousPrice()}]", $"[{prod.getPrice()}]");
            }
        }

        private void UpdateCookie_Click(object sender, EventArgs e)
        {
            TechProduct? prod = game.getTechProduct("cookieUpdate");
            if (prod == null)
            {
                return;
            }
            if (game.money >= prod?.getPrice())
            {
                prod.Buy();
                game.cookiePrice *= 10;
                UpdateClick.Text = UpdateClick.Text.Replace($"[{prod.PreviousPrice()}]", $"[{prod.getPrice()}]");
            }
        }
    }
}