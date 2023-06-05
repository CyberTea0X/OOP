using DeviceTypeNS;
using DeviceControllerNS;
using System.ComponentModel;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
    {
        public int[] dataCaptured;
        public int mostRecentMeasure;
        public DeviceController? controller;
        public DeviceType measurementType;
        private BackgroundWorker dataCollector;
        public bool WorkerSupportsCancellation;
        public bool WorkerReportsProgress;

        public int HeartBeatInterval => throw new NotImplementedException();

        public event EventHandler? NewMeasurementTaken;

        event IEventEnabledMeasuringDevice.HeartBeatEventHandler IEventEnabledMeasuringDevice.HeartBeat
        {
            add { }
            remove { }
        }

        protected virtual void OnNewMeasurementTaken() => NewMeasurementTaken?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Преобразует необработанные данные, собранные устройством измерения, в значение в метрических единицах.
        /// </summary>
        /// <returns>Последнее измерение устройства преобразовано в метрические единицы.</returns>
        public abstract decimal MetricValue();
        /// <summary>
        /// Преобразует необработанные данные, собранные устройством измерения, в значение в имперических единицах.
        /// </summary>
        /// <returns>Последнее измерение устройства преобразовано в имперические единицы.</returns>
        public abstract decimal ImperialValue();
        /// <summary>
        /// Запускает сбор данных устройства измерения.
        /// </summary>
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            GetMeasurements();
        }
        /// <summary>
        /// Останавливает сбор данных устройства измерения.
        /// </summary>
        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }
        }

        public void GetMeasurements()
        {

            dataCollector = new BackgroundWorker();
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
            dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);

            dataCollector.RunWorkerAsync();

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

        private void dataCollector_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }
    }
}
