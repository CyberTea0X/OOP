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
А теперь, явная реализация. Сравните с кодом класса SampleClass выше, чтобы понять разницу.
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