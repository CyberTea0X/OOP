# Справка по проектированию с использованием абстрактных классов и интерфейсов

Интерфейсы могут быть удобны при проектировании приложений, так как можно обозначить функционал, которым будущее приложение должно будет обладать, примерную структуру приложения, типы, после чего его можно реализовать хотя бы заглушками и уже это спасёт вам не один час. С грамотными заглушками приложение можно будет тестировать и запускать с самого начала, а низкая связность интерфейсов позволят работать над изолированными частями приложения сразу нескольким программистам параллельно, программистам зачастую не нужно смотреть код других интерфейсов, чтобы делать свой. Технический руководитель может не знать, какова точная бизнес-логика, но с помощью интерфейса может наглядно продемонстрировать своё виденье проекта всем программистам, работающим с классами.

Интерфейсы также могут быть полезны при проектировании приложений, где важно параллельное программирование, из за своей низкой связности.

Далее, интерфейсы можно объединять в единые сущности как абстрактные классы, чтобы потом от них наследовали более конкретные сущности. При этом использование интерфейсов и абстрактных классов избавит от необходимости построения сложной иерархии наследования, что в свою очередь уменьшит количества кода, который придётся изменять как разработчикам приложений при внедрении новых фич, так и пользователям библиотек когда библиотеки будут изменятся. 

## Когда следует использовать абстрактные классы:

 - Если надо определить общий функционал для родственных объектов

 - Если нужно, чтобы все производные классы на всех уровнях наследования имели некоторую общую реализацию. 
 При использовании абстрактных классов, если мы захотим изменить базовый функционал во всех наследниках, то достаточно поменять его в абстрактном базовом классе.

 - Если же нам вдруг надо будет поменять название или параметры метода интерфейса, то придется вносить изменения и также во всех классы, которые данный интерфейс реализуют.

## Когда следует использовать интерфейсы:

 - Если нам надо определить функционал для группы разрозненных объектов, которые могут быть никак не связаны между собой.

 - Если мы проектируем большой громоздкий тип, то лучше будет разбить его на мелкие интерфейсы, после чего собрать их в одном абстрактном классе.

 - Если мы находимся на стадии проектирования нашего приложения и хотим запустить частично рабочую версию максимально быстро, при этом собрав примерную картину нашего приложения без конкретной пока реализации.

Таким образом, если разноплановые классы обладают каким-то общим действием, то это действие лучше выносить в интерфейс. А для одноплановых классов, которые имеют общее состояние, оптимально определить абстрактный класс.