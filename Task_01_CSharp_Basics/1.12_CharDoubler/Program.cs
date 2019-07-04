using System;

namespace _112_CharDoubler
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            do
            {
                Console.Write("Введите первую строку: ");
                string firstStr = Console.ReadLine();

                Console.Write("Введите вторую строку: ");
                string secondStr = Console.ReadLine();

                string result = ResultString(firstStr, secondStr);
                Console.Write($"Результирующая строка: {result}");
                Console.WriteLine("Начать заново? 1 - Да, 2 - Выход из программы");
            } while (IsContinue());
        }

        /// <summary>
        /// Удаляет дублирующие символы.
        /// </summary>
        /// <param name="changeString">редактируемая строка.</param>
        /// <returns>Возвращает строку.</returns>
        static string RemoveDuplicateChars(string changeString)
        {
            string result = "";
            foreach (char ch in changeString)
                if (result.IndexOf(ch) == -1)
                    result += ch;
            return result;
        }

        /// <summary>
        /// Дублирует знаки, присутствующие в проверяемой строке.
        /// </summary>
        /// <param name="changeString">изменяемая строка.</param>
        /// <param name="checkString">проверяющая строка.</param>
        /// <returns></returns>
        static string ResultString(string changeString, string checkString)
        {
            char[] checkChars = RemoveDuplicateChars(checkString).ToCharArray();

            foreach (char ch in checkChars)
            {
                if (changeString.Contains(ch))
                    changeString = changeString.Replace($"{ch}", $"{ch}{ch}");
            }

            return changeString;
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        /// <returns>Возвращает bool-значение.</returns>
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
