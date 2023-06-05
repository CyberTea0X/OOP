# Задание 1. Использование событий

## Введение

# **ВНИМАНИЕ! ГЛАВА НАХОДИТСЯ В РАЗРАБОТКЕ.**

В этом упражнении мы изменим интерфейс `IMeasuringDevice` и добавим событие `NewMeasurementTaken`. Это событие будет запускаться каждый раз, когда устройство обнаруживает изменение и выполняет новое измерение.
 
Мы изменим абстрактный класс `MeasureDataDevice` из предыдущей лабораторной работы и реализуем событие. Событие `NewMeasurementTaken` произойдет после того, как устройство заполнит внутренний буфер новым измерением и зарегистрирует его.
 
Мы будем использовать компонент `BackgroundWorker` для опроса новых измерений. Опрос новых измерений будет происходить в событии `DoWork`, а событие `ProgressReported` вызовет событие `NewMeasurementTaken`, чтобы уведомить клиентское приложение о том, что было выполнено новое измерение.
 
Мы запустим фоновый поток с помощью метода `RunWorkerAsync`, а устройство будет поддерживать отмену фонового потока с помощью метода `CancelWorkerAsync`.
 
Мы протестируем новую функциональность, используя приложение, которое создает экземпляр класса `MeasureMassDevice` и перехватывает вызываемые им события с помощью делегата. Приложение должно иметь возможность приостановить, а затем перезапустить класс `MeasureMassDevice`.

**Настоятельно рекомендуется скопировать своё решение задания 2 предедущей работы**

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

Самостоятельно добавим приватное поле типа `BackgroundWorker` с названием `dataCollector` в наш класс `MeasureDataDevice`;
```C#
private BackgroundWorker dataCollector;
```

Далее нам придётся изменить метод `GetMeasurments`. 
Делаем его приватным `void`, без параметров. Например, так:

```C#
private void GetMeasurements() {
    //...
}
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

## GetMeasurements

```C#
public void GetMeasurements()
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
```