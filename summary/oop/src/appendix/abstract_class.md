# Абстрактный класс
Абстрактные классы в C#. Для абстрактного класса такое создание объекта через new невозможно, т.к. абстрактный класс - это шаблон или базовый класс, который предназначен для наследования другими классами.
У абстрактных классов могут быть как [абстрактные методы](#Абстрактные-методы) (методы без реализации), так и не абстрактные методы. Если у класса есть хотя бы один абстрактный метод, то он должен быть объявлен как абстрактный.
Абстрактные классы могут использоваться в ситуациях, когда вы хотите определить набор общих поведений или свойств, которые должны быть реализованы производными классами (однако, если производный класс абстрактный, [этого можно и не делать](#Отказ-от-реализации-абстрактных-членов)).

Определив абстрактный класс, вы можете обеспечить согласованный интерфейс для всех классов, которые наследуются от него.

При определении абстрактных классов используется ключевое слово `abstract`. Например, определим абстрактный класс, который представляет некое транспортное средство:
```C#
abstract class Transport
{
    public void Move()
    {
        Console.WriteLine("Транспортное средство движется");
    }
}
```

А также определим несколько производных классов

```C#
// класс корабля
class Ship : Transport { }
// класс самолета
class Aircraft : Transport { }
// класс машины
class Car : Transport { }
```

```C#
Transport car = new Car();
Transport ship = new Ship();
Transport aircraft = new Aircraft();
car.Move(); // Выведет: Транспортное средство движется
ship.Move(); // Выведет: Транспортное средство движется
aircraft.Move(); // Выведет: Транспортное средство движется
```

Транспортное средство представляет некоторую абстракцию, которая не имеет конкретного воплощения. То есть есть легковые и грузовые машины, самолеты, морские судна, кто-то на космическом корабле любит покататься, но как такового транспортного средства нет. Тем не менее все транспортные средства имеют нечто общее - они могут перемещаться. И для этого в классе определен метод Move, который эмулирует перемещение.

Главное отличие абстрактных классов от обычных состоит в том, что мы НЕ можем использовать конструктор абстрактного класса для создания экземпляра класса. Например, следующий код выдаст ошибку:

```C#
Transport tesla = new Transport();  // Ошибка компиляции
```

## Конструкторы
Выше писалось, что мы не можем использовать конструктор абстрактного класса для создания экземпляра этого класса. Тем не менее такой класс также может определять конструкторы:

```C#
abstract class Transport
{
    public string Name { get; }
    // конструктор абстрактного класса Transport
    public Transport(string name)
    {
        Name = name;
    }
    public void Move() =>Console.WriteLine($"{Name} движется");
}
```

В данном случае в абстрактном классе Transport определен конструктор - с помощью параметра он устанавливает значение свойства Name, которое хранит название транспортного средства. И в этом случае производные классы должны в своих конструкторах вызвать этот конструктор.

## Абстрактные члены классов

Кроме обычных свойств и методов абстрактный класс может иметь абстрактные члены классов, которые определяются с помощью ключевого слова `abstract` и не имеют никакого функционала. В частности, абстрактными могут быть:

 - [Методы](#Абстрактные-методы)

 - [Свойства](#Абстрактные-свойства)

 - Индексаторы

 - События

 Абстрактные члены классов не должны иметь модификатор `private`. При этом производный класс обязан переопределить и реализовать все абстрактные методы и свойства, которые имеются в базовом абстрактном классе.

### Абстрактные методы

Например, выше в примере с транспортными средствами метод Move описывает передвижение транспортного средства. Однако различные типы транспорта перемещаются по разному - ездят по земле, летят по воздуху, плывут на воде и т.д. В этом случае мы можем сделать метод `Move` абстрактным.

```C#
abstract class Transport
{
    public abstract void Move();
}
```

А его реализацию метода `Move` переложить на производные классы:

```C#
class Ship : Transport 
{
    // мы должны реализовать все абстрактные методы и свойства базового класса
    public override void Move()
    {
        Console.WriteLine("Корабль плывет");
    }
}

class Aircraft : Transport
{
    public override void Move()
    {
        Console.WriteLine("Самолет летит");
    }
}

class Car : Transport
{
    public override void Move()
    {
        Console.WriteLine("Машина едет");
    }
}
```

Применение классов:

```C#
Transport car = new Car();
Transport ship = new Ship();
Transport aircraft = new Aircraft();
 
car.Move();         // Выведет: Машина едет
ship.Move();        // Выведет: Корабль плывет
aircraft.Move();    // Выведет: Самолет летит
```

### Абстрактные свойства

Следует отметить использование абстрактных свойств:

```C#
abstract class Transport
{
    // абстрактное свойство для хранения скорости
    public abstract int Speed { get; set; } 
}
```

Определим производный класс:

```C#
class Ship: Transport
{
    int speed;
    public override int Speed 
    { 
        get => speed; 
        set => speed = value; 
    }
}
```

### Отказ от реализации абстрактных членов

Производный класс обязан реализовать все абстрактные члены базового класса. Однако мы можем отказаться от реализации, но в этом случае производный класс также должен быть определен как абстрактный:

```C#
abstract class Transport
{
    public abstract void Move();
}
// класс машины
abstract class Car :Transport{}
```

Однако любые неабстрактные классы, производные от Car, все равно должны реализовать все унаследованные абстрактные методы и свойства:

```C#
/// Класс легковой машины
class Auto: Car
{
    public override void Move()
    {
        Console.WriteLine("легковая машина едет");
    }
}

Transport tesla = new Auto();
tesla.Move();           // легковая машина едет
```

Возможно, у вас возник вопрос, чем отличаются абстрактные классы от интерфейсов, тогда вам [сюда](./abstract_classes_vs_interfaces.md)