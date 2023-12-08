using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module16.practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Интерактивный ввод путей от пользователя
                Console.Write("Введите путь к отслеживаемой директории: ");
                string directoryPath = Console.ReadLine();

                Console.Write("Введите путь к лог-файлу: ");
                string logFilePath = Console.ReadLine();

                // Создание объекта FileSystemWatcher
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    watcher.Path = directoryPath;

                    // Настройка фильтров для отслеживания всех изменений
                    watcher.NotifyFilter = NotifyFilters.LastWrite
                                         | NotifyFilters.FileName
                                         | NotifyFilters.DirectoryName;

                    // Подписка на события
                    watcher.Created += OnChanged;
                    watcher.Deleted += OnChanged;
                    watcher.Renamed += OnRenamed;

                    // Включение отслеживания
                    watcher.EnableRaisingEvents = true;

                    Console.WriteLine($"Отслеживание запущено. Для завершения работы введите 'exit'.\n");

                    // Цикл для ожидания ввода пользователя 'exit'
                    while (Console.ReadLine() != "exit") ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Log($"[{DateTime.Now}] {e.ChangeType}: {e.FullPath}");
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Log($"[{DateTime.Now}] {e.ChangeType}: {e.OldFullPath} -> {e.FullPath}");
        }

        private static void Log(string message)
        {
            try
            {
                // Запись в лог-файл
                string logFilePath = "C:\\Users\\1\\Desktop\\homework\\log.txt";
                File.AppendAllText(logFilePath, $"{message}\n");
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
            }
        }
    }
}
