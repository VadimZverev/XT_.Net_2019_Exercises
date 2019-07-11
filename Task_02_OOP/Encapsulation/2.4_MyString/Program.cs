using System;
using System.Text;

namespace _24_MyString
{
    class Program
    {
        static void Main()
        {
            int opts;       // выбор опции
            string str;     // строка для временного хранения ответа
            MyString my;    // выбор строки на проверку

            Console.InputEncoding = Encoding.Unicode;

            do
            {
                Console.Write("Введите первую строку: ");
                MyString firstStr = new MyString(Console.ReadLine());

                Console.Write("Введите вторую строку: ");
                MyString secondStr = new MyString(Console.ReadLine());

                while (true)
                {
                    Console.WriteLine("Выберете операцию над строками:");
                    opts = ChooseOptions();

                    switch (opts)
                    {
                        case 1:
                            str = firstStr == secondStr ? "да" : "нет";
                            Console.WriteLine($"1 строка равна 2 строке?" +
                                $" Ответ: {str}");
                            continue;
                        case 2:
                            str = firstStr != secondStr ? "да" : "нет";
                            Console.WriteLine($"1 строка не равна 2 строке? Ответ: {str}");
                            continue;
                        case 3:
                            str = firstStr > secondStr ? "да" : "нет";
                            Console.WriteLine($"1 строка больше 2 строки? Ответ: {str}");
                            continue;
                        case 4:
                            str = firstStr < secondStr ? "да" : "нет";
                            Console.WriteLine($"1 строка меньше 2 строки? Ответ: {str}");
                            continue;
                        case 5:
                            Console.WriteLine($"Результат конкатенации: {firstStr + secondStr}");
                            continue;
                        case 6:
                            my = ChooseString(firstStr, secondStr);
                            ShowIndex(my);
                            continue;
                        case 7:
                            my = ChooseString(firstStr, secondStr);
                            ShowCharArray(my);
                            continue;
                        case 8:
                            ShowMyStringFromCharArray();
                            continue;
                        case 9:
                            Console.Clear();
                            break;
                        case 10:
                            return;
                    }

                    break;
                }

            } while (true);
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

        static void ShowMyStringFromCharArray()
        {   
            char[] temp = { 'A', 'B', 'd', ' ', '\\', '/'};

            Console.WriteLine("Массив символов до преобразования: ");
            foreach (char ch in temp)
            {
                Console.Write($"{ch}, ");
            }
            Console.WriteLine("\b\b.");

            Console.Write($"После преобразования в строку: {MyString.ToString(temp)}" 
                + Environment.NewLine);
        }

        static int ChooseBegin()
        {
            while (true)
            {
                Console.WriteLine("Выберете откуда начать поиск: 1 - с начала 2 - с конца");
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value == 1 || value == 2) return value;
                }

                Console.WriteLine("Ввод некорректен, выберете начало поиска.");
            }
        }

        static int ChooseOptions()
        {
            while (true)
            {
                Console.WriteLine("Параметры:" + Environment.NewLine
                                  + "\t1 - Сравнение: равенство" + Environment.NewLine
                                  + "\t2 - Сравнение: неравенство" + Environment.NewLine
                                  + "\t3 - Сравнение, больше ли первая строка" + Environment.NewLine
                                  + "\t4 - Сравнение, меньше ли первая строка" + Environment.NewLine
                                  + "\t5 - Конкатенация" + Environment.NewLine
                                  + "\t6 - Поиск символа" + Environment.NewLine
                                  + "\t7 - Конвертация в массив символов" + Environment.NewLine
                                  + "\t8 - Демонстрация конвертации из массива символов в строку" + Environment.NewLine
                                  + "\t9 - Начать заново" + Environment.NewLine
                                  + "\t10 - Выход из программы" + Environment.NewLine);

                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0 && value < 11) return value;
                }

                Console.WriteLine("Ввод некорректен, выберете параметр.");
            }
        }

        static MyString ChooseString(MyString str1, MyString str2)
        {
            while (true)
            {
                Console.WriteLine("Выберете строку: 1 - первая 2 - вторая");
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value == 1) return str1;
                    else if (value == 2) return str2;
                }

                Console.WriteLine("Ввод некорректен, выберете строку.");
            }
        }

        static void ShowIndex(MyString str)
        {
            char ch;
            int temp = ChooseBegin();

            Console.Write("Введите символ: ");
            while (!char.TryParse(Console.ReadLine(), out ch))
            {
                Console.Write("Ввод некорректен, повторите ввод: ");
            }

            if (temp == 1)
            {
                temp = str.FindFirst(ch);
                if (temp == -1)
                    Console.WriteLine("Такого символа нет в строке.");
                else
                    Console.WriteLine($"Первое вхождение символа с начала начинается с {temp} индекса.");
            }
            else
            {
                temp = str.FindLast(ch);
                if (temp == -1)
                    Console.WriteLine("Такого символа нет в строке.");
                else
                    Console.WriteLine($"Первое вхождение символа c конца начинается с {str.FindLast(ch)} индекса.");
            }
        }
    }
}
