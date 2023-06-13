# Задание 2. Использование лямбда-выражений

В этом задании мы объявим новый тип делегата и новый тип `EventArgs` для поддержки события `HeartBeat`. Мы изменим интерфейс `IMeasuringDevice` и класс `MeasureDataDevice`, чтобы сгенерировать heartbeat с помощью объекта `BackgroundWorker`. Мы укажем код для запуска в новом потоке с помощью лямбда-выражения.

В обработчике событий `ReportProgress` мы реализуем код для уведомления клиентского приложения с помощью другого лямбда-выражения.
 
Мы будем обрабатывать событие `HeartBeat` в приложении с помощью лямбда-выражения.

 > **Для выполнения данного задания необходимо скопировать своё решение из 1-го задания данной практики.**

## Создаём пользовательский класс аргументов события

Нажимаем **(Ctrl+Shift+A)** и таким образом объявим класс `HeartBeatEventArgs`.
Небходимо добавить данный класс в пространство имён `MeasuringDevice`.
Также данный класс должен наследовать от класса `EventArgs`.

Должно получиться что-то похожее:

```C#
namespace MeasuringDevice
{
    public class HeartBeatEventArgs: EventArgs
    {
    }
}
```

Пользовательский класс аргументов события может содержать любое количество свойств; эти свойства сохраняют информацию при возникновении события, позволяя обработчику событий получать информацию, относящуюся к событию, когда событие обрабатывается.

В класс `HeartBeatEventArgs` добавим доступное только для чтения авто-свойство `DateTime` с именем `TimeStamp`.

Добавим конструктор в класс `HeartBeatEventArgs`. Конструктор не должен принимать аргументы и инициализировать свойство `TimeStamp` датой и временем создания класса. Конструктор также должен расширять конструктор базового класса.

 > Нажмите на класс, а затем нажмите **(CTRL + '.')**, после чего выберите "Создать конструктор `HeartBeatEventArgs`"

Возможная реализация:

### HeartBeatEventArgs

```C#
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
```

## Объявление обработчика нашего события

В том же файле, либо в новом, объявим `public delegate` с названием `HeartBeatEventHandler`.

Делегат должен ссылаться на метод, который не возвращает значение, но имеет следующие параметры:
 - Параметр типа `object` с именем `sender`.
 - Параметр `HeartBeatEventArgs` с именем `args`.

Возможная реализация:

### HeartBeatEventHandler

```C#
namespace MeasuringDevice
{
    public delegate void HeartBeatEventHandler (object sender, HeartBeatEventArgs e);
}
```

## Обновляем интерфейс IEventEnabledMeasuringDevice.



Откроем файл с интерфейсом `IEventEnabledMeasuringDevice`.

Добавим доступное только для чтения целочисленное авто-свойство с названием `HeartBeatInterval` в интерфейс.

**Если, конечно, такого свойства нет.**

Если что, примерно так должен выглядеть интефейс в итоге:

```C#
namespace MeasuringDevice
{
    interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        // Интервал Heartbeat, доступный только для чтения. Устанавливается только в конструкторе.
        int HeartBeatInterval { get; }

        event EventHandler NewMeasurementTaken;
        // Событие, которое должно срабатывать при каждом Heartbeat

        delegate void HeartBeatEventHandler();
        // Делегат для события HeartBeat

        event HeartBeatEventHandler HeartBeat;
    }
}
```

## Добавим событие `HeartBeat` и свойство `HeartBeatInterval` в класс `MeasureDataDevice`.

Найдём класс `MeasureDataDevice`. 

Добавим `public int` свойство `HeartBeatInterval`, которое определяет интерфейс `IEventEnabledMeasuringDevice`. Свойство должно возвращать значение члена `heartBeatInterval` при вызове метода доступа `get`. Свойство должно иметь метод доступа `private set`, чтобы только конструктор мог установить свойство. Кстати, `heartBeatInterval` необходимо объявить самостоятельно. Лучше его сделать публичным...

Возможная реализация:

```C#
public int heartBeatInterval;
public int HeartBeatInterval { get => heartBeatInterval; private set => heartBeatInterval = value; }
```

Добавим событие `HeartBeat`, которое определяет интерфейс `IEventEnabledMeasuringDevice`.

```C#
public event HeartBeatEventHandler? HeartBeat;
```

