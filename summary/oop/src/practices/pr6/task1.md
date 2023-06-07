# Задание 1. Использование событий

## Введение

# **ВНИМАНИЕ! ГЛАВА НАХОДИТСЯ В РАЗРАБОТКЕ.**

В этом упражнении мы изменим интерфейс `IMeasuringDevice` и добавим событие `NewMeasurementTaken`. Это событие будет запускаться каждый раз, когда устройство обнаруживает изменение и выполняет новое измерение.
 
Мы изменим абстрактный класс `MeasureDataDevice` из предыдущей лабораторной работы и реализуем событие. Событие `NewMeasurementTaken` произойдет после того, как устройство заполнит внутренний буфер новым измерением и зарегистрирует его.
 
Мы будем использовать компонент `BackgroundWorker` для опроса новых измерений. Опрос новых измерений будет происходить в событии `DoWork`, а событие `ProgressReported` вызовет событие `NewMeasurementTaken`, чтобы уведомить клиентское приложение о том, что было выполнено новое измерение.
 
Мы запустим фоновый поток с помощью метода `RunWorkerAsync`, а устройство будет поддерживать отмену фонового потока с помощью метода `CancelWorkerAsync`.
 
Мы протестируем новую функциональность, используя приложение, которое создает экземпляр класса `MeasureMassDevice` и перехватывает вызываемые им события с помощью делегата. Приложение должно иметь возможность приостановить, а затем перезапустить класс `MeasureMassDevice`.

**Настоятельно рекомендуется скопировать своё решение задания 2 предедущей практики!**

## Расширяем интерфейс

Теперь создадим новый интерфейс с поддержкой событий, который будет наследовать от нашего старого интерфейса `IMeasuringDevice`. Для этого нажмём **(Ctrl + Shift + A)** и создадим интерфейс IEventEnabledMeasuringDevice:

### IEventEnabledMeasuringDevice

```C#
namespace MeasuringDevice
{  
    interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        event EventHandler NewMeasurementTaken;
        // Событие, которое должно срабатывать при каждом Heartbeat
        event HeartBeatEventHandler HeartBeat;
        // Интервал Heartbeat, доступный только для чтения. Устанавливается только в конструкторе.
        int HeartBeatInterval { get; }
    }
}
```

 > Создание нового интерфейса, расширяющего существующий интерфейс, является хорошей практикой программирования, поскольку при этом сохраняется структура исходного интерфейса для обеспечения обратной совместимости с ранее существовавшим кодом. Весь ранее существовавший код может ссылаться на исходный интерфейс, а новый код может ссылаться на новый интерфейс и использовать преимущества любых новых функций.

Заметим, что `HeartBeatEventHandler` подчёркивается красным. Чтобы исправить это безобразие, добавим соответвующий делегат самостоятельно:

```C#
//...
delegate void HeartBeatEventHandler();
// Делегат для события HeartBeat
//...
```

Также исправим тип `NewMeasurementTaken` `EventHandler` на `EventHandler?` чтобы избавиться от зелёной обводки. Таким образом VS говорит нам, `что NewMeasurementTaken` может быть `null`, но об этом позже.

Запустим приложение чтобы Microsoft IntelliSense® отражала наши изменения.

## Реализовываем расширенный интерфейс в абстрактном классе MeasureDataDevice

Теперь отправимся в `MeasureDataDevice` чтобы реализовать наш расширенный интерфейс. 
Далее нам нужно поменять строку:

```C#
public abstract class MeasureDataDevice : IMeasuringDevice
```

на строку:

```C#
public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
```

И тут же `IEventEnabledMeasuringDevice` было обведено красным. Жмём по нему, жмём **(CTRL + '.')**, выбираем реализовать интерфейс и нажимаем.

В нашем классе появляются такие две вещи:

```C#
//...
public event EventHandler NewMeasurementTaken;

event IEventEnabledMeasuringDevice.HeartBeatEventHandler IEventEnabledMeasuringDevice.HeartBeat
{
    add
    {
        throw new NotImplementedException();
    }

    remove
    {
        throw new NotImplementedException();
    }
}
//...
```

Тут просто нужно убрать `throw new NotImplementedException();` и всё будет хорошо.

## Расширяем абстрактный класс и внедряем работу с событиями

Добавим protected virtual метод `OnNewMeasurementTaken`. Метод не должен принимать никаких параметров и иметь возвращаемый тип `void`. Класс `MeasureDataDevice` будет использовать этот метод для создания события `NewMeasurementTaken`.

```C#
protected virtual void OnNewMeasurementTaken() { }
```

