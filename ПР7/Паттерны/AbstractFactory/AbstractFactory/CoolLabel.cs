using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class CoolLabel : AbstractWidget
    {
        string text;
        Size size;

        public CoolLabel(string text, Size size)
        {
            this.text = text;
            this.size = size;
        }

        public override void spawn(Point point, ContainerControl spawnTarget)
        {
            var label = new Label();
            label.Text = text;
            label.Location = point;
            label.ForeColor = Color.White;
            label.BackColor = Color.Black;
            label.Size = size;
            spawnTarget.Controls.Add(label);
        }
    }
}