Добавим `protected virtual void` метод `OnHeartBeat` не принимающий параметры.

В методе `OnHeartBeat` добавим код для выполнения следующих действий:

 - Проверим что у события `HeartBeat` есть обработчики.
 - Если есть, вызовем событие, передавая текущий объект и новый экземпляр `HeartBeatEventArgs` как параметры.

Возможная реализация:

```C#
protected virtual void OnHeartBeat() => HeartBeat?.Invoke(this, new HeartBeatEventArgs());
```

## Используем объект BackgroundWorker для генерации heartbeat

определим `private` поле типа `BackgroundWorker` с именем `heartBeatTimer`.

Объявим закрытый метод с именем `StartHeartBeat`, который не принимает параметров и не возвращает значение.

Далее в этом методе:
 - Создадим объект `heartBeatTimer` типа `BackgroundWorker`
 - Настроим объект `heartBeatTimer` для поддержки отмены.
 - Настроим объект `heartBeatTimer` для поддержки уведомлений о ходе выполнения.

Также в методе `StartHeartBeat` добавим обработчик для события `heartBeatTimer` `DoWork`, используя лямбда-выражение для определения выполняемых действий. Лямбда-выражение должно принимать два параметра (e, args). Внутри лямбда-выражения добавим цикл `while`, который постоянно повторяется и содержит код для выполнения следующих действий:
 - запуск `Thread.Sleep`, чтобы перевести текущий поток в спящий режим на время, указанное в свойстве `HeartBeatInterval`.
 - Проверка значения свойства `disposed`. Если значение истинно, завершить цикл.
 - Вызов метода `heartBeatTimer.ReportProgress`, с параметром 0.

 > Примечание. Можно использовать составной оператор присваивания +=, чтобы указать, что метод будет обрабатывать событие `DoWork`, определить сигнатуру лямбда-выражения, а затем использовать оператор => для обозначения начала тела лямбда-выражения.

В методе `StartHeartBeat` добавим обработчик события `heartBeatTimer.ReportProgress`, используя другое лямбда-выражение для создания тела метода. В тело лямбда-выражения добавим код для вызова метода `OnHeartBeat`, который вызывает событие `HeartBeat`.

В конце метода `StartHeartBeat` добавим строку кода, чтобы запустить асинхронно работающий объект `HeartBeatTimer` `BackgroundWorker`.

Возможная реализация:

```C#
private void startHeartBeat()
{
    heartBeatTimer = new BackgroundWorker();
    heartBeatTimer.WorkerReportsProgress = true;
    heartBeatTimer.WorkerSupportsCancellation = true;
    heartBeatTimer.DoWork += (e, args) =>
    {
        while (!disposed)
        {
            Thread.Sleep(heartBeatInterval);
            heartBeatTimer.ReportProgress(0);
        }
    };
    heartBeatTimer.ProgressChanged += (e, args) =>
    {
        OnHeartBeat();
    };
    heartBeatTimer.RunWorkerAsync();
}
```

## Вызовем метод StartHeartBeat при запуске объекта MeasureDataDevice.

В методе StartCollecting добавим строчку чтобы вызвать метод `StartHeartBeat`
На этом моменте становится ясно, что небходимо будет удалить некоторый код из предедущего задания. Займёмся этим немного позже.

## Удалим объект HeartBeatTimer BackgroundWorker при уничтожении объекта MeasureDataDevice.

В методе `Dispose` добавим код для проверки того, что объект `HeartBeatTimer` `BackgroundWorker` не равен `null`. Если объект `heartBeatTimer` не равен `null`, вызовем метод `Dispose` объекта `BackgroundWorker`.

Теперь мы обновили абстрактный класс `MeasureDataDevice` для реализации обработчиков событий с помощью лямбда-выражений. Чтобы приложение могло воспользоваться этими изменениями, необходимо изменить класс `MeasureMassDevice`(А также чуть позже `MeasureLengthDevice`), который расширяет класс `MeasureDataDevice`.

## Обновим конструктор класса MeasureMassDevice.

 - Откроем файл класса `MeasureMassDevice`.
 - Изменим сигнатуру конструктора, чтобы он принимал дополнительное целочисленное значение с именем `heartBeatInterval`.
 - Изменим тело конструктора, чтобы сохранить значение `heartBeatInterval` в  `heartBeatInterval` поле.

