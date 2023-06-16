using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class GreenLabel : AbstractWidget
    {
        string text;
        Size size;

        public GreenLabel(string text, Size size)
        {
            this.text = text;
            this.size = size;
        }

        public override void spawn(Point point, ContainerControl spawnTarget)
        {
            var label = new Label();
            label.Text = text;
            label.Location = point;
            label.BackColor = Color.Green;
            label.Size = size;
            spawnTarget.Controls.Add(label);
        }
    }
}
