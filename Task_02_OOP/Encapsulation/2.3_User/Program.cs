using System;
using System.Globalization;
using System.Text;

namespace _23_User
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;

            do
            {
                User user = new User();

                Console.Write("Введите Фамилию: ");
                user.LastName = GetStringFromConsole();
                Console.WriteLine();

                Console.Write("Введите Имя: ");
                user.FirstName = GetStringFromConsole();
                Console.WriteLine();

                Console.Write("Введите Отчество: ");
                user.MiddleName = GetStringFromConsole();
                Console.WriteLine();

                user.DateOfBirth = GetDate();

                Console.WriteLine($"User: {user}");
                Console.WriteLine($"Дата рождения: {user.DateOfBirth: d MMMM yyyy}");
                Console.WriteLine($"Возраст: {user.Age}");

                Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");
            } while (IsContinue());
        }

        /// <summary>
        /// Возвращает дату, введенную с консоли.
        /// </summary>
        static DateTime GetDate()
        {
            while (true)
            {
                Console.Write("Введите Дату Рождения (Пример: 1 января(ь) 1970): ");

                string str = Console.ReadLine(); // Не знаю, избыточно ли это, либо наоборот, лучше для чтения.

                if (DateTime.TryParseExact(str, "d MMMM yyyy", null, DateTimeStyles.None, out DateTime date))
                    return date;
                else
                    Console.WriteLine("Введите корректно дату рождения.");
            }
        }

        /// <summary>
        /// Обрабатывает ввод данных, игнорируя ввод знаков препинания и цифр.
        /// </summary>
        static string GetStringFromConsole()
        {
            bool isFinished = false;

            StringBuilder sb = new StringBuilder();

            while (!isFinished)
            {
                ConsoleKeyInfo btnPress = Console.ReadKey(true);

                switch (btnPress.Key)
                {
                    case ConsoleKey.Enter when sb.Length != 0:
                        isFinished = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (sb.Length != 0)
                        {
                            Console.Write("\b \b");
                            sb.Remove(sb.Length - 1, 1);
                        }
                        break;
                    default:
                        if (char.IsLetter(btnPress.KeyChar))
                        {
                            if (sb.Length == 0)
                            {
                                sb.Append(char.ToUpperInvariant(btnPress.KeyChar));
                                Console.Write(char.ToUpperInvariant(btnPress.KeyChar));
                            }
                            else
                            {
                                sb.Append(btnPress.KeyChar);
                                Console.Write(btnPress.KeyChar);
                            }
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        static bool IsContinue()
        {
            while (true)
            {
                Console.Write("Ваш ввод: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int value);

                if (isParse && value == 1)
                {
                    Console.Clear();
                    return true;
                }
                else if (isParse && value == 2) return false;
                else Console.WriteLine("Некорректный ввод, повторите ввод.");
            }
        }
    }
}
