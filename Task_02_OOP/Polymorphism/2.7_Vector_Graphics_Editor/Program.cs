using System;

namespace _27_Vector_Graphics_Editor
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

                Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");
            } while (IsContinue());
        }

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
            }
        }

        static int ChooseFigure()
        {
            while (true)
            {
                Console.WriteLine("Параметры:" + Environment.NewLine
                                  + "\t1 - Линия" + Environment.NewLine
                                  + "\t2 - Окружность" + Environment.NewLine
                                  + "\t3 - Прямоугольник" + Environment.NewLine
                                  + "\t4 - Круг" + Environment.NewLine
                                  + "\t5 - Кольцо" + Environment.NewLine);

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
        /// <param name="isNotNegative">запрещен ли ввод отрицательных значений. По умолчанию false</param>
        static int InputValue(string line, bool isNotNegative = false)
        {
            int value;

            while (true)
            {
                Console.Write(line);

                try
                {
                    value = int.Parse(Console.ReadLine());

                    if (isNotNegative && value <= 0)
                    {
                        Console.WriteLine("Значение не может быть меньше либо равен 0.");
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вводимое должно быть натуральное целое число.");
                    continue;
                }

                return value;
            }
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
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

            figure.Draw();
        }

        static void ShowLine()
        {
            Console.WriteLine("Ввод начальной точки.");
            InputCoordinate(out int x, out int y);
            Point start = new Point(x, y);

            Console.WriteLine("Ввод конечной точки.");
            InputCoordinate(out x, out y);
            Point end = new Point(x, y);

            IDrawable figure = new Line(start, end);
            figure.Draw();
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

            figure.Draw();
        }

        static void ShowRing()
        {
            int inR, outR;
            IDrawable figure;

            int opts = ChooseCoordinates();

            if (opts == 1)
            {
                while (true)
                {
                    try
                    {
                        inR = InputValue("Введите внутренний радиус: ", true);
                        outR = InputValue("Введите внешний радиус: ", true);
                        figure = new Ring(inR, outR);
                    }
                    catch (ArgumentException exc)
                    {
                        Console.WriteLine(exc.Message);
                        continue;
                    }

                    break;
                }
            }
            else
            {
                InputCoordinate(out int x, out int y);

                while (true)
                {
                    try
                    {
                        inR = InputValue("Введите внутренний радиус: ", true);
                        outR = InputValue("Введите внешний радиус: ", true);
                        figure = new Ring(inR, outR, x, y);
                    }
                    catch (ArgumentException exc)
                    {
                        Console.WriteLine(exc.Message);
                        continue;
                    }

                    break;
                }
            }

            figure.Draw();
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

            figure.Draw();
        }
    }
}