# Задание 1. Использование интерфейсов

Для начала создадим новый проект с графическим интерфейсом, лично я буду использовать старый добрый Windows Forms:

![Скриншот создания проекта](./images/p5-1.png)

Теперь добавим в новый элемент нажатием **Ctrl+Shift+A**, в котором мы будем хранить наш интерфейс измерительного прибора. Назовём его `MeasuringDevice.cs`

Теперь скопируем код с методички, кстати, документацию я перевёл я. Вот как должен выглядить файл после копирования:
 > p.s там есть кнопочка справа вверху кодового блока, можешь не выделять

##
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasuringDevice
{
    public interface IMeasuringDevice
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
```

Теперь нужно создать перечисление Units, которое описывает выбранную систему измерения (метрическая или империческая). Добавим новый элемент нажатием **Ctrl+Shift+A** и назовём его [UnitsEnumeration.cs](#unitsenumerationcs). Нам нужно объявить перечисление через ключевое слово `enum`, назвать Units. Членами перечисления должны быть `Metric` и `Imperial`. Также, можно добавить комментарии. Это всё выполним самостоятельно.

Создадим класс MeasureLengthDevice, где нам нужно будет реализовать выше объявленный интерфейс. Для этого создадим файл [MeasureLengthDevice.cs](#measurelengthdevicecs), где этот класс и определим:

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class MeasureLengthDevice
    {
    }
}
```

Теперь нужно заменить модификатор доступа на `public`


## Код, который нужно было написать самостоятельно:

### UnitsEnumeration.cs 
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitsEnumeration
{
    /// <summary>
    /// Перечисление, которое используется для указания системы измерения.
    /// </summary>
    public enum Units
    {
        Metric,
        Imperial
    }
}
```

### MeasureLengthDevice.cs
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class MeasureLengthDevice
    {
    }
}
```
