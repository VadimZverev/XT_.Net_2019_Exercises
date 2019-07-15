using _28_Game.Bonuses;
using System;

namespace _28_Game.Units
{
    class Player : Unit
    {
        public Player() : this(0, 0) { }

        public Player(int x, int y)
            : this(x, y, health: 100, attack: 0) { }

        public Player(int x, int y, int health, int attack)
            : base(x, y, health, attack) { }

        public void UpAbility(Bonus bonus)
        {
            switch (bonus)
            {
                case Apple _:
                case Cherry _:
                    Health += bonus.Ability;
                    break;
                case Sword _:
                    Attack = bonus.Ability;
                    break;
            }
        }

        public void Move(ConsoleKey motion)
        {
            switch (motion)
            {
                case ConsoleKey.DownArrow:
                    Move(Position.X, Position.Y - 1);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(Position.X - 1, Position.Y);
                    break;
                case ConsoleKey.RightArrow:
                    Move(Position.X + 1, Position.Y);
                    break;
                case ConsoleKey.UpArrow:
                    Move(Position.X, Position.Y + 1);
                    break;
            }
        }
    }
}
