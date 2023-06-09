# Модель событий .NET Framework

Все события в .NET Framework основаны на делегате `EventHandler`. Первый параметр этого делегата - это объект,
посылающий уведомление. 
В качестве второго параметра он принимает класс наследующий `EventArgs`. Использование такого
шаблона позволит подписываться на события, объявленные в классах .NET Framework. Класс `EventArgs` не содержит никакой полезной информации. Тем не менее он может быть использован в
ситуациях, когда важен сам факт того, что событие произошло. В этом случае нужно пользоваться полем Empty.
Также, от класса `EventArgs` можно наследовать, таким образом передавая полезную информацию о событии, например, так:

```C#
public class SomeEventArgs : EventArgs
{
    public SomeEventArgs(string s) { message = s; }
    private string message;
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
}
```

## Определения `EventHandler` и `EventArgs` в .NET Framework.
```C#
public delegate void EventHandler( Object sender, EventArgs e );
```

```C#
public class EventArgs
{
    public static readonly EventArgs Empty;
}
```