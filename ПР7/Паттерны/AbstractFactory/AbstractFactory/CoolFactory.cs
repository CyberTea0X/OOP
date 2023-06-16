using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class CoolFactory : AbstractWidgetFactory
    {
        public override AbstractWidget MakeButton(string text, Size size, Action action)
        {
            return new CoolButton(text, size, action);
        }

        public override AbstractWidget MakeLabel(string text, Size size)
        {
            return new CoolLabel(text, size);
        }
    }
}
