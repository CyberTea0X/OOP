# Делегаты
Делегаты - это указатели на методы и с помощью делегатов мы можем вызвать методы по указателю.

## Определение делегатов

Для объявления делегата используется ключевое слово `delegate`, после которого идет возвращаемый тип, название и параметры. Например:

```C#
delegate void Message();
```

Делегат `Message` в качестве возвращаемого типа имеет тип `void` (то есть ничего не возвращает) и не принимает никаких параметров. Это значит, что этот делегат может указывать на любой метод, который не принимает никаких параметров и ничего не возвращает.

Рассмотрим примение этого делегата:

```C#
using System;

public class Program
{
    static void Hello() => Console.WriteLine("Amogus ඞ");  // Объявляем метод
    delegate void Message();  // Объявляем делегат

    public static void Main()
    {
        Message mes = Hello;      // Присваиваем переменной mes адрес метода Hello
        mes();                    // Вызываем метод Hello, выведет: Amogus ඞ
    }
}
```

При этом делегаты также могут указывать на методы, определённые в других классах и структурах:

```C#
using System;

public class Amogus 
{
	public static void Hello() => Console.WriteLine("Amogus ඞ");
}

public class Program
{
    void Hello() => Console.WriteLine("Amogus ඞ"); 
    delegate void Message(); 

    public static void Main()
    { 
        Message mes = Amogus.Hello;    
        mes();                      // Вызываем метод Hello, выведет: Amogus ඞ
    }
}
```

## Параметры и результат делегата

Рассмотрим определение и применение делегата, который принимает параметры и возвращает результат:

```C#
using System;

public class Calculator
{
	public static int Add(int x, int y) => x + y;
	public static int Multiply(int x, int y) => x * y;
}

public class Program
{
    delegate int Operation(int x, int y);

    public static void Main()
    { 
		Operation op = Calculator.Add;
		Console.WriteLine(op(1, 2));  // 3
		op = Calculator.Multiply;
		Console.WriteLine(op(1, 2));  // 2
    }
}
```

В данном случае делегат `Operation` возвращает значение типа `int` и имеет два параметра типа `int`. Поэтому такому делегату соответствует любой метод, который возвращает значение типа `int` и принимает два параметра типа `int`. В данном случае это методы `Add` и `Multiply`. То есть мы можем присвоить переменной делегата любой из этих методов и вызывать.

## Присвоение ссылки на метод

Выше переменной делегата напрямую присваивался метод. Есть еще один способ - создание объекта делегата с помощью конструктора, в который передается нужный метод:

```C#
//...
Operation operation1 = Add;
Operation operation2 = new Operation(Add);
//...
```

Оба способа равноценны.

## Соответствие методов делегату

`ref`, `in` и `out` также определяют сигнатуру делегата:

```C#
delegate void SomeDel(int a, double b);

void SomeMethod1(int g, double n) { };  // Соответствует делегату
double SomeMethod2(int g, double n) { return g + n; }  // Не соответствует делегату
void SomeMethod3(out int g, double n) { g = 6; }  // Не соответствует делегату
```

## Добавление методов в делегат

В примерах выше переменная делегата указывала на один метод. В реальности же делегат может указывать на множество методов, которые имеют ту же сигнатуру и возвращаемый тип. Все методы в делегате попадают в специальный список - список вызова или **invocation list**. При вызове делегата все методы из этого списка последовательно вызываются. Для добавления методов в делегат применяется операция +=:

```C#
Message message = Hello;
message += HowAreYou;  // теперь message указывает на два метода
message();              // вызываются оба метода - Hello и HowAreYou
 
void Hello() => Console.WriteLine("Hello");
void HowAreYou() => Console.WriteLine("How are you?");
 
delegate void Message();  // Выведет на одной строке Hello, на другой: How are you?
```
Делегаты, которые включают в себя больше одного метода, называют **мультикаст-делегатами**.

При добавлении методов, которые возвращают какой-то результат, делегат вернёт результат последнего метода:

