# Регулярные выражения

[Регулярные выражения](https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/regular-expressions)
[Пространство имен](https://docs.microsoft.com/ru-ru/dotnet/api/system.text.regularexpressions?view=net-5.0)
[Класс Regex](https://docs.microsoft.com/ru-ru/dotnet/api/system.text.regularexpressions.regex?view=net-5.0)

## МЕТАСИМВОЛЫ символы для составления Шаблона поиска

`\w` - Любой текстовый символ, не являющийся пробелом, символом табуляции и т.п.
`\W` - Любой символ, не являющийся текстовым символом
`\s` - Любой пробельный символ из набора Unicode
`\S` - Любой не пробельный символ из набора Unicode. Символы `\w` и `\S` - это не одно и то же
`\d` - Любые ASCII-цифры. Эквивалентно [0-9]
`\D` - Любой символ, отличный от ASCII-цифр.
`.`  - Определяет любой символ кроме символа новой строки.
`\.` - Определяет символ точки.

## КЛАССЫ СИМВОЛОВ

`[...]` - Любой из символов, указанных в скобках  -> `[a-z]` - В исходной строке может быть любой символ английского алфавита в нижнем регистре
`[^...]` - Любой из символов, не указанных в скобках -> `[^0-9]` В исходной строке может быть любой символ кроме цифр

## КВАНТИФИКАТОРЫ

*Символы которые определяют, где и сколько раз необходимое вхождение символов может встречаться.*
`^` - С начала строки.
`$` - С конца строки
`?` - Соответствует нулю или одному экземпляру предшествующего шаблона; Эквивалентно {0,1}
`+` - Соответствует одному или более экземплярам предшествующего шаблона; Эквивалентно {1,}
`*` - Соответствует нулю или более экземплярам предшествующего шаблона; Эквивалентно {0,}

## СИМВОЛЫ ПОВТОРЕНИЯ

`{n,m}` - Соответствует предшествующему шаблону, повторенному не менее n и не более m раз шаблон {2,4} "Press", "ssl", "progressss"
`{n,}` - Соответствует предшествующему шаблону, повторенному n или более раз шаблон {1,} "ssl"
`{n}` - Соответствует в точности n экземплярам предшествующего шаблона шаблон {2} "Press", "ssl", но не "progressss"

## СИМВОЛЫ РЕГУЛЯРНЫХ ВЫРАЖЕНИЙ ВЫБОРА

`|` - Соответствует либо подвыражению слева, либо подвыражению справа (аналог логической операции ИЛИ).
`(...)` - Группировка. Группирует элементы в единое целое, которое может использоваться с символами *, +, ?, | и т.п.
Также запоминает символы, соответствующие этой группе для использования в последующих ссылках.
`(?:...)` - Только группировка. Группирует элементы в единое целое, но не запоминает символы, соответствующие этой группе.
`\b` - Соответствует границе слова, т.е. соответствует позиции между символом \w и символом \W или между символом \w и началом или концом строки. \b(my)\b В строке "Hello my world" выберет слово "my"
`\B` - Соответствует позиции, не являющейся границей слов. `\B(ld)\b` Соответствие найдется в слове "World", но не в слове "ld"

## МЕТАСИМВОЛЫ ЗАМЕНЫ

`$ number` Замещает часть строки, соответствующую группе number `\b(\w+)(\s)(\w+)\b` `$3$2$1` "один два" -> "два один"
`$$` Подставляет литерал "$" `\b(\d+)\s?USD` `$$$1` "103 USD" -> "$103"
`$&` Замещает копией полного соответствия `(\$*(\d*(\.+\d+)?){1})` `**$&"$1.30" -> "**$1.30**"`
`$` Замещает весь текст входной строки до соответствия `B+` `$` "AABBCC" -> "AAAACC"
`$'` Замещает весь текст входной строки после соответствия `B+` `$'` "AABBCC" -> "AACCCC"
`$+` Замещает последнюю захваченную группу `B+(C+)` `$+` "AABBCCDD" -> "AACCDD"
`$_` Замещает всю входную строку `B+` `$_` "AABBCC" -> "AAAABBCCCC"

***

## Regex.IsMatch

```c#
string pattern = @"\d+";
var regex = new Regex(pattern, RegexOptions.CultureInvariant);
while (true)
{
    Console.WriteLine("Введите символ для сравнения: ");
    string input = Console.ReadLine(); //Строка с которой будет сравнивать шаблон.
    
    if (input == "exit")
        break;
    
    Console.WriteLine(
        input != null && regex.IsMatch(input)
        ? $"Строка {input} соответствуен шаблону {pattern}"
        : $"Строка {input} не соответствуен шаблону {pattern}"
        );
    
    Console.WriteLine(new string('-', 25));
}
```

## Regex.Replace

Метод Regex.Replace заменяет в первом параметре - строке (myString) подстроку соответствующую шаблону (String) во втором параметре, на подстроку-вставку в третьем параметре (Test).

```c#
Regex.Replace("myString","String","Test"); // результат myTest  

Console.WriteLine(Regex.Replace("test123aaa3x5x6bbb789ccc", //Исходная строка
                                 @"\d+",                    // Шаблон
                                 " "));                     // Символ замены

Console.WriteLine(Regex.Replace("02/05/1982",                                           // Исходная строка
                                @"(?<месяц>\d{1,2})/(?<день>\d{1,2})/(?<год>\d{2,4})",  // Шаблон
                                "${день}-${месяц}-${год}"));                            // Новый формат

Console.WriteLine(Regex.Replace(@"test123firststr456secondstr",                         // Исходная строка.
                                @"test(?<var1>\d+)firststr(?<var2>\d+)secondstr",       // Шаблон.
                                @"test${var2}firststr${var1}secondstr"));               // Новый формат.

Console.WriteLine(Regex.Replace("5 is less than 10",                                    // Исходная строка.
                                @"\d+",                                                 // Шаблон.
                                m => (int.Parse(m.Value) + 1).ToString()));             // Функция изменения совпадения

string result = string.Empty;
// Замена недопустимых символов пустыми символами.
result = Regex.Replace("@_H e l l o-777.,:;'!@#$%^&*()_-+<>?/",
                      @"[^\w\.@-]", "");
```

## Match

Match представляет подстроку соответствующую шаблону. Match.Success - булево значение, которое показывает найдено вхождение или нет. Все переменные объявленные в шаблоне( (?`<link>`) и (?`<text>`) ) хранятся в коллекции Mathes.Groups. В нашем случае нам придётся использовать m.Groups["link"] и m.Groups["text"], для вывода значения каждой переменной.

```c#
string input = "";
input += "<a href='http://cbsystematics.com'>Home-page</a>";
input += "<a href='http://google.com'>Search</a>";
input += "<a href='https://ya.ru'>Yandex</a>";
input += "<a href='https://yandex.ru'>Yandex Full</a>";
input += "<a href='http://microsoft.com'>Microsoft</a>";

var regex = new Regex(@"href='(?<link>\S+)'>(?<text>\S+)</a>");
Console.WriteLine(input);
for (var m = regex.Match(input); m.Success; m = m.NextMatch())
{
    // {0,-25} - значит что выделить 25 знакомест под вывод {0}. (-) - значит "прижаться" влево :)
    Console.WriteLine("ССЫЛКА: {0,-25} на: {1,-4} позиции с именем: {2}", m.Groups["link"],
                                                                          m.Groups["link"].Index,
                                                                          m.Groups["text"]);
}

foreach (Match m in regex.Matches(input))
{
    Console.WriteLine("ССЫЛКА: {0,-25} на: {1,-4} позиции с именем: {2}", m.Groups["link"],
                      m.Groups["link"].Index,
                      m.Groups["text"]);
}

var htmlQuery = from Match m in regex.Matches(input)
                where m.Groups["link"].Value.StartsWith("https")
                select m;

foreach (var m in htmlQuery)
{
    Console.WriteLine("ССЫЛКА: {0,-25} на: {1,-4} позиции с именем: {2}", m.Groups["link"],
                     m.Groups["link"].Index,
                     m.Groups["text"]);
}
```

## MatchCollection

Regex помещает все найденные шаблоном комбинации и помещает их в коллекцию

```c#
Regex regex = new Regex(@"[0-9A-Za-z_.-]+@[0-9a-zA-Z-]+\.[a-zA-Z]{2,4}");
MatchCollection collection = regex.Matches("русский edu@cbsystematics.com текст123ещерусскийsupport@cbsystematics.comтекст");
foreach (Match element in collection)
{
    Console.WriteLine("{0,-25}  на {1,-3} позиции.", element.Value, element.Index);
}
```

## Примеры применения паттернов

```c#
Regex regex;
string pattern;
string text;

// Возможно указать необходимые символы между скобок [].
pattern = "^[qwerty]+$";
text = "qwec"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// Строка может состоять только из символов - [qwerty]. Например:  qqq, yyqyqyyyq, eeer ...
pattern = "^[qwerty]+$";
text = "qrwere";  // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

pattern = "^[abcdefghigklmnopqrstuvwxyz]+$";
text = "test"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// Второй способ, a-z это замена более длинного шаблона abcdefghigklmnopqrstuvwxyz
pattern = @"^[a-z]+$";
text = "test"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// 0-9 это замена 01234567890.
pattern = "^[a-z0-9]+$";
text = "test007"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// 0-9 это замена 01234567890.
pattern = "^[a-z0-9]+$";
text = "test 007"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// Шаблон даты.
pattern = @"^\d{2}-\d{2}-\d{4}$";
text = "02-05-1982"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// Квантификатор * значит, что вхождение 0 и более раз...
pattern = @"^\d*$";
text = "ertty"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// Квантификатор * значит, что вхождение 0 и более раз...
pattern = @"^\d*$";
text = ""; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));

// Простой шаблон сравнения e-mail.
pattern = @"^[0-9a-z_-]+@[\S]+\.\S{2,4}$";
text = "test@mail123.rлu"; // Анализируемая строка.
regex = new Regex(pattern);
Console.WriteLine("{0} == {1} : {2}\n", text, pattern, regex.IsMatch(text));
```
