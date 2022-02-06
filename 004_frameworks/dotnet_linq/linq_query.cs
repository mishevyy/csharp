string[] words = { "one", "two", "three", "zero", };
int[] numbers = { 1, 2, 3, 4, 5, };

// Агрегирование
int sum = numbers.Sum();
int count = numbers.Count();
double average = numbers.Average();
long longCount = numbers.LongCount();
int min = numbers.Min();
int max = numbers.Max();

string agg = words.Aggregate("seed",
    (current, item) => current + item,
    resultSelector => resultSelector.ToUpper());

// Конкатенация
int[] newArray = numbers.Concat(new int[] { 1, 2, 3, 4, 5, }).ToArray();

// Преобразование
object[] allStrings = { "These", "are", "all", "string" };
object[] notAllStrings = { "Number", "at", "the", "end", 5 };

string[] castString = allStrings.Cast<string>().ToArray();
string[] ofTypeString = notAllStrings.OfType<string>().ToArray();

var toDict = words.ToDictionary(k => k.Substring(0, 2));
var toLookup = words.ToLookup(word => word[0]);

// Операции элементов
var el1 = words.ElementAt(2);
var el2 = words.ElementAtOrDefault(10);
var el3 = words.First();
var el4 = words.First(w => w.Length == 3);
var el5 = words.Last();
// var el6 = words.Single();
// var el7 = words.SingleOrDefault();

// Эквивалентность
var equ = words.SequenceEqual(new[] { "zero", "one", "two" });

// Генерация
var g1 = numbers.DefaultIfEmpty();
var g2 = Enumerable.Range(0, 100);
var g3 = Enumerable.Repeat(5, 2);
var g4 = Enumerable.Empty<int>();

// Группировка
var gr1 = words.GroupBy(g => g.Length);
var gr2 = words.GroupBy(g => g.Length, (key, h) => key + ": " + h.Count());

// Соединение
string[] names = { "Robing", "Ruth", "Bob", "Emma" };
string[] colors = { "Red", "Blue", "Beige", "Green" };

var jn1 = names.Join(
    colors,
    name => name[0],
    color => color[0],
    (name, color) => name + " - " + color);

var jn2 = names.GroupJoin(
    colors,
    name => name[0],
    color => color[0],
    (name, matches) => name + ": " + string.Join("/", matches.ToArray()));

// Разделение
var take = words.Take(2);
var skip = words.Skip(1);
var takeWile = words.TakeWhile(w => w.Length > 3);

// Проецирование
var sl1 = words.Select(s => new { Word = s });
var sl2 = words.SelectMany(word => word.ToCharArray());
var sl3 = names.Zip(colors, (x, y) => x + " " + y);

// Квантификаторы
var qt1 = words.All(w => w.Length > 3);
var qt2 = words.Any();
var qt3 = words.Contains("four");

// Фильтрация
var fl1 = words.Where(w => w.Length == 2);

// Операции основанные на множествах
string[] abcd = { "a", "b", "c", "d" };
string[] cd = { "c", "d" };

var mn1 = abcd.Distinct();
var mn2 = abcd.Intersect(cd);
var mn3 = abcd.Union(cd);
var mn4 = abcd.Except(cd);

// Сортировка
var sort1 = words.OrderBy(o => o);
var sort2 = words.OrderBy(o => o[2]);
var sort3 = words.OrderByDescending(o => o);
var sort4 = words.OrderBy(o => o.Length).ThenBy(tb => tb);
var sort5 = words.Reverse();