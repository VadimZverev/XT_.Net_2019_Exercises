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
                Console.WriteLine("Начать заново? 1 - Да, 2 - Выход из программы");

            } while (IsContinue());
        }

        /// <summary>
        /// Ввод значения стороны прямоугольника.
        /// </summary>
        /// <param name="sideName">Имя стороны.</param>
        /// <param name="side">Размер стороны.</param>
        static void InputValue(char sideName, out int side)
        {
            while (true)
            {
                Console.Write($"Введите сторону {sideName}: ");
                bool isParse = int.TryParse(GetIntFromConsole(), out side);

                if (isParse && side > 0)
                    break;
                else
                    Console.WriteLine($"{Environment.NewLine}Отрицательные числа и 0 недопустимы.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Обрабатывает ввод данных, игнорируя ввод знаков препинания и букв.
        /// </summary>
        /// <returns>Возвращает строковое значение, введёное с консоли.</returns>
        static string GetIntFromConsole()
        {
            bool isFinished = false;

            StringBuilder sb = new StringBuilder();

            while (!isFinished)
            {
                ConsoleKeyInfo btnPress = Console.ReadKey(true);

                switch (btnPress.Key)
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
                            sb.Append(btnPress.KeyChar);
                            Console.Write(btnPress.KeyChar);
                        }
                        break;
                    default:
                        if (char.IsDigit(btnPress.KeyChar))
                        {
                            sb.Append(btnPress.KeyChar);
                            Console.Write(btnPress.KeyChar);
                        }
                        break;
                }
            }

            return sb.ToString();
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
