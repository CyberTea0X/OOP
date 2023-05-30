# Явная реализация интерфейсов
Если класс реализует два интерфейса, а интерфейсы содержат одинаковые методы, то возникает необходимость в явной реализации интерфейсов. Сначала рассмотрим проблемный код.
```C#
// Определим интерфейсы
public interface IControl
{
    void Paint();
}
public interface ISurface
{
    void Paint();
}
// Попробуем их реализовать стандартным способом
public class SampleClass : IControl, ISurface
{
    // Оба ISurface.Paint и IControl.Paint вызывают данный метод
    public void Paint()
    {
        Console.WriteLine("Paint method in SampleClass");
    }
}
SampleClass sample = new SampleClass();
IControl control = sample;
ISurface surface = sample;

// Все эти строки вызовут один и тот же метод.
sample.Paint();
control.Paint();
surface.Paint();

// Вывод (О нет, он одинаковый):
// Paint method in SampleClass
// Paint method in SampleClass
// Paint method in SampleClass
```
А теперь, явная реализация. Сравните с кодом класса `SampleClass` выше, чтобы понять разницу.
```C#
public class SampleClass : IControl, ISurface
{
    void IControl.Paint() // [интерфейс].[метод]
    {
        System.Console.WriteLine("IControl.Paint");
    }
    void ISurface.Paint() // Так и выглядит явная реализация интерфейсов
    {
        System.Console.WriteLine("ISurface.Paint");
    }
}
SampleClass sample = new SampleClass();
IControl control = sample;
ISurface surface = sample;

// Все эти строки вызовут один и тот же метод.
//sample.Paint(); // Compiler error.
control.Paint();  // Вызовет IControl.Paint на SampleClass.
surface.Paint();  // Вызовет ISurface.Paint на SampleClass.

// Так тоже можно
((IControl)sample).Study(); // Вызовет IControl.Paint на SampleClass.
((ISurface)sample).Study(); // Вызовет ISurface.Paint на SampleClass.

// Вывод:
// IControl.Paint
// ISurface.Paint
```

## Модификаторы доступа
Члены интерфейса могут иметь разные модификаторы доступа. Если модификатор доступа не `public`, а какой-то другой, то для реализации метода, свойства или события интерфейса в классах и структурах также необходимо использовать явную реализацию интерфейса.

```C#
interface IMovable
{
    protected internal void Move();
    protected internal string Name { get;}
    delegate void MoveHandler();
    protected internal event MoveHandler MoveEvent;
}
class Person : IMovable
{
    string name;
    // явная реализация события - дополнительно создается переменная
    IMovable.MoveHandler? moveEvent;
    event IMovable.MoveHandler IMovable.MoveEvent
    {
        add => moveEvent += value;
        remove => moveEvent -= value;
    }
    // явная реализация свойства - в виде автосвойства
    string IMovable.Name { get => name; }
    public Person(string name) => this.name = name;
    // явная реализация метода
    void IMovable.Move()
    {
        Console.WriteLine($"{name} is walking");
        moveEvent?.Invoke();
    }
}
```



