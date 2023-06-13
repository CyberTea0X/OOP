using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public class Game
    {
        public int currentIncome;
        public int money;
        public int cookies;
        public int incomeCd;
        public int cookiePrice;
        public int clickPrice;
        public Dictionary<string, IncomeProduct> incomeProducts = new Dictionary<string, IncomeProduct>();
        public Dictionary<string, TechProduct> techProducts = new Dictionary<string, TechProduct>();
        private BackgroundWorker IncomeWorker;
        public event EventHandler<IncomeEvent>? GotIncome;
        public event EventHandler? MoneyChanged;

        public Game(int currentIncome, int money, int incomeCd, int cookiePrice, int clickPrice)
        {
            this.currentIncome = currentIncome;
            this.money = money;
            this.incomeCd = incomeCd;
            this.cookiePrice = cookiePrice;
            this.clickPrice = clickPrice;
            IncomeWorker = new BackgroundWorker();
            IncomeWorker.WorkerSupportsCancellation = true;
            IncomeWorker.WorkerReportsProgress = true;
        }

        public void StartGame()
        {
            IncomeWorker.ProgressChanged += new ProgressChangedEventHandler((object? sender, ProgressChangedEventArgs e) =>
            {
                GotIncome?.Invoke(this, new IncomeEvent(currentIncome));
                onMoneyChange();
            });
            IncomeWorker.DoWork += (target, e) =>
            {
                while (IncomeWorker.CancellationPending == false)
                {
                    money += currentIncome * cookiePrice;
                    cookies += currentIncome;
                    IncomeWorker.ReportProgress(0);
                    Thread.Sleep(incomeCd);
                }
            };
            IncomeWorker.RunWorkerAsync();
        }

        public void OnCookieClick()
        {
            cookies += clickPrice;
            money += clickPrice * cookiePrice;
        }

        public void addIncomeProduct(string name, IncomeProduct incomeProduct)
        {
            incomeProduct.IncreaseIncome += (object? sender, IncreaseIncomeEventArgs e) =>
            {
                currentIncome += e.IncomeAdds;
            };
            incomeProduct.BoughtEvent += (object? sender, BuyEventArgs e) =>
            {
                money -= e.price;
                onMoneyChange();
            };
            incomeProducts.Add(name, incomeProduct);
        }

        public void addTechProduct(string name, TechProduct techProduct)
        {
            techProducts.Add(name, techProduct);
            techProduct.BoughtEvent += (object? sender, BuyEventArgs e) =>
            {
                money -= e.price;
                onMoneyChange();
            };

        }

        private void onMoneyChange() => MoneyChanged?.Invoke(this, new EventArgs());

        public IncomeProduct? getIncomeProduct(string name)
        {
            return incomeProducts.TryGetValue(name, out IncomeProduct? incomeProduct) ? incomeProduct : null;
        }

        public TechProduct? getTechProduct(string name)
        {
            return techProducts.TryGetValue(name, out TechProduct? techProduct) ? techProduct : null;
        }

        public Product? GetProduct(string name)
        {
            IncomeProduct? product1;
            if (incomeProducts.TryGetValue(name, out product1))
            {
                return product1;
            }
            TechProduct? product2;
            techProducts.TryGetValue(name, out product2);
            return product2;
        }
    }
}
