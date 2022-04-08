# Циклы

## Цикл FOR

for ( Инициализация счетчика; Условие работы; Изменение счетчика )

```c#
for (int i = 5; i < 5; i++)
{
    Console.WriteLine(i);
}
```

## Цикл WHILE

Выполняется пока условие истинно.

```c#
int counter = 5;
while (counter > 0)
{
    Console.WriteLine(counter);
    counter--;
}
```

## Цикл DO-WHILE

Выполняется пока условие истинно. Выполнится хотя бы 1 раз, даже если условие ложно.

```c#
int counter = 3;
do
{
    Console.WriteLine(counter);
    counter--;
}
while (counter > 0);
```

## Ключевые слова BREAK и CONTINUE

`continue` - пропускает итерацию цикла.
`break` - прерывает выполнение цикла.

```c#
int condition = 0;
while (condition > 100)
{
    if (condition % 2 == 0)
        continue;

    if (condition > 50)
        break;

    Console.WriteLine(condition);
    condition++;
}
```

## Бесконечные циклы или циклы без условий

```c#
while (true) { }
do { } while (true);
for (; ; ) { }
```