```C#
using System;

public class Calculator
{
	public static int Add(int x, int y) {
		Console.WriteLine("Gonna do some math");
		return x + y;
	}
	public static int Multiply(int x, int y) => x * y;
}

public class Program
{
    delegate int Operation(int x, int y);

    public static void Main()
    { 
		Operation op = Calculator.Add;
		op += Calculator.Multiply;
		Console.WriteLine(op(1, 2));  // Выведет на одной строке: Gonna do some math, на другой строке: 2
    }
}
```

## Удаление методов из делегата

Мы можем удалять методы из делегата с помощью операций -=:

```C#
Message? message = Hello; 
message += HowAreYou;
message();  // вызываются все методы из message
message -= HowAreYou;   // удаляем метод HowAreYou
if (message != null) message(); // вызывается метод Hello
```

Стоит отметить, что при удалении метода может сложиться ситуация, что в делегате не будет методов, и тогда переменная будет иметь значение `null`. Поэтому в данном случае переменная определена не просто как переменная типа `Message`, а именно `Message?`, то есть типа, который может представлять как делегат `Message`, так и значение `null`.

Кроме того, перед вторым вызовом мы проверяем переменную на значение `null`.

При удалении следует учитывать, что если делегат содержит несколько ссылок на один и тот же метод, то операция -= начинает поиск с конца списка вызова делегата и удаляет только первое найденное вхождение. Если подобного метода в списке вызова делегата нет, то операция -= не имеет никакого эффекта.


## Метод Invoke()

Также делегат можно вызвать через `Invoke` - такой метод есть у всех делегатов и это всеравно что вызвать сам делегат. `Invoke` полезен при проверке делегатов на `null` через оператор ?:
```C#
Message? mes = Hello;
mes?.Invoke(); // Amogus ඞ"
```

## Объединение делегатов

```C#
Message mes1 = Hello;
Message mes2 = HowAreYou;
Message mes3 = mes1 + mes2; // объединяем делегаты
mes3(); // вызываются все методы из mes1 и mes2
 
void Hello() => Console.WriteLine("Hello");
void HowAreYou() => Console.WriteLine("How are you?");
 
delegate void Message();
```

В данном случае объект `mes3` представляет объединение делегатов `mes1` и `mes2`. Объединение делегатов значит, что в список вызова делегата `mes3` попадут все методы из делегатов `mes1` и `mes2`. И при вызове делегата `mes3` все эти методы одновременно будут вызваны.

## Обобщённые делегаты

Делегаты, как и другие типы, могут быть обобщенными (написаны через генерики), например:

```C#
delegate T Operation<T>(T val);
int Double(int n) => n + n;

Operation<int> doubleOperation = Double;
Console.WriteLine(doubleOperation(5));  // 10
```

## Делегаты как параметры методов
Также делегаты могут быть параметрами методов. Например:

```C#
delegate int Operation(int x, int y);

int Add(int x, int y) => x + y;

void DoOperation(int a, int b, Operation op)
{
    Console.WriteLine(op(a,b));
}

DoOperation(5, 4, Add);  // 9
```

## Возвращение делегатов из метода

Делегаты можно возвращать из методов. То есть мы можем возвращать из метода какое-то действие в виде другого метода. Например:

```C#
delegate int Operation(int x, int y);
 
int Add(int x, int y) => x + y;
int Subtract(int x, int y) => x - y;
int Multiply(int x, int y) => x * y;

enum OperationType
{
    Add, Subtract, Multiply
}

Operation SelectOperation(OperationType opType)
{
    switch (opType)
    {
        case OperationType.Add: return Add;
        case OperationType.Subtract: return Subtract;
        default: return Multiply;
    }
}

Operation operation = SelectOperation(OperationType.Add);
Console.WriteLine(operation(10, 4));    // 14
```

Делегатами также являются и лямбда выражения, но это уже [отдельная тема](./lambda.md).
