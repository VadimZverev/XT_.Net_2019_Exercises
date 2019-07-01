using System;

namespace _111_AverageStringLength
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Console.Write("Введите строку: ");
            string str = Console.ReadLine();

            int averageLength = AverageStringLength(str);

            Console.WriteLine($"Средняя длина слов равна {averageLength}");
        }

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
    }
}