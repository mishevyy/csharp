/*------------Base first в Net Core------------------------*/
// Необходимые пакеты в проекте где будет храниться БД
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
// Необходимые пакеты в проекте клиента для подключения БД
Microsoft.EntityFrameworkCore.Design

// Команды для подключения муществующей БД

// Загрузка всей бд
Scaffold-DbContext "Server=servdb1;Database=Essen_chicago;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer

// Настройка контекста и моделей
Scaffold-DbContext ... -ContextDir Data -OutputDir Models

// Загрузка отдельных таблиц
Scaffold-DbContext ... -Tables Artist, Album

// Загрузка отдельных таблиц (по схеме)
Scaffold-DbContext ... -Schemas dbo

// Обновление таблиз из базы
Scaffold-DbContext ... -Force

Scaffold-DbContext "Server=ServName;Database=BaseName;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Models -Tables tablesName

// Расширения Visual studio для реверм инжинеринга
// https://github.com/ErikEJ/EFCorePowerTools/