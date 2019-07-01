using System;

namespace _112_CharDoubler
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            string firstStr, secondStr;

            Console.Write("Введите первую строку: ");
            firstStr = Console.ReadLine();

            Console.Write("Введите вторую строку: ");
            secondStr = Console.ReadLine();

            string result = ResultString(firstStr, secondStr);
            Console.Write($"Результирующая строка: {result}");
        }

        static string RemoveDuplicateChars(string changeString)
        {
            string result = "";
            foreach (char ch in changeString)
                if (result.IndexOf(ch) == -1)
                    result += ch;
            return result;
        }

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
    }
}
