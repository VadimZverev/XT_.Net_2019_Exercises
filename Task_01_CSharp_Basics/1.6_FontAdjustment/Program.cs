using System;

namespace _16_FontAdjustment
{
    class Program
    {
        static void Main()
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
                    {
                        // При расширении тут не будет работать эта манипуляция.
                        // Необходимо будет переделывать способ преобразования из номера
                        // списка в значение шрифта.
                        if (value == 3) value++;
                        break;
                    }

                    Console.WriteLine("Invalid input. Enter the number of the font." +
                        Environment.NewLine + "Enter:");
                }

                if (font == Font.None)
                    font = (Font)value;
                else if (font.HasFlag((Font)value))
                    font ^= (Font)value;
                else
                    font |= (Font)value;

                Console.WriteLine($"Your choice: {(Font)value}");
            }
        }

        private static void ShowOptions(Font font)
        {
            Console.WriteLine($"Inscription parameters: {font}");
            Console.WriteLine("Enter:");

            for (int i = 1, j = 1; i <= 4; i *= 2, j++)
            {
                Console.WriteLine($"\t{j}: {(Font)i}");
            }

            Console.Write($"{Environment.NewLine}Enter: ");
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
