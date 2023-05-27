using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice
{
    public interface MeasuringDevice
    {
        /// <summary>
        /// Преобразует необработанные данные, собранные устройством измерения, в значение в метрических единицах.
        /// </summary>
        /// <returns>Последнее измерение устройства преобразовано в метрические единицы.</returns>
        decimal MetricValue();

        /// <summary>
        /// Преобразует необработанные данные, собранные устройством измерения, в значение в имперических единицах.
        /// </summary>
        /// <returns>Последнее измерение устройства преобразовано в имперические единицы.</returns>
        decimal ImperialValue();

        /// <summary>
        /// Запускает сбор данных устройства измерения.
        /// </summary>
        void StartCollecting();

        /// <summary>
        /// Останавливает сбор данных устройства измерения.
        /// </summary>
        void StopCollecting();

        /// <summary>
        /// Предоставляет доступ к необработанным данным, собранным устройством измерения, в любых единицах, используемых устройством.
        /// </summary>
        /// <returns>Необработанные данные, собранные устройством измерения, в их сыром формате.</returns>
        int[] GetRawData();
    }
}

