using DeviceTypeNS;

namespace DeviceControllerNS
{
    internal class DeviceController
    {
        DeviceType measurementType;
        bool isStopped;

        public DeviceController(DeviceType measurementType)
        {
            this.measurementType = measurementType;
            this.isStopped = false;
        }

        public static DeviceController StartDevice(DeviceType measurementType)
        {
            return new DeviceController(measurementType);
        }

        internal void StopDevice()
        {
            this.isStopped = true;
        }

        internal int TakeMeasurement()
        {
            if (!this.isStopped)
            {
                Random random = new Random();
                return random.Next(1, 10);
            }
            throw new Exception("Устройство остановлено, однако, была попытка сделать измерение");
        }
    }
}
