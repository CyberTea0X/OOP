# Интерфейсы

Интерфейс представляет ссылочный тип, который может определять некоторый функционал - набор методов и свойств без реализации (а после версии  C# 8.0 и с [реализацией по умолчанию](#Реализация-по-умолчанию)). Затем этот функционал реализуют классы и структуры, которые применяют данные интерфейсы.
Для определения интерфейса используется ключевое слово `interface`. Как правило, названия интерфейсов в C# начинаются с заглавной буквы I, например, `IComparable`, `IEnumerable` (так называемая венгерская нотация), однако это не обязательное требование, а больше стиль программирования.
В целом интерфейсы могут определять следующие сущности:

 - Методы

 - Свойства

 - Индексаторы

 - События

 - Статические поля и константы (начиная с версии C# 8.0)

Простейший интерфейс, который определяет все эти компоненты:
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

В данном случае определен интерфейс `IMovable`, который представляет некоторый движущийся объект. Интерфейс описывает некоторый функционал, который должен быть у движущегося объекта.

## Модификаторы доступа

В интерфейсе методы и свойства по умолчанию имеют модификатор `public`, так как цель интерфейса - определение функционала для реализации его классом. Сам интерфейс имеет модификатор доступа `internal` Мы могли бы обратиться к константе `minSpeed` и переменной `maxSpeed` интерфейса `IMovable`:

```C#
Console.WriteLine(IMovable.maxSpeed);   // 60
Console.WriteLine(IMovable.minSpeed);   // 0
```

Но также, начиная с версии C# 8.0, мы можем явно указывать модификаторы доступа у компонентов интерфейса:

```C#
interface IMovable
{
    public const int minSpeed = 0;     // минимальная скорость
    private static int maxSpeed = 60;   // максимальная скорость
    public void Move();
    protected internal string Name { get; set; }    // название
    public delegate void MoveHandler(string message);  // определение делегата для события
    public event MoveHandler MoveEvent;    // событие движения
}
```

Также с помощью модификатора public мы можем сделать интерфейс общедоступным:

```C#
//...
public interface IMovable
//...
```

## Реализация по умолчанию

Также начиная с версии C# 8.0 интерфейсы поддерживают реализацию методов и свойств по умолчанию. Это значит, что мы можем определить в интерфейсах полноценные методы и свойства, которые имеют реализацию как в обычных классах и структурах. Например, определим реализацию метода Move по умолчанию:

```C#
interface IMovable
{
    // реализация метода по умолчанию
    void Move()
    {
        Console.WriteLine("Walking");
    }
}
```

Стоит отметить, что если интерфейс имеет приватные методы и свойства (то есть с модификатором private), то они должны иметь реализацию по умолчанию. То же самое относится к статическим методам (не обязательно приватным):

```C#
interface IMovable
{
    public const int minSpeed = 0;     // минимальная скорость
    private static int maxSpeed = 60;   // максимальная скорость
    // находим время, за которое надо пройти расстояние distance со скоростью speed
    static double GetTime(double distance, double speed) => distance / speed;
    static int MaxSpeed
    {
        get => maxSpeed;
        set
        {
            if (value > 0) maxSpeed = value;
        }
    }
}
```
Благодаря интерфейсам, можно реализовать [множественное наследование](./multiple_interface_inherit.md), которое на самом деле называется композицией. 

## Примеры применения интерфейсов

### Интерфейсы без реализации по умолчанию
```C#
interface IMovable
{
    void Move();
}
class Person : IMovable
{
    public void Move() => Console.WriteLine("Человек идет");
}
struct Car : IMovable
{
    public void Move() => Console.WriteLine("Машина едет");
}

Person Tom = new Person();
Car Volga = new Car();
Tom.Move();  // Выведет: Человек идет
Volga.Move();  // Выведет: Машина едет
```
### Интефейсы с реализацией по умолчанию

```C#
interface IMovable
{
    void Move() => Console.WriteLine("Walking");
}

class Person : IMovable { }

class Car : IMovable
{
    public void Move() => Console.WriteLine("Driving");
}

IMovable tom = new Person();
Car tesla = new Car();
tom.Move();     // Walking
tesla.Move();   // Driving
```

## Бонус: старый синтаксис интерфейсов (C# <8.0)

```C#
interface IMovable
{
    // минимальная скорость
    int minSpeed { get; set; }     
    // максимальная скорость
    int maxSpeed { get; set; }    
    // метод
    void Move();                // движение
    // свойство
    string Name { get; set; }   // название
}
```

Возможно, у вас возник вопрос, чем отличаются абстрактные классы от интерфейсов, тогда вам [сюда](./abstract_classes_vs_interfaces.md)

