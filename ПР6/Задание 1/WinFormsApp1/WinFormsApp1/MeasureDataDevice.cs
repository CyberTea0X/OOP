using DeviceTypeNS;
using DeviceControllerNS;
using System.ComponentModel;
using System.Linq;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
    {
        public int[] dataCaptured;
        public int mostRecentMeasure;
        public DeviceController? controller;
        public DeviceType measurementType;
        private BackgroundWorker? dataCollector;
        public bool disposed = false;
        private StreamWriter? loggingFileWriter;

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
            string currentTime = DateTime.Now.ToString().Replace(' ', '-').Replace(':', '-');
            loggingFileWriter = new StreamWriter($"log_{currentTime}.txt");
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
            loggingFileWriter?.Close();
            loggingFileWriter?.Dispose();
            dataCollector?.CancelAsync();
            Dispose();
        }

        private void Dispose()
        {
            disposed = true;
            dataCollector?.Dispose();
        }

        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerReportsProgress = true;
            dataCollector.WorkerSupportsCancellation = true;

            dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
            dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);

            dataCollector.RunWorkerAsync();
        }

        private void dataCollector_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;
            while (dataCollector?.CancellationPending == false && disposed == false)
            {
                dataCaptured[i] = controller != null ?
                    controller.TakeMeasurement() : dataCaptured[i];
                mostRecentMeasure = dataCaptured[i];
                loggingFileWriter?.WriteLine($"Measurement - {mostRecentMeasure}");
                Thread.Sleep(500);
                dataCollector.ReportProgress(0);
                i = (i + 1) % 10;
            }
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }
    }
}
