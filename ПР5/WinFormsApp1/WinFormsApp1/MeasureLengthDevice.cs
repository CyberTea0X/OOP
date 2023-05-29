using MeasuringDevice;
using UnitsEnumeration;
using DeviceControllerNS;
using DeviceTypeNS;

namespace WinFormsApp1
{
    public class MeasureLengthDevice : IMeasuringDevice
    {
        private Units unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure;
        private DeviceController? controller;
        private const DeviceType measurementType = DeviceType.LENGTH;

        public MeasureLengthDevice(Units unitsToUse)
        {
            this.unitsToUse = unitsToUse;
            this.dataCaptured = new int[0];
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        public decimal ImperialValue()
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

        public decimal MetricValue()
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

        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            GetMeasurements();
        }

        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }
        }

        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();

                while (controller != null)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ?
                        controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];
                    x++;
                    if (x == 10)
                    {
                        x = 0;
                    }
                }
            });
        }
    }

}
