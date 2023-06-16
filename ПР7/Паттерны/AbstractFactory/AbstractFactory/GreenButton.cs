using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class GreenButton: AbstractWidget
    {
        string text;
        Size size;
        Action onClick;

        public GreenButton(string text, Size size, Action onClick)
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
            button.BackColor = Color.Green;
            button.Size = size;
            spawnTarget.Controls.Add(button);
        }
    }
}
