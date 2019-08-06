using System;
using System.Globalization;

namespace _51_Backup_System
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                ConsoleKeyInfo btnStop;
                object mode = ChooseMode();

                if (mode is Watcher watcher)
                {
                    watcher.FSWatcher.EnableRaisingEvents = true;

                    Console.WriteLine("Press Q to stop tracking changes...");

                    do
                    {
                        btnStop = Console.ReadKey(true);
                    } while (btnStop.Key != ConsoleKey.Q);

                    watcher.FSWatcher.EnableRaisingEvents = false;
                }
                else if (mode is Backup backup)
                {
                    backup.DateTimeRolback = InputDateTime();
                    backup.Run();
                    Console.WriteLine($"Files for the current date and time "
                                      + $"{backup.DateTimeRolback.ToString("yyyy.MM.dd HH:mm:ss")}"
                                      + $" have been restored.");
                }
                else
                    break;
            }
        }

        static object ChooseMode()
        {
            Console.WriteLine($"Choose Mode:{Environment.NewLine}"
                              + $"\t1 - Watcher;{Environment.NewLine}"
                              + $"\t2 - Backup;{Environment.NewLine}"
                              + $"\t3 - Exit.");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        return new Watcher();
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return new Backup();
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        return new object();
                }
            }
        }

        static DateTime InputDateTime()
        {
            Console.WriteLine("Enter the date and time to roll back the changes.");

            while (true)
            {
                Console.Write("Enter (Pattern: yyyy.MM.dd HH:mm:ss. Example: 2010.12.31 18:00:00): ");
                string str = Console.ReadLine();

                if (DateTime.TryParseExact(str, "yyyy.MM.dd HH:mm:ss", null, DateTimeStyles.None, out DateTime date))
                    return date;
                else
                    Console.WriteLine("Enter the correct date and time to roll back the changes.");
            }
        }
    }
}
