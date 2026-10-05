# Домашнее задание №1 — Анкета

.NET MAUI приложение с анкетой разработчика для Android и Windows.
Основные элементы управления, проверка имени и email, вывод всех ответов
и полная очистка формы. Логика находится в `MainPage.xaml.cs`.

## Запуск

Visual Studio с поддержкой .NET 10 (Visual Studio 2026), установленной нагрузкой
«.NET Multi-platform App UI development», Android SDK и Windows SDK.
Открыть `MauiHomework1.sln`, выбрать `Windows Machine` или Android-устройство и нажать F5.
Картинки локальные, подключение к интернету для работы анкеты не нужно.

Проверка из PowerShell: `./build.ps1` (Windows) или `./build.ps1 -Target Android`.
SDK, workload и NuGet-пакеты должны быть установлены; для их загрузки нужен интернет.
Автоматическая сборка обеих платформ настроена в GitHub Actions.

`CHECKLIST.md` содержит аудит требований и сценарии ручной проверки.
`EXPLAIN.md` — краткая шпаргалка по коду.
