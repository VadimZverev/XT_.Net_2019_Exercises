using System;
using System.Text;

namespace _11_Rectangle
{
    class Program
    {
        static void Main()
        {
            do
            {
                InputValue('A', out int a);
                InputValue('B', out int b);

                Console.WriteLine($"Площадь прямоугольника равна: {a * b}");
                Console.WriteLine("Повторить ввод? 1 - Да, 2 - Выход из программы");

            } while (isContinue());
        }

        static void InputValue(char sideName, out int side)
        {
            while (true)
            {
                Console.Write($"Введите сторону {sideName}: ");
                bool isParse = int.TryParse(GetIntFromConsole(), out side);

                if (isParse && side > 0)
                    break;
                else
                    Console.WriteLine("\nОтрицательные числа и 0 недопустимы.");
            }
            Console.WriteLine();
        }

        static string GetIntFromConsole()
        {
            bool isFinished = false;

            StringBuilder sb = new StringBuilder();

            while (!isFinished)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        isFinished = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (sb.Length != 0)
                        {
                            Console.Write("\b \b");
                            sb.Remove(sb.Length - 1, 1);
                        }
                        break;
                    case ConsoleKey.OemMinus:
                        if (sb.Length == 0)
                        {
                            sb.Append(key.KeyChar);
                            Console.Write(key.KeyChar);
                        }
                        break;
                    default:
                        if (char.IsDigit(key.KeyChar))
                        {
                            sb.Append(key.KeyChar);
                            Console.Write(key.KeyChar);
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        static bool isContinue()
        {
            while (true)
            {
                Console.Write("Ваш ввод: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int value);

                if (isParse && value == 1) return true;
                else if (isParse && value == 2) return false;
                else Console.WriteLine("Некорректный ввод, повторите ввод.");
            }
        }
    }
}
