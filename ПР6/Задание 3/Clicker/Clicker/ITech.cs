using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker
{
    public interface ITech
    {
        public event EventHandler<TechEventArgs> TechEvent;
        public void TechUpdate();
    }
}
