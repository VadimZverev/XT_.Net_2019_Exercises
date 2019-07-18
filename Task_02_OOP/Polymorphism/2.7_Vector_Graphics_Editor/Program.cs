using System;
using Vector_Graphics_Editor.Classes;

namespace Vector_Graphics_Editor
{
    class Program
    {
        static void Main()
        {
            do
            {
                Console.WriteLine("Выберете фигуру:");
                int choice = ChooseFigure();

                switch (choice)
                {
                    case 1: // Line
                        ShowLine();
                        break;
                    case 2: // Circle
                        ShowCircle();
                        break;
                    case 3: // Rectangle
                        ShowRectangle();
                        break;
                    case 4: // Round
                        ShowRound();
                        break;
                    case 5: // Ring
                        ShowRing();
                        break;
                }

            } while (IsContinue());
        }

        /// <summary>
        /// Возвращает выбранный вариант ввода координат.
        /// </summary>
        static int ChooseCoordinates()
        {
            while (true)
            {
                Console.WriteLine("Параметры ввода координат: 1 - По умолчанию 2 - Вручную");
                Console.Write("Ваш ввод: ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value == 1 || value == 2) return value;
                }

                Console.WriteLine("Ввод некорректен, выберете параметр.");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Возвращает, какую фигуру необходимо отобразить.
        /// </summary>
        static int ChooseFigure()
        {
            Console.WriteLine("Параметры:" + Environment.NewLine
                                  + "\t1 - Линия" + Environment.NewLine
                                  + "\t2 - Окружность" + Environment.NewLine
                                  + "\t3 - Прямоугольник" + Environment.NewLine
                                  + "\t4 - Круг" + Environment.NewLine
                                  + "\t5 - Кольцо" + Environment.NewLine);

            while (true)
            {
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0 && value < 6) return value;
                }

                Console.WriteLine("Ввод некорректен, выберете параметр.");
            }
        }

        static void InputCoordinate(out int x, out int y)
        {
            x = InputValue("Введите координату Х: ");
            y = InputValue("Введите координату Y: ");
        }

        /// <summary>
        /// Возвращает число с проверкой на корректность данных.
        /// </summary>
        /// <param name="line">Информирующая строка.</param>
        /// <param name="isRadius"><c>true</c>, если вводимое число является радиусом.
        /// По умолчанию <c>false</c></param>
        static int InputValue(string line, bool isRadius = false)
        {
            while (true)
            {
                Console.Write(line);

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (!isRadius)
                    {
                        return value;
                    }
                    else
                    {
                        if (value > 0) return value;

                        Console.WriteLine("Радиус не может быть меньше, либо равен 0.");
                        Console.WriteLine();
                        continue;
                    }
                }

                Console.WriteLine("Вводимое должно быть натуральное целое число.");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Ввод внутреннего и внешнего радиуса кольца.
        /// </summary>
        /// <param name="inR">внутренний радиус</param>
        /// <param name="outR">внешний радиус</param>
        static void InputRingRadiuses(out int inR, out int outR)
        {
            Console.WriteLine();

            while (true)
            {
                inR = InputValue("Введите внутренний радиус: ", true);

                while (true)
                {
                    outR = InputValue("Введите внешний радиус: ", true);

                    if (!(inR >= outR)) break;

                    Console.WriteLine("Внутренняя окружность не может быть "
                                + "больше, либо равно внешней окружности");
                    Console.WriteLine();
                }
                break;
            }
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        static bool IsContinue()
        {
            Console.WriteLine(Environment.NewLine
                              + "Начать заново? 1 - Да, 2 - Завершить программу.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        return true;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return false;
                }
            }
        }

        static void ShowCircle()
        {
            IDrawable figure;

            int opts = ChooseCoordinates();
            if (opts == 1)
            {
                figure = new Circle()
                {
                    Radius = InputValue("Введите радиус: ", true)
                };
            }
            else
            {
                InputCoordinate(out int x, out int y);
                figure = new Circle(x, y)
                {
                    Radius = InputValue("Введите радиус: ", true)
                };
            }

            Console.WriteLine();

            figure.Draw();
        }

        static void ShowLine()
        {
            Console.WriteLine(Environment.NewLine + "Ввод начальной точки.");
            InputCoordinate(out int x, out int y);
            Point start = new Point(x, y);

            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Ввод конечной точки.");
                InputCoordinate(out x, out y);
                Point end = new Point(x, y);

                if (!(start.X == end.X && start.Y == end.Y))
                {
                    IDrawable figure = new Line(start, end);

                    Console.WriteLine();

                    figure.Draw();
                    return;
                }

                Console.WriteLine("Две точки не могут находиться в одних и тех же"
                                  + " координатах, повторите ввод конечной точки."
                                  + Environment.NewLine);
            }

        }

        static void ShowRectangle()
        {
            IDrawable figure;
            int height, width;

            int opts = ChooseCoordinates();
            if (opts == 1)
            {
                height = InputValue("Введите высоту: ");
                width = InputValue("Введите ширину: ");

                figure = new Rectangle(height, width);
            }
            else
            {
                InputCoordinate(out int x, out int y);

                height = InputValue("Введите высоту: ");
                width = InputValue("Введите ширину: ");

                figure = new Rectangle(height, width, x, y);
            }

            Console.WriteLine();

            figure.Draw();
        }

        static void ShowRing()
        {
            int inR, outR;
            IDrawable figure;

            int opts = ChooseCoordinates();

            switch (opts)
            {
                case 1:
                    InputRingRadiuses(out inR, out outR);
                    figure = new Ring(inR, outR);
                    Console.WriteLine();
                    figure.Draw();
                    break;
                case 2:
                    InputCoordinate(out int x, out int y);
                    InputRingRadiuses(out inR, out outR);
                    figure = new Ring(inR, outR, x, y);
                    Console.WriteLine();
                    figure.Draw();
                    break;
            }
        }

        static void ShowRound()
        {
            IDrawable figure;

            int opts = ChooseCoordinates();
            if (opts == 1)
            {
                figure = new Round()
                {
                    Radius = InputValue("Введите радиус: ", true)
                };
            }
            else
            {
                InputCoordinate(out int x, out int y);

                figure = new Round(x, y)
                {
                    Radius = InputValue("Введите радиус: ", true)
                };
            }

            Console.WriteLine();

            figure.Draw();
        }
    }
}