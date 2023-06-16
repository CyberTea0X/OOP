using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal abstract class AbstractWidgetFactory
    {
        protected AbstractWidgetFactory()
        {
        }

        public abstract AbstractWidget MakeLabel(string text, Size size);
        public abstract AbstractWidget MakeButton(string text, Size size, Action action);
    }
}
