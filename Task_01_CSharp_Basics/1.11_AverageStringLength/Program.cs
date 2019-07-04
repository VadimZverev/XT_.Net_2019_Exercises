using System;

namespace _111_AverageStringLength
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            do
            {
                Console.Write("Введите строку: ");
                string str = Console.ReadLine();

                int averageLength = AverageStringLength(str);

                Console.WriteLine($"Средняя длина слов равна {averageLength}");
                Console.WriteLine("Повторить ввод? 1 - Да, 2 - Выход из программы");
            } while (IsContinue());
        }

        /// <summary>
        /// Вычисляет среднее значение слов в предложении.
        /// </summary>
        /// <param name="str">проверяемая строка.</param>
        /// <returns></returns>
        static int AverageStringLength(string str)
        {
            int sum = 0;
            char[] separators = { ' ', ',', '.', '!', '?', ':', ';', '"' };

            string[] words = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                sum += words[i].Length;
            }
            sum /= words.Length;

            return sum;
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