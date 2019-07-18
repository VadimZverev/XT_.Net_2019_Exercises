using _28_Game.Bonuses;
using _28_Game.Obstacles;
using _28_Game.Units;
using System;
using System.Collections.Generic;

namespace _28_Game.General
{
    sealed class Game
    {
        private GameField gameField;        // игровое поле.
        private List<Bonus> bonus;          // бонусы.
        private List<Unit> enemy;           // монстры.
        private List<GameObject> obtacles;  // препятствия.
        private Player player;              // игрок.

        public Game()
        {
            gameField = new GameField();
            player = new Player();
            bonus = AddBonus(2, 2, 1);
            enemy = AddEnemy(2, 1);
            obtacles = AddObtacles(3, 2);
        }

        /// <summary>
        /// Стартует начало игры.
        /// </summary>
        public void StartGame()
        {
            while (true)
            {
                ShowMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Play();
                        return;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Options();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        return;
                }
            }
        }

        /// <summary>
        /// Добавление бонусов
        /// </summary>
        /// <param name="numApple">количество яблок</param>
        /// <param name="numCherry">количество вишен</param>
        /// <param name="numSword">количество мечей</param>
        /// <returns>Возвращает коллекцию бонусов</returns>
        private List<Bonus> AddBonus(int numApple, int numCherry, int numSword)
        {
            Random r = new Random();
            bonus = new List<Bonus>();

            for (int i = 0; i < numApple; i++)
            {
                bonus.Add(new Apple(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            for (int i = 0; i < numCherry; i++)
            {
                bonus.Add(new Cherry(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            for (int i = 0; i < numSword; i++)
            {
                bonus.Add(new Sword(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            return bonus;

        }

        /// <summary>
        /// Добавление препятствий
        /// </summary>
        /// <param name="numStone">количество камней</param>
        /// <param name="numTree">количество деревьев</param>
        /// <returns>Возвращает коллекцию препятствий</returns>
        private List<GameObject> AddObtacles(int numStone, int numTree)
        {
            Random r = new Random();
            obtacles = new List<GameObject>();

            for (int i = 0; i < numStone; i++)
            {
                obtacles.Add(new Stone(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            for (int i = 0; i < numTree; i++)
            {
                obtacles.Add(new Tree(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            return obtacles;
        }

        /// <summary>
        /// Добавление врагов.
        /// </summary>
        /// <param name="numberWolf">количество волков</param>
        /// <param name="numberBear">количество медведей</param>
        /// <returns>Возвращает коллекцию врагов</returns>
        private List<Unit> AddEnemy(int numberWolf, int numberBear)
        {
            Random r = new Random();
            List<Unit> enemy = new List<Unit>();

            for (int i = 0; i < numberWolf; i++)
            {
                enemy.Add(new Wolf(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            for (int i = 0; i < numberBear; i++)
            {
                enemy.Add(new Bear(r.Next(gameField.Width), r.Next(gameField.Height)));
            }

            return enemy;
        }

        /// <summary>
        /// Отображение списка меню
        /// </summary>
        private void Options()
        {
            string str = string.Empty;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Параметры:" + Environment.NewLine
                      + "\t 1 - Уровень сложности" + Environment.NewLine
                      + "\t 2 - Размер карты" + Environment.NewLine
                      + "\t 3 - Назад");

                Console.WriteLine(str);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        SelectDifficultyLevel(out str);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        SelectFieldSize(out str);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        return;
                }
            }
        }

        /// <summary>
        /// Запуск игры с установленными настройками.
        /// </summary>
        public void Play()
        {
            string text = "It has begun!!! To Be Continue...";

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            int padding = width / 2 + text.Length / 2;

            Console.Clear();
            Console.SetCursorPosition(0, height / 2 - 1);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("{0," + padding + "}", text);

            Console.ResetColor();

            Console.ReadKey();
        }

        /// <summary>
        /// Установка уровня сложности.
        /// </summary>
        /// <param name="str">какой будет выбран уровень сложности</param>
        private void SelectDifficultyLevel(out string str)
        {
            Console.WriteLine("Уровень сложности:" + Environment.NewLine
                  + "\t 1 - Easy" + Environment.NewLine
                  + "\t 2 - Normal" + Environment.NewLine
                  + "\t 3 - Medium" + Environment.NewLine
                  + "\t 4 - Hard" + Environment.NewLine
                  + "\t 5 - Cancel");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        str = "Выбран уровень: Easy";
                        enemy = AddEnemy(2, 0);
                        return;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        str = "Выбран уровень: Normal";
                        enemy = AddEnemy(2, 1);
                        return;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        str = "Выбран уровень: Medium";
                        enemy = AddEnemy(3, 2);
                        return;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        str = "Выбран уровень: Hard";
                        enemy = AddEnemy(3, 3);
                        return;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        str = string.Empty;
                        return;
                }
            }
        }

        /// <summary>
        /// Установка размера игрового поля.
        /// </summary>
        /// <param name="str">какой размер будет выбран</param>
        private void SelectFieldSize(out string str)
        {
            Console.WriteLine("Размер поля:" + Environment.NewLine
                              + "\t 1 - 120 Х 30" + Environment.NewLine
                              + "\t 2 - 240 Х 60" + Environment.NewLine
                              + "\t 3 - 480 Х 120" + Environment.NewLine
                              + "\t 4 - 800 Х 600" + Environment.NewLine
                              + "\t 5 - Cancel");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        gameField = new GameField(120, 30);
                        str = "Выбрано поле 120 Х 30";
                        return;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        gameField = new GameField(240, 60);
                        str = "Выбрано поле 240 Х 60";
                        return;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        gameField = new GameField(480, 120);
                        str = "Выбрано поле 480 Х 120";
                        return;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        gameField = new GameField(800, 600);
                        str = "Выбрано поле 800 Х 600";
                        return;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        str = string.Empty;
                        return;
                }
            }
        }

        /// <summary>
        /// Показать стартовое меню
        /// </summary>
        private void ShowMenu()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Console.Clear();

            string text = "1 - Start Game";
            int padding = width / 2 + text.Length / 2;
            Console.SetCursorPosition(0, height / 2 - 1);
            Console.WriteLine("{0," + padding + "}", text);

            text = "2 - Options";
            padding = width / 2 + text.Length / 2;
            Console.SetCursorPosition(0, height / 2);
            Console.WriteLine("{0," + padding + "}", text);

            text = "3 - Exit";
            padding = width / 2 + text.Length / 2;
            Console.SetCursorPosition(0, height / 2 + 1);
            Console.WriteLine("{0," + padding + "}", text);
        }
    }
}
