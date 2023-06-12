using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice
{
    public class HeartBeatEventArgs: EventArgs
    {
        public DateTime TimeStamp { get; }
        public HeartBeatEventArgs() : base()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
