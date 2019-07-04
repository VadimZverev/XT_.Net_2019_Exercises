using System;

namespace _16_FontAdjustment
{
    class Program
    {
        static void Main(string[] args)
        {
            int value;
            Font font = Font.None;

            while (true)
            {
                ShowOptions(font);

                while (true)
                {
                    bool isTrue = int.TryParse(Console.ReadLine(), out value);
                    if (isTrue && value > 0 && value < 4)
                        break;
                    Console.WriteLine("Некорректный ввод. Введите номер шрифта." +
                        Environment.NewLine +"Введите:");
                }

                switch (value)
                {
                    case 1:
                        if (font == Font.None)
                            font = Font.Bold;
                        else if (font.HasFlag(Font.Bold))
                            font ^= Font.Bold;
                        else
                            font |= Font.Bold;
                        break;
                    case 2:
                        if (font == Font.None)
                            font = Font.Italic;
                        else if (font.HasFlag(Font.Italic))
                            font ^= Font.Italic;
                        else
                            font |= Font.Italic;
                        break;
                    case 3:
                        if (font == Font.None)
                            font = Font.Underline;
                        else if (font.HasFlag(Font.Underline))
                            font ^= Font.Underline;
                        else
                            font |= Font.Underline;
                        break;
                    default:
                        font = Font.None;
                        break;
                }

                Console.WriteLine(font);
            }
        }

        private static void ShowOptions(Font font)
        {
            Console.WriteLine($"Параметры надписи: {font}");
            Console.WriteLine("Введите:");

            for (int i = 1, j = 1; i <= 4; i *= 2, j++)
            {
                Console.WriteLine($"\t{j}: {(Font)i}");
            }
        }
    }

    [Flags]
    enum Font
    {
        None = 0,
        Bold = 1,
        Italic = 2,
        Underline = 4
    }
}
