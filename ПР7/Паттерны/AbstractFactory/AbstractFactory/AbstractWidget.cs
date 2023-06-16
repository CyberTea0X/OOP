using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal abstract class AbstractWidget
    {
        public abstract void spawn(Point point, ContainerControl spawnTarget);
    }
}
