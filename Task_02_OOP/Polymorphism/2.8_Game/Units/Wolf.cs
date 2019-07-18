namespace _28_Game.Units
{
    /// <summary>
    /// Волк
    /// </summary>
    class Wolf : Unit
    {
        /// <summary>
        /// Инициализирует экземпляр волка с вручную заданными координатами.
        /// По умолчанию имеет 150 пунктов жизни и 10 пунктов уровня атаки.
        /// </summary>
        /// <param name="x">ось Х</param>
        /// <param name="y">ось У</param>
        public Wolf(int x, int y)
            : this(x, y, health: 150, attack: 10) { }

        /// <summary>
        /// Инициализирует экземпляр волка с вручную заданными координатами,
        /// а также уровнем жизни и атаки.
        /// </summary>
        /// <param name="x">ось Х</param>
        /// <param name="y">ось У</param>
        /// <param name="health">уровень жизни</param>
        /// <param name="attack">уровень атаки</param>
        public Wolf(int x, int y, int health, int attack)
            : base(x, y, health, attack) { }
    }
}