В методе `OnNewMeasurementTaken` самостоятельно добавим код для проверки наличия подписчика на событие `NewMeasurementTaken` Если такие имеются, нужно будет вызвать событие. Сигнатура делегата `EventHandler` определяет два параметра: типа object, указывающий объект, вызвавший событие, и параметр `EventArgs`, предоставляющий любые дополнительные данные, которые передаются обработчику событий. Зададим для объекта значение `this`, а для параметра `EventArgs` — значение null.

```C#
protected virtual void OnNewMeasurementTaken() => NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
```

Здесь оператор `?` проверяет `NewMeasurementTaken` на `null` и если `NewMeasurementTaken` не null, то вызывается соответствующее событие через `Invoke`

 > Примечание. Хорошей практикой программирования является проверка наличия подписчиков на событие перед его инициированием. Если у события нет подписчиков, связанный делегат имеет значение null, и среда выполнения .NET Framework выдаст исключение, если возникнет событие.

## Background worker-ы

Самостоятельно добавим `private` поле типа `BackgroundWorker` с названием `dataCollector` в наш класс `MeasureDataDevice`;
```C#
private BackgroundWorker dataCollector;
```

Далее нам придётся полностью переписать `GetMeasurments`. 
Делаем его приватным `void`, без параметров. Например, так:

```C#
private void GetMeasurements() { }
```

В методе `GetMeasurements` самостоятельно добавим код для выполнения следующих действий:

 - Создадим экземпляр элемента DataCollector BackgroundWorker.
 - Укажем, что член `DataCollector` `BackgroundWorker` поддерживает отмену.
 - Укажем, что член `DataCollector` `BackgroundWorker` сообщает о ходе выполнения во время работы.

Для этого сперва создадим свойства `WorkerSupportsCancellation` и `WorkerReportsProgress`.
Их нужно задать в нашем абстрактном классе:

```C#
public bool WorkerSupportsCancellation;
public bool WorkerReportsProgress;
```

Далее добавим следующий код в наш метод:

```C#
dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
```

Данный код добавляет обработчик события `DoWork`, который вызывает наш пока ещё не реализованный метод `dataCollector_DoWork`. 

Теперь похожим образом нужно самостоятельно реализовать обработчик события `ProgressChanged`. В качестве вызываемого метода будет выступать пока ещё не реализованный метод `dataCollector_ProgressChanged`:

```C#
dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);
```

Последний шаг перед реализацией необходимых методов - самостоятельно добавим код, запускающий нашего рабочего:

```C#
dataCollector.RunWorkerAsync();
```

Теперь нам нужно реализовать сами методы. Сперва можно прокликать их и реализовать заглушки:

```C#
private void dataCollector_ProgressChanged(object? sender, ProgressChangedEventArgs e)
{
    throw new NotImplementedException();
}

private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
{
    throw new NotImplementedException();
}
```

Код GetMeasurements, который получился в итоге:

### GetMeasurements

```C#
public void GetMeasurements()
{

    dataCollector = new BackgroundWorker();
    WorkerReportsProgress = true;
    WorkerSupportsCancellation = true;

    dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
    dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);

    dataCollector.RunWorkerAsync();
}
```

## Реализация метода dataCollector_DoWork

Под методом `GetMeasurements` найдите метод `dataCollector_DoWork`.
 
Этот метод мы создали во время выполнения предыдущей задачи. Он работает в фоновом потоке, и его целью является сбор и хранение данных измерений.

### В методе dataCollector_DoWork() необходимо удалить оператор, который генерирует исключение NotImplementedException, и добавить код для выполнения следующих действий:

 - Создать массив `dataCaptured` с помощью нового целочисленного массива, который содержит 10 элементов. Определить целочисленную переменную `i` со значением нуля. Мы будем использовать эту переменную для отслеживания текущей позиции в массиве `dataCaptured`.
 - Добавить цикл `while`, который будет работать до тех пор, пока свойство `dataCollector.CancellationPending` будет равно `false`.

### В цикле while необходимо добавить код для выполнения следующих действий:

 - Вызвать метод `controller.TakeMeasurement()` и сохранить результат в массив `dataCaptured` по позиции, указанной целочисленной переменной `i`. Метод `TakeMeasurement()` объекта `controller` блокируется до тех пор, пока новое измерение не будет готово.

 - Обновить свойство `mostRecentMeasure`, чтобы оно содержало значение `dataCaptured` по позиции, указанной целочисленной переменной `i`.

 - Если значение переменной `disposed` равно `true`, завершить цикл `while`. Этот шаг гарантирует, что сбор измерений останавливается, когда объект `MeasureDataDevice` уничтожается.

 - Кстати, переменной `disposed` у нас никогда и не было, поэтому самостоятельно придумаем что это за переменная и куда её вставлять.

