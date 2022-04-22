# CLR Команды

dotnet -?|-h|--help Выводит список доступных команд
dotnet --info Выводит подробные сведения об установке .Net
dotnet --version Отображает версию пакета SDK для .NET, используемую командами dotnet.
dotnet sdk check Проверка sdk
dotnet build - Собирает проект
dotnet new Создает новый проект
dotnet restore Востанавливает пакеты проекта
dotnet new --list Выводит списко проектов которые можно создать
dotnet run Запускает проект
dotnet watch run Запускает проект в режиме хот релоад
dotnet tool 'команда' Команды для работы инструментами

dotnet cli

cls - очистить powershell

dotnet
dotnet --info

dotnet new gitignore -- создать гитигнор фаил

dotnet -h показать команды
dotnet new --list - список проектов которые можно создать

dotnet new sln - создать решение
dotnet new console -o DemoApp - добивить проект
dotnet sln add DemoApp - Добавить проект в решение

dotnet new classlib -o DemoLib - Добавить библиотеку классов

dotnet add package Dapper - доавить нугет, необходимо находится в той папке проекта в которой доавляется пакет

dotnet add reference ..\DemoLib\DemoLib.csproj - Добавить в проект ссылку на другой проект
PS D:\Work\Programming\Repo\test\clidemo\DemoApp> dotnet add reference ..\DemoLib\DemoLib.csproj

dotnet restore - востановление проект
dotnte build - сборка проекта
dotnet clean - очистка решения от временных файлов

dotnet run - запуск решения

dotnet publish -p:PublishSingleFile=true -r win-x64 --self-contained false - публикация

-- Команды
-- Команды командной строки
-- Команды гит
-- Команды дот нет
-- Команды докер
-- Команды RabbitMQ
