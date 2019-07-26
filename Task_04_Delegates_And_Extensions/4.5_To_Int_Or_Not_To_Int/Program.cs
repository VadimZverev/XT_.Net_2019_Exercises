using System;

namespace _45_To_Int_Or_Not_To_Int
{
    class Program
    {
        static void Main()
        {
            string[] str = new string[]
            {
                "-24",
                "2-4",
                "24test5",
                "245",
                "987",
                "2 4 - 5",
                "24.5",
                "24,5",
            };

            foreach (string s in str)
            {
                Console.Write($"Is the string \"{ s}\" a positive integer?");
                ShowAnswer(s);
            }

        }

        static void ShowAnswer(string str)
        {
            if (str.IsPositiveInt())
            {
                Console.WriteLine(" Answer: Yes.");
            }
            else
            {
                Console.WriteLine(" Answer: No.");
            }
        }
    }

    public static class MyExtension
    {
        /// <summary>
        /// Checks if a string is a positive integer.
        /// </summary>
        /// <param name="str">check string</param>
        /// <returns>Returns true if the string is a positive integer; otherwise false.</returns>
        public static bool IsPositiveInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            if (str[0] == '-') return false;

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsDigit(str[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
