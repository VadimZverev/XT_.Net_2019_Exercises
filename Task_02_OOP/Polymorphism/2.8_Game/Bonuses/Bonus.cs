using _28_Game.General;

namespace _28_Game.Bonuses
{
    /// <summary>
    /// Абстрактный класс Бонус.
    /// </summary>
    abstract class Bonus : GameObject
    {
        public Bonus(int x, int y) 
            : base(x, y) { }

        /// <summary>
        /// Содержит свойство, которое повышает одну из характеристик игрока.
        /// </summary>
        public int Ability { get; protected set; } = 0;
    }
}
