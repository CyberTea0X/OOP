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
        public bool disposed = false;
        private StreamWriter? loggingFileWriter;
        private BackgroundWorker? heartBeatTimer;

        public event HeartBeatEventHandler? HeartBeat;

        public int heartBeatInterval;
        public int HeartBeatInterval { get => heartBeatInterval; private set => heartBeatInterval = value; }
        private void startHeartBeat()
        {
            dataCaptured = new int[10];
            int i = 0;
            heartBeatTimer = new BackgroundWorker();
            heartBeatTimer.WorkerReportsProgress = true;
            heartBeatTimer.WorkerSupportsCancellation = true;
            heartBeatTimer.DoWork += (e, args) =>
            {
                while (heartBeatTimer?.CancellationPending == false && disposed == false)
                {
                    dataCaptured[i] = controller != null ? controller.TakeMeasurement() : dataCaptured[i];
                    Thread.Sleep(heartBeatInterval);
                    mostRecentMeasure = dataCaptured[i];
                    if (heartBeatTimer?.CancellationPending == false)
                    {
                        loggingFileWriter?.WriteLine($"Measurement - {mostRecentMeasure}");
                        heartBeatTimer.ReportProgress(0);
                        i = (i + 1) % 10;
                    }
                }
            };
            heartBeatTimer.ProgressChanged += (e, args) =>
            {
                OnHeartBeat();
            };
            heartBeatTimer.RunWorkerAsync();
        }

        event IEventEnabledMeasuringDevice.HeartBeatEventHandler IEventEnabledMeasuringDevice.HeartBeat
        {
            add {}
            remove {}
        }

        protected virtual void OnHeartBeat() => HeartBeat?.Invoke(this, new HeartBeatEventArgs());

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
            startHeartBeat();
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
            heartBeatTimer?.CancelAsync();
            Dispose();
        }

        private void Dispose()
        {
            disposed = true;
            heartBeatTimer?.Dispose();
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }
    }
}
