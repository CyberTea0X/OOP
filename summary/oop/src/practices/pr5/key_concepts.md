# Ключевые понятия
Краткое описание ключевых понятий без особых подробностей.
## Интерфейс
Интерфейс представляет ссылочный тип, который может определять некоторый функционал - набор методов и свойств без реализации. Затем этот функционал реализуют классы и структуры, которые применяют данные интерфейсы.
Для определения интерфейса используется ключевое слово `interface`. Как правило, названия интерфейсов в C# начинаются с заглавной буквы I, например, `IComparable`, `IEnumerable` (так называемая венгерская нотация), однако это не обязательное требование, а больше стиль программирования.
```C#
interface IMovable
{
    // константа
    const int minSpeed = 0;     // минимальная скорость
    // статическая переменная
    static int maxSpeed = 60;   // максимальная скорость
    // метод
    void Move();                // движение
    // свойство
    string Name { get; set; }   // название
     
    delegate void MoveHandler(string message);  // определение делегата для события
    // событие
    event MoveHandler MoveEvent;    // событие движения
}
```
 > Более подробно об [интерфейсах](../../appendix/interfaces.md)
## Абстрактный класс
Абстрактные классы в C#. Для абстрактного класса такое создание объекта через new невозможно, т.к. абстрактный класс - это шаблон или базовый класс, который предназначен для наследования другими классами.
У абстрактных классов могут быть как абстрактные методы (методы без реализации), так и не абстрактные методы. Если у класса есть хотя бы один абстрактный метод, то он должен быть объявлен как абстрактный.
Абстрактные классы могут использоваться в ситуациях, когда вы хотите определить набор общих поведений или свойств, которые должны быть реализованы производными классами. Делая это, вы можете обеспечить согласованный интерфейс для всех классов, которые наследуются от него.
```C#
abstract class Transport
{
    public void Move()
    {
        Console.WriteLine("Транспортное средство движется");
    }
}
```
 > Более подробно об [абстрактных классах](../../appendix/abstract_class.md)
## Множественное наследование интерфейсов
Множественное наследование относится к способности класса наследовать от нескольких базовых классов. C # не поддерживает множественное наследование классов, но он поддерживает множественное наследование с использованием интерфейсов. Это может быть полезно, когда нам нужно реализовать несколько абстракций и наследовать их свойства и методы.
```C#
public interface I1
{
    void Method1();
}

public interface I2
{
    void Method2();
}

public interface I3
{
    void Method3();
}

public class MyClass : I1, I2, I3
{
    public void Method1()
    {
        Console.WriteLine("Method1 called");
    }

    public void Method2()
    {
        Console.WriteLine("Method2 called");
    }

    public void Method3()
    {
        Console.WriteLine("Method3 called");
    }
}
```
 > Более подробно о [множественном наследовании](../../appendix/multiple_interface_inherit.md)
## Явная реализация интерфейсов
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

// Вывод:
// IControl.Paint
// ISurface.Paint
```
 > Более подробно о [явной реализации интерфейсов](../../appendix/explicit_interfaces.md)