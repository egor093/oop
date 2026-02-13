using System;
using System.Collections.Generic;

namespace Variant11_Events
{
    // Программное обеспечение (ПО)
    public class ProgramApp
    {
        public string Name { get; private set; }
        public VersionInfo Version { get; private set; }
        public bool IsWorking { get; private set; }
            
        public ProgramApp(string name, int major, int minor = 0)
        {
            Name = name;
            Version = new VersionInfo(major, minor);
            IsWorking = false;
        }

        // Метод, который будет подписан на событие Upgrade
        public void OnUpgrade(int increaseMajor)
        {
            Version.Major += increaseMajor;
            Console.WriteLine($"[{Name}] апдейт: новая версия {Version}");
        }

        public void OnUpgradeStr(string info)
        {
            Console.WriteLine($"[{Name}] UpgradeInfo: {info}");
        }

        // Метод, который будет подписан на событие Work
        public void OnWork(string message)
        {
            IsWorking = true;
            Console.WriteLine($"[{Name}] начало работы: {message}");
        }

        public void StopWork()
        {
            IsWorking = false;
        }

        public override string ToString()
        {
            return $"{Name} | Версия: {Version} | Работает: {IsWorking}";
        }
    }

    public class VersionInfo
    {
        public int Major { get; set; }
        public int Minor { get; set; }

        public VersionInfo(int major, int minor = 0)
        {
            Major = major;
            Minor = minor;
        }

        public override string ToString() => $"{Major}.{Minor}";
    }

    // Источник событий — пользователь
    public class User
    {
        // делегаты
        public delegate void UpgradeHandler(int addMajor);
        public delegate void WorkHandler(string message);

        // события (множественная подписка)
        public event UpgradeHandler Upgrade;
        public event WorkHandler Work;
            
        // Возбуждаем событие upgrade
        public void DoUpgrade(int addMajor)
        {
            Console.WriteLine($"\n>>> Пользователь вызывает событие UPGRADE (+{addMajor})");
            Upgrade?.Invoke(addMajor);
        }
        public void DoUpgradeWithInfo(string info)
        {
            Console.WriteLine($"\n>>> Пользователь вызывает событие UPGRADE_INFO (\"{info}\")");
        }

        public void DoWork(string msg)
        {
            Console.WriteLine($"\n>>> Пользователь вызывает событие WORK (\"{msg}\")");
            Work?.Invoke(msg);
        }
    }

    class Program
    {
        static void Main()
        {
            // Создаём ПО
            var chrome = new ProgramApp("Chrome", 1, 2);
            var vscode = new ProgramApp("VSCode", 1, 59);
            var telegram = new ProgramApp("Telegram", 7, 12);
            var notepad = new ProgramApp("NotepadX", 0, 9);

            var all = new List<ProgramApp> { chrome, vscode, telegram, notepad };

            // Источник событий
            var user = new User();

            // upgrade: Chrome и Telegram будут обновляться методом
            user.Upgrade += chrome.OnUpgrade;
            user.Upgrade += telegram.OnUpgrade;

            // Добавим дополнительный подписчик-лог (лямбда)
            user.Upgrade += (inc) => Console.WriteLine($"[Log] Все подписчики проапдейтены на +{inc}");

            // Work: VSCode и Notepad будут "работать"
            user.Work += vscode.OnWork;
            user.Work += notepad.OnWork;

            // Доп. лог при работе
            user.Work += msg => Console.WriteLine($"[Audit] Произошло событие Work: {msg}");

            // Telegram также может реагировать на Work, но по-другому — через лямбду
            user.Work += msg =>
            {
                if (msg.Contains("сообщение") || msg.Contains("чат"))
                {
                    telegram.OnWork("Обработка чата после события Work");
                }
                else
                {
                    Console.WriteLine("[Telegram] Игнорирует тип сообщения для работы.");
                }
            };

            // Выполнение событий:
            user.DoUpgrade(1);                         // апдейт всех подписчиков
            user.DoWork("Компиляция проекта");         // запускает VSCode и Notepad
            user.DoWork("Отправка сообщения в чат");   // запустит Telegram + остальные

            Console.WriteLine("\n--- Конечное состояние ПО ---");
            foreach (var p in all)
                Console.WriteLine(p);

            // Демонстрация отписки:
            Console.WriteLine("\n--- Отписываем Notepad от Work ---");
            user.Work -= notepad.OnWork;
            user.DoWork("Запуск фоновой задачи");
        }
    }
}
