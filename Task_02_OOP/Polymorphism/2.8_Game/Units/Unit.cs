using _28_Game.General;

namespace _28_Game.Units
{
    /// <summary>
    /// Абстрактный класс Юнит. Является базовым классом, 
    /// для подвижных объектов на игровом поле.
    /// </summary>
    abstract class Unit : GameObject
    {
        public Unit(int x, int y) : base(x, y) { }

        public Unit(int x, int y, int health, int attack)
            : base(x, y)
        {
            Health = health;
            Attack = attack;
        }

        public int Health { get; protected set; }
        public int Attack { get; protected set; }

        protected virtual void Move(Point point)
        {
            Position = point;
        }

        protected virtual void Move(int x, int y)
        {
            Position = new Point(x, y);
        }
    }
}
