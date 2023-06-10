using DeviceControllerNS;
using DeviceTypeNS;
using MeasuringDevice;
using UnitsEnumeration;

namespace WinFormsApp1
{
    public class MeasureLengthDevice : MeasureDataDevice
    {
        private Units unitsToUse;
        new private const DeviceType measurementType = DeviceType.LENGTH;
        public MeasureLengthDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
            this.dataCaptured = new int[0];
        }

        override public decimal ImperialValue()
        {
            switch (unitsToUse)
            {
                case Units.Metric:
                    return (decimal)(mostRecentMeasure * 0.03937);
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
                    return (decimal)(mostRecentMeasure * 25.4);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