Возможная реализация `disposed`:

```C#
public bool disposed = false;  // Добавляем новое поле в класс MeasureDataDevice
// ...
public void StopCollecting()
{
    if (controller != null)
    {
        controller.StopDevice();
        controller = null;
    }
    disposed = true;  // Устанавливаем disposed как true в случае остановки сбора данных
}
```

Пока что в методе `dataCollector_DoWork` должно быть что-то похожее на это:

```C#
private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
{
    dataCaptured = new int[10];
    int i = 0;

    while (dataCollector?.CancellationPending == false && disposed == false)
    {
        dataCaptured[i] = controller != null ?
            controller.TakeMeasurement() : dataCaptured[i];
        mostRecentMeasure = dataCaptured[i];
    }
}
```

Идём дальше

### После добавления операторов, которые мы добавили на предедущем шаге, необходимо добавить код для выполнения следующих действий:

 - Добавить в наш класс свойство `loggingFileWriter` типа `StreamWriter`, а также инициализировать его в методе `StartCollecting`

 - Если свойство `loggingFileWriter` не равно `null`, вызвать метод `loggingFileWriter.WriteLine()`, передавая строковый параметр формата "Measurement - mostRecentMeasure", где `mostRecentMeasure` - значение переменной `mostRecentMeasure`.

 - Добавить строку кода в конец цикла `while`, чтобы вызвать метод `dataCollector.ReportProgress()`, передавая ему ноль в качестве параметра.

Метод `ReportProgress()` генерирует событие `ReportProgress` и обычно используется для возврата процента завершения задач, назначенных объекту `BackgroundWorker`. Мы можем использовать это событие для обновления индикаторов прогресса или оценок времени в пользовательском интерфейсе. Т. к. задача будет работать бесконечно до отмены, мы будем использовать это событие как механизм для запроса пользовательского интерфейса о обновлении отображения с новым измерением.

Добавим код в конец цикла `while` для выполнения следующих действий:

 - Увеличить целочисленную переменную `i`.
 - Если значение целочисленной переменной больше 9, сбросить `i` в ноль.

Мы используем целочисленную переменную `i` как указатель на следующую позицию, в которую нужно записать измерение, массива `dataCaptured`. Этот массив имеет место для 10 измерений. Когда элемент последний элемент заполнен (его индекс 9), устройство начнет перезаписывать данные с начала массива. 

Возможная реализация `LoggingFileWriter`

#### LoggingFileWriter

```C#
private StreamWriter? loggingFileWriter;
//...
public void StartCollecting()
{
    controller = DeviceController.StartDevice(measurementType);
    loggingFileWriter = new StreamWriter("log.txt");
    GetMeasurements();
}
```

Возможная реализация `dataCollector_DoWork`:

#### dataCollector_DoWork

```C#
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
        dataCollector.ReportProgress(0);
        i = (i + 1) % 10;  // Когда в скобках будет значение 10, операция 10 % 10 даст 0, так это и работает.
    }
}
```

## Реализация метода `dataCollector_ProgressChanged`

 - Найдите в своём коде метод `dataCollector_ProgressChanged`.

 > Этот метод мы недавно добавили как заглушку. 
 Он запускается при возникновении события `ProgressChanged`.
 Это событие мы вызываем, когда метод dataCollector_DoWork принимает и сохраняет новое измерение.
 Код, отвечающий за привязку метода `dataCollector_ProgressChanged`,
 мы реализовали в [GetMeasurements](#getmeasurements)

 - В методе удалите код, вызывающий исключение и затем вызовите метод `OnNewMeasurementTaken` без параметров.

Должно получиться как-то так:

#### dataCollector_ProgressChanged

```C#
private void dataCollector_ProgressChanged(object? sender, ProgressChangedEventArgs e)
{
    OnNewMeasurementTaken();
}
```

 > Метод `OnNewMeasurementTaken` вызывает событие `NewMeasurementTaken`,
 определенное ранее. Мы можем изменить пользовательский интерфейс,
 чтобы подписаться на это событие, чтобы при его возникновении пользовательский интерфейс
 мог обновлять отображаемую информацию.

## Доработка нескольких методов и проверка кода

 - Убедимся, что в методе `StartCollecting` мы вызываем [GetMeasurements](#getmeasurements)
 - в методе `StopCollecting`, если элемент `dataCollector` не является `null`,
 нам нужно добавить вызов метода `CancelAsync`, чтобы остановить работу, выполняемую объектом `DataCollector BackgroundWorker`.