Тестовое задание: Minimal API + EF Core + PostgreSQL + интеграционные тесты
Цель

Реализовать Minimal API (.NET 9) с двумя эндпоинтами для сущности Product, используя EF Core и PostgreSQL.
Приложение должно сопровождаться интеграционными тестами.

Технологии
.NET 9
ASP.NET Core Minimal API
EF Core 9
PostgreSQL 16+
Тесты: xUnit, Microsoft.AspNetCore.Mvc.Testing, Testcontainers for .NET

Сущность и модель БД
Product
Id uuid (PK, генерируется на сервере)
Name varchar(100) (required, уникальный)
Price numeric(18,2) (> 0)
CreatedAt timestamptz (UTC, выставляется на сервере)

Требования к схеме
Все ограничения и типы — через Fluent API + миграции.
Индекс на поле Name.

Эндпоинты
POST /api/products
Тело запроса: { "name": string, "price": decimal }

Валидация:
name не пустой, длина 2–100
price > 0

Поведение: создать продукт, Id и CreatedAt назначаются на сервере

Ответы:
200 Created с Location: /products/{id} и телом созданного продукта
400 Bad Request — ошибки валидации (указать поле/причину)

GET /products/{id:guid}
Возвращает продукт по Id

Ответы:
200 OK — найден
400 Not Found — не найден

Нефункциональные требования
Отдельные DTO на вход/выход (не светить доменную модель напрямую)

Конфигурация строки подключения в appsettings.json
Асинхронный код (EF Core async)