# Множественное наследование
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

При таком наследовании может возникнуть проблема, что несколько интерфейсов определяют один и тот же метод. В таком случае в производном от этих интерфейсов классе можно применять [явную реализацию интерфейсов](./explicit_interfaces.md)