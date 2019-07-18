using System;
using System.Text;

namespace _24_MyString
{
    class Program
    {
        static void Main()
        {
            ConsoleKeyInfo opts;    // выбор опции
            string str;             // строка для временного хранения ответа
            MyString myString;      // выбор строки на проверку

            Console.InputEncoding = Encoding.Unicode;

            do
            {
                MyString firstStr = InputString("Введите 1 строку: ");
                MyString secondStr = InputString("Введите 2 строку: ");

                Console.WriteLine();

                ShowOptions();

                while (true)
                {
                    opts = Console.ReadKey(true);

                    switch (opts.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            {
                                Console.WriteLine(opts.KeyChar);

                                str = firstStr == secondStr ? "да" : "нет";
                                Console.WriteLine($"1 строка равна 2 строке? Ответ: {str}"
                                                  + Environment.NewLine);

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            {
                                Console.WriteLine(opts.KeyChar);

                                str = firstStr != secondStr ? "да" : "нет";
                                Console.WriteLine($"1 строка не равна 2 строке? Ответ: {str}"
                                                  + Environment.NewLine);

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            {
                                Console.WriteLine(opts.KeyChar);

                                str = firstStr > secondStr ? "да" : "нет";
                                Console.WriteLine($"1 строка больше 2 строки? Ответ: {str}"
                                                  + Environment.NewLine);

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            {
                                Console.WriteLine(opts.KeyChar);

                                str = firstStr < secondStr ? "да" : "нет";
                                Console.WriteLine($"1 строка меньше 2 строки? Ответ: {str}"
                                                  + Environment.NewLine);

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:
                            {
                                Console.WriteLine(opts.KeyChar);

                                Console.WriteLine($"Результат конкатенации: {firstStr + secondStr}"
                                                  + Environment.NewLine);
                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6:
                            {
                                Console.WriteLine(opts.KeyChar);

                                myString = ChooseString(firstStr, secondStr);
                                ShowIndex(myString);

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D7:
                        case ConsoleKey.NumPad7:
                            {
                                Console.WriteLine(opts.KeyChar);

                                myString = ChooseString(firstStr, secondStr);
                                ShowCharArray(myString);

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D8:
                        case ConsoleKey.NumPad8:
                            {
                                Console.WriteLine(opts.KeyChar);

                                ShowMyStringFromCharArray();

                                Console.Write("Ваш выбор: ");
                                continue;
                            }
                        case ConsoleKey.D9:
                        case ConsoleKey.NumPad9:
                            Console.Clear();
                            break;
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            Console.WriteLine(opts.KeyChar);
                            return;
                        case ConsoleKey.I:
                            Console.WriteLine(opts.KeyChar);
                            ShowOptions();
                            continue;
                        default:
                            continue;
                    }

                    break;
                }

            } while (true);
        }

        static int ChooseBegin()
        {
            Console.WriteLine();
            Console.WriteLine("Выберете откуда начать поиск: 1 - с начала 2 - с конца");
            Console.Write("Ваш выбор: ");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("поиск с начала." + Environment.NewLine);
                        return 1;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("поиск с конца." + Environment.NewLine);
                        return 2;
                }
            }
        }

        static MyString ChooseString(MyString str1, MyString str2)
        {
            while (true)
            {
                Console.WriteLine("Выберете строку: 1 - первая 2 - вторая");
                Console.Write("Ваш выбор: ");

                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.WriteLine("первая строка.");
                            return str1;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("вторая строка.");
                            return str2;
                    }
                }
            }
        }

        static MyString InputString(string line)
        {
            string temp;
            while (true)
            {
                Console.Write(line);

                try
                {
                    temp = Console.ReadLine();
                    if (temp == string.Empty)
                        throw new Exception("Пустая строка, введите заново.");

                    return new MyString(temp);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        static void ShowCharArray(MyString str)
        {
            char[] chars = str.ToCharArray();

            Console.Write("Массив символов: ");

            foreach (char item in chars)
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine("\b\b.");
        }

        static void ShowIndex(MyString str)
        {
            char symbol;
            int temp = ChooseBegin();

            Console.Write("Введите символ: ");
            while (!char.TryParse(Console.ReadLine(), out symbol))
            {
                Console.Write("Ввод некорректен, повторите ввод: ");
            }

            if (temp == 1)
            {
                temp = str.FindFirst(symbol);
                if (temp == -1)
                    Console.WriteLine("Такого символа нет в строке.");
                else
                    Console.WriteLine($"Первое вхождение символа с начала начинается с {temp} индекса.");
            }
            else
            {
                temp = str.FindLast(symbol);
                if (temp == -1)
                    Console.WriteLine("Такого символа нет в строке.");
                else
                    Console.WriteLine($"Первое вхождение символа c конца начинается с {str.FindLast(symbol)} индекса.");
            }
        }

        static void ShowOptions()
        {
            Console.WriteLine("Выберете операцию над строками:" + Environment.NewLine
                            + "\t1 - Сравнение: равенство" + Environment.NewLine
                            + "\t2 - Сравнение: неравенство" + Environment.NewLine
                            + "\t3 - Сравнение, больше ли первая строка" + Environment.NewLine
                            + "\t4 - Сравнение, меньше ли первая строка" + Environment.NewLine
                            + "\t5 - Конкатенация" + Environment.NewLine
                            + "\t6 - Поиск символа" + Environment.NewLine
                            + "\t7 - Конвертация в массив символов" + Environment.NewLine
                            + "\t8 - Демонстрация конвертации из массива символов в строку" + Environment.NewLine
                            + "\t9 - Начать заново" + Environment.NewLine
                            + "\t0 - Выход из программы" + Environment.NewLine
                            + "\tI - Показать параметры." + Environment.NewLine);

            Console.Write("Ваш выбор: ");
        }

        static void ShowMyStringFromCharArray()
        {
            char[] temp = { 'A', 'B', 'd', ' ', '\\', '/' };

            Console.WriteLine("Массив символов до преобразования: ");
            foreach (char ch in temp)
            {
                Console.Write($"{ch}, ");
            }
            Console.WriteLine("\b\b.");

            Console.Write($"После преобразования в строку: {MyString.ToMyString(temp)}"
                + Environment.NewLine);
        }
    }
}