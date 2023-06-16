using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class CoolButton : AbstractWidget
    {
        string text;
        Size size;
        Action onClick;

        public CoolButton(string text, Size size, Action onClick)
        {
            this.text = text;
            this.size = size;
            this.onClick = onClick;
        }

        public override void spawn(Point point, ContainerControl spawnTarget)
        {
            var button = new Button();
            button.Text = text;
            button.Click += new EventHandler((object? sender, EventArgs e) => onClick());
            button.Location = point;
            button.ForeColor = Color.White;
            button.BackColor = Color.Black;
            button.Size = size;
            spawnTarget.Controls.Add(button);
        }
    }
}
