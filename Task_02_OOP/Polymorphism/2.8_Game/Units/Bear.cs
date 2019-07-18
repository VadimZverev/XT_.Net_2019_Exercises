using _28_Game.General;

namespace _28_Game.Units
{
    /// <summary>
    /// Медведь
    /// </summary>
    class Bear : Unit
    {
        /// <summary>
        /// Инициализирует экземпляр медведя с вручную заданными координатами.
        /// По умолчанию имеет 300 пунктов жизни и 20 пунктов уровня атаки.
        /// </summary>
        /// <param name="x">ось Х</param>
        /// <param name="y">ось У</param>
        public Bear(int x, int y)
            : base(x, y, health: 300, attack: 20) { }

        /// <summary>
        /// Инициализирует экземпляр медведя с вручную заданными координатами,
        /// а также уровнем жизни и атаки.
        /// </summary>
        /// <param name="x">ось Х</param>
        /// <param name="y">ось У</param>
        /// <param name="health">уровень жизни</param>
        /// <param name="attack">уровень атаки</param>
        public Bear(int x, int y, int health, int attack)
            : base(x, y, health, attack) { }
    }
}