Под существующим конструктором добавим второй конструктор, который принимает следующие параметры:
 - Экземпляр `Units` с именем `deviceUnits`.
 - Строку `logFileName`.
 - Изменим новый конструктор, чтобы он неявно вызывал существующий конструктор. Передадим значение 1000 в качестве значения параметра `heartBeatInterval`.

 > Кстати, `logFileName` неизвестно вообще куда девать, поэтому можно тихонько от него избавиться, ну либо придумывать зачем он нам всё-таки нужен и как-то связывать с сохранением записей в файл.

Возможная реализация:

```C#
public MeasureMassDevice(Units unitsToUse, int heartBeatInterval)
{
    this.unitsToUse = unitsToUse;
    this.dataCaptured = new int[0];
    this.heartBeatInterval = heartBeatInterval;
}

public MeasureMassDevice(Units deviceUnits) : this(deviceUnits, 1000) { }
```

## Последние штрихи перед работой с UI

Теперь нужно разобраться с тем, что мы натворили.

С предедущего задания у нас остался `dataCollector`. Его стоит удалить из нашего абстрактного класса `MeasureDataDevice`.

Вместе с удалением `dataCollector` сломались методы `GetMeasurements` и `dataCollector_DoWork`. Смело их удаляем.

Некоторые методы также сломались, но там нужно просто подправить пару вещей и где-то заменить `dataCollector` на `heartBeatTimer`.

А теперь проделываем предедущий пункт с классом `MeasureLengthDevice`.

Далее можем запустить решение. Скорее всего всё соберётся и даже можно будет понажимать кнопочки, однако,
приложение ничего не будет делать.

А знаете почему оно ничего не делает? Потому что в  методе `startHeartBeat` класса `MeasureDataDevice` никто не позаботился о том, чтобы делать измерения. Таким образом необходимо прикрутить отвечающий за всё это код.

Возможная реализация:

### startHeartBeat

```C#
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
            loggingFileWriter?.WriteLine($"Measurement - {mostRecentMeasure}");
            heartBeatTimer.ReportProgress(0);
            i = (i + 1) % 10;
        }
    };
    heartBeatTimer.ProgressChanged += (e, args) =>
    {
        OnHeartBeat();
    };
    heartBeatTimer.RunWorkerAsync();
}
```

Ещё одна проблема - мы не удалили старый обработчик событий. Поэтому вот эту строчку также необходимо удалить:

```C#
public event EventHandler? NewMeasurementTaken;
```

А вместе с ней и метод `OnNewMeasurementTaken`, а также `dataCollector_ProgressChanged`.

Также возможно придётся забраться в интерфейс `IEventEnabledMeasuringDevice` чтобы удалить оттуда строчку:

```C#
event EventHandler NewMeasurementTaken;
```

## Работа с UI

Теперь в UI нам нужно подписаться на другой обработчик событий.

Где-то внутри формы среди скорее всего есть строчка:

```C#
EventHandler newMeasurementTaken;
```

Её можно смело удалить.

Далее необходимо почистить метод, отвечающий за обработку нажатия "Начать сбор данных".
Метод в итоге должен выглядить как-то так:

```C#
private void startCollecting1_Click(object sender, EventArgs e)
{
    if (device1 == null)
    {
        MessageBox.Show("Устройство ещё не создано");
        return;
    }
    device1.StartCollecting();
}
```

Далее нам необходимо добавить обработчик события `device.HeartBeat`
В обработчике можно вызвать уже существующий в форме метод `device_NewMeasurementTaken`.
Этот метод отвечает за получение нового измерения.

Либо можно последовать методичке и реализовать вместо этого метода лямбда выражение, где мы могли бы добавить код для обновления метки `HeartBeat` `TimeStamp` текстом «HeartBeat Timestamp: timestamp», где timestamp — это значение свойства `args.TimeStamp`.

Дальнейшая реализация на ваше усмотрение.

Например, можно было бы реализовать так:

```C#
private void startCollecting1_Click(object sender, EventArgs e)
{
    if (device1 == null)
    {
        MessageBox.Show("Устройство ещё не создано");
        return;
    }
    device1.HeartBeat += new HeartBeatEventHandler(device1_NewMeasurementTaken);
    device1.StartCollecting();
}
```


















