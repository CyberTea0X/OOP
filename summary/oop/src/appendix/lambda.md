# Лямбда выражения

Лямбда-выражения представляют упрощенную запись анонимных методов. Лямбда-выражения позволяют создать емкие лаконичные методы, которые могут возвращать некоторое значение и которые можно передать в качестве параметров в другие методы.

С точки зрения типа данных лямбда-выражение представляет [делегат](./delegate.md). Например, определим простейшее лямбда-выражение:

```C#
delegate void Message();
Message hello = () => Console.WriteLine("Hello");
hello();  // Hello
```

Если лямбда-выражение содержит несколько действий, то они помещаются в фигурные скобки:

```C#
Message hello = () =>
{
    Console.Write("Hello ");
    Console.WriteLine("World");
};
hello();  // Hello World
```

## Неявная типизация

Начиная с версии C# 10 мы можем применять неявную типизацию (определение переменной с помощью оператора `var`) при определении лямбда-выражения:

```C#
var hello = () => Console.WriteLine("Hello");
hello();  // Hello
```

Но какой тип в данном случае представляет переменная `hello`? При неявной типизации компилятор сам пытается сопоставить лямбда-выражение на основе его определения с каким-нибудь делегатом. Например, выше определенное лямбда-выражение `hello` по умолчанию компилятор будет рассматривать как переменную встроенного делегата `Action`, который не принимает никаких параметров и ничего не возвращает.

## Параметры лямбды

При определении списка параметров мы можем не указывать для них тип данных:

```C#
delegate void Operation(int x, int y);

Operation sum = (x, y) => Console.WriteLine($"{x} + {y} = {x + y}");
sum(1, 2);       // 1 + 2 = 3
```

В данном случае компилятор видит, что лямбда-выражение sum представляет тип `Operation`, а значит оба параметра лямбды представляют тип `int`. Поэтому никак проблем не возникнет.

Однако если мы применяем неявную типизацию, то у компилятора могут возникнуть трудности, чтобы вывести тип делегата для лямбда-выражения, например, в следующем случае:

```C#
var sum = (x, y) => Console.WriteLine($"{x} + {y} = {x + y}");   // ! Ошибка
```

В этом случае нужно явно указать тип параметров:

```C#
var sum = (int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");
sum(22, 14);    // 22 + 14 = 36
```

## Возвращение результата

Лямбда-выражение может возвращать результат. Возвращаемый результат можно указать после лямбда-оператора:

```C#
var sum = (int x, int y) => x + y;
Console.WriteLine(sum(4, 5));  // 9
```
Если лямбда-выражение содержит несколько выражений, тогда нужно использовать оператор `return`, как в обычных методах:

```C#
var subtract = (int x, int y) =>
{
    if (x > y) return x - y;
    else return y - x;
};

Console.WriteLine(subtract(10, 6));     // 4
```

## Добавление и удаление действий в лямбда-выражении

Поскольку лямбда-выражение представляет делегат, тот как и в делегат, в переменную, которая представляет лямбда-выражение можно добавлять методы и другие лямбды:

```C#
var message = () => Console.Write("Hello ");
for (int i = 1; i < 10; i++) {
    message += message;
}
message();  // 512 раз выведет Hello через пробел
```

Лямбды также можно удалять:

```C#
var message = () => Console.Write("Hello ");
var world = () => Console.Write("World!");
var meow = () => Console.Write("Meow! ");
message += world;
message += meow;  // Добавляем Meow
message -= world;  // Уничтожаем мир с помощью C#
message();  // Hello Meow!
```

Как и делегаты, лямбда-выражения можно передавать параметрам метода, которые представляют делегат.

```C#
int[] nums = {1, 2, 3, 4};
nums = nums.Where((num) => num % 2 == 0).ToArray();
foreach (int element in nums)
    Console.Write(element + " "); // 2 4
```

Метод также может возвращать лямбда-выражение. В этом случае возвращаемым типом метода выступает делегат, которому соответствует возвращаемое лямбда-выражение.

```C#
delegate int Operation(int x, int y);

enum OperationType
{
    Add, Subtract, Multiply
}

Operation SelectOperation(OperationType opType)
{
    switch (opType)
    {
        case OperationType.Add: return (x, y) => x + y;
        case OperationType.Subtract: return (x, y) => x - y;
        default: return (x, y) => x * y;
    }
}

Operation operation = SelectOperation(OperationType.Add);
Console.WriteLine(operation(10, 4));    // 14
 
operation = SelectOperation(OperationType.Subtract);
Console.WriteLine(operation(10, 4));    // 6
 
operation = SelectOperation(OperationType.Multiply);
Console.WriteLine(operation(10, 4));    // 40
```