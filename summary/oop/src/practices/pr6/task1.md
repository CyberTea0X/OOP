# Задание 1. Использование событий

## Введение

В этом задании мы изменим интерфейс `IMeasuringDevice` и добавим событие `NewMeasurementTaken`. Это событие будет запускаться каждый раз, когда устройство выполняет новое измерение.

Мы изменим абстрактный класс `MeasureDataDevice` из предыдущей лабораторной работы и реализуем в нём событие. Событие `NewMeasurementTaken` будет происходить всякий раз, когда будет добавляться новое измерение в наш буфер.
 
Мы будем использовать компонент `BackgroundWorker` для сбора новых измерений. Сбор новых измерений будет происходить в событии `DoWork`, а событие `ProgressReported` вызовет событие `NewMeasurementTaken`, чтобы уведомить клиентское приложение о том, что было выполнено новое измерение.
 
Мы запустим фоновый поток с помощью метода `RunWorkerAsync`, а устройство будет поддерживать отмену фонового потока с помощью метода `CancelWorkerAsync`.
 
Мы протестируем новую функциональность, используя приложение, которое создает экземпляр класса `MeasureMassDevice` и перехватывает вызываемые им события с помощью делегата. Приложение должно иметь возможность приостановить, а затем перезапустить экземпляр класса `MeasureMassDevice`.

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

Также исправим тип `EventHandler` поля `NewMeasurementTaken` на `EventHandler?` чтобы избавиться от зелёной обводки. Таким образом VS говорит нам, что `NewMeasurementTaken` может быть `null`, но об этом позже.

Запустим приложение чтобы Microsoft IntelliSense® отражала наши изменения.
Запуск на данном этапе должен пройти без проблем.

## Реализовываем расширенный интерфейс в абстрактном классе MeasureDataDevice

Теперь отправимся в `MeasureDataDevice`, чтобы реализовать наш расширенный интерфейс. 
Далее нам нужно поменять строку:

```C#
public abstract class MeasureDataDevice : IMeasuringDevice
```

на строку:

```C#
public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
```

И тут же `IEventEnabledMeasuringDevice` был обведен красным. Жмём по нему, жмём **(CTRL + '.')**, нажимаем реализовать интерфейс.

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

В методе `OnNewMeasurementTaken` самостоятельно добавим код для проверки наличия подписчика на событие `NewMeasurementTaken` Если такие имеются, нужно будет вызвать событие. Сигнатура делегата `EventHandler` определяет два параметра: типа `object`, указывающий объект, вызвавший событие, и параметр `EventArgs`, предоставляющий любые дополнительные данные, которые передаются обработчику событий. Зададим для объекта значение `this`, а для параметра `EventArgs` — значение `EventArgs.Empty`.

```C#
protected virtual void OnNewMeasurementTaken() => NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
```

Здесь оператор `?` проверяет `NewMeasurementTaken` на `null` и если `NewMeasurementTaken` не `null`, то вызывается соответствующее событие через `Invoke`

 > Примечание. Хорошей практикой программирования является проверка наличия подписчиков на событие перед его инициированием. Если у события нет подписчиков, связанный делегат имеет значение null, и среда выполнения .NET Framework выдаст исключение, если возникнет событие.

## Background worker-ы

Самостоятельно добавим `private` поле типа `BackgroundWorker` с названием `dataCollector` в наш класс `MeasureDataDevice`;
```C#
private BackgroundWorker dataCollector;
```

Далее нам придётся полностью переписать `GetMeasurments`. 
Делаем его приватным `void` без параметров. Например, так:

```C#
private void GetMeasurements() { }
```

В методе `GetMeasurements` самостоятельно добавим код для выполнения следующих действий:

 - Создадим экземпляр элемента `DataCollector` `BackgroundWorker`.
 - Укажем, что член `DataCollector` `BackgroundWorker` поддерживает отмену.
 - Укажем, что член `DataCollector` `BackgroundWorker` сообщает о ходе выполнения во время работы.

 > Примечание: у `dataCollector` есть отдельные поля, называющиеся `WorkerReportsProgress` и `WorkerSupportsCancellation`.

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

Возможный код `GetMeasurements`, который получился в итоге:

### GetMeasurements

```C#
public void GetMeasurements()
{

    dataCollector = new BackgroundWorker();
    dataCollector.WorkerReportsProgress = true;
    dataCollector.WorkerSupportsCancellation = true;

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
 - Добавить цикл `while`, который будет работать пока свойство `dataCollector.CancellationPending` является `false`.

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

Итак, чтобы реализовать `FileWriter`, нам понадобиться создавать его экземпляр в `StartCollecting`,
а также где-то закрывать файл и останавливать поток записи в файл.
Позже, когда мы наконец запустим наше приложение и успешно будем делать измерения, при закрытии файла
туда добавятся все наши измерения. В данной реализации с каждым запуском измерений файл логов перезаписывается.

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

Закрытие файла и остановку потока будем производить при остановке сбора измерений:

```C#
public void StopCollecting() {
    //...
    loggingFileWriter?.Close();
    loggingFileWriter?.Dispose();
    //...
}
```

Файл потом можно будет найти примерно где-то в (путь к коду проекта) + `\bin\Debug\net6.0-windows`.

#### dataCollector_DoWork

Возможная реализация `dataCollector_DoWork`:

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
 - Создадим новый метод с названием `Dispose` в классе `MeasureDataDevice`, который будет вызывать `Dispose` на поле
 `dataCollector`, в случае если `dataCollector` не равен `null`. Таким образом мы будем уничтожать наш фоновый сборщик данных при уничтожении экземпляра класса `MeasureDataDevice`.

Вот возможная реализация трёх вышеперечисленных пунктов:

### StartCollecting

```C#
public void StartCollecting()
{
    controller = DeviceController.StartDevice(measurementType);
    loggingFileWriter = new StreamWriter("log.txt");
    GetMeasurements();
}
```

### StopCollecting

```C#
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
```

### Dispose

```C#
private void Dispose()
{
    disposed = true;
    dataCollector?.Dispose();
}
```

Как всегда проверим, что всё работает, простым запуском решения.
На этом моменте всё должно собираться, однако, нажатие кнопки "Начать сбор данных" после создания устройства вызывает полное зависание приложения. Пока лучше не обращать на это внимания.

Займёмся интерфейсом.

## Обновление пользовательского интерфейса для обработки событий измерения

 > Примечание: мы работаем с двумя классами, значит соответствующие переменные и обработчики нужно объявить для обоих. В какой последовательности реализовывать работу с событиями для этих классов - на ваше усмотрение.

Итак, первым делом нам нужно добавить как поле формы делегат для связи с методом `NewMeasurementEvent`.
Этот делегат должен иметь тип `EventHandler` и называться `newMeasurementTaken`.

Далее, в методе, отвечающем за начало сбора измерений, нам необходимо сделать следующее:

 - Инициализируем наш новый делегат `newMeasurementTaken` как `new EventHandler()`, передавая ему метод
 `device_NewMeasurementTaken`. Сам метод мы реализуем немного позже, поэтому пока просто сделаем для него заглушку.
 - Добавим код, чтобы подписать наш новый делегат (обработчик событий) на событие объекта устройства. Под объектом устройства имеется ввиду экземпляр наследника класса `MeasureDataDevice`. Для того, чтобы соединить делегат и событие, можно использовать оператор +=.

Возможный код метода, отвечающего за начало сбора измерений:

### startCollecting_Click

```C#
private void startCollecting_Click(object sender, EventArgs e)
{
    if (device1 == null)
    {
        MessageBox.Show("Устройство ещё не создано");
        return;
    }
    device1.StartCollecting();
    newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
    device1.NewMeasurementTaken += newMeasurementTaken;
}
```

Далее мы реализуем метод `device_NewMeasurementTaken`

### Реализация метода обработки событий device_NewMeasurementTaken.

Необходимо добавить в нашу форму `private` метод обработки событий с названием `device_NewMeasurementTaken`.
Метод не должен возвращать никакого значения и должен принимать следующие параметры:

 - Объект `sender` типа object
 - Объект `e` типа `EventArgs`

Если вы на предедущем шаге делали для него заглушку, то у вас должна быть такая же сигнатура метода, что и описана выше, за исключением того, что sender может быть `null`, тоесть объявлен как `object? sender`.

Далее добавим в метод `device_NewMeasurementTaken` проверку того, что объект устройства не `null`.
После проверки на `null` мы должны обновить пользовательский интерфейс новыми значениями, например:

 - `ListBox` с измерениями
 - `TextBox` Imperial Value
 - `TextBox` Metric Value

Всё это можно сделать при помощи вызова отдельных методов нашего экземпляра устройства, таких как:

 - `MetricValue`
 - `ImperialValue`
 - `GetRawData`

### Отсоединение обработчика событий

В методе, отвечающем за остановку сбора измерений, необходимо добавить код, который отсоединит обработчик событий от объекта устройства. Для того, чтобы отсоединить обработчик, можно использовать оператор -=.

## Бонус - решение утечки памяти и зависания потока

Данная проблема была решена самостоятельно (почти). В оригинальном документе никакого решения нет.
И после запуска сбора измерений, ваш основной поток зависнет.

Возможное решение проблемы:

```C#
private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
{
    //...
    while (dataCollector?.CancellationPending == false && disposed == false)
    {
        //...
        Thread.Sleep(500);  // Добавляем эту строку
        //...
    }
}
```

Пояснение: Так как задержки между измерениями не было, отдельный поток, где сидел наш рабочий без какой-либо задержки вызывал события измерения. Основной поток на обработке этих событий в итоге просто зависал, так как пытался по 100 раз в секунду (а может и 1000, кто его знает сколько) обработать события и обновить интерфейс. 