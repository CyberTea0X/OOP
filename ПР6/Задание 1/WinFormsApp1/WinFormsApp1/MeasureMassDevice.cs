using DeviceTypeNS;
using MeasuringDevice;
using UnitsEnumeration;

namespace MeasureMassDeviceNS
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        private Units unitsToUse;
        new private const DeviceType measurementType = DeviceType.MASS;

        public MeasureMassDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
            this.dataCaptured = new int[0];
        }
        override public decimal ImperialValue()
        {
            switch (unitsToUse)
            {
                case Units.Metric:
                    return (decimal)(mostRecentMeasure * 2.2046226);
                case Units.Imperial:
                    return mostRecentMeasure;
                default:
                    throw new NotImplementedException();
            }
        }

        override public decimal MetricValue()
        {
            switch (unitsToUse)
            {
                case Units.Metric:
                    return mostRecentMeasure;
                case Units.Imperial:
                    return (decimal)(mostRecentMeasure * 0.4535924);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
