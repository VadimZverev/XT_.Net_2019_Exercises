namespace _28_Game.Bonuses
{
    /// <summary>
    /// Вишня, поднимающая здоровье на 20 пунктов.
    /// </summary>
    class Cherry : Bonus
    {
        public Cherry(int x, int y) : base(x, y)
        {
            Ability = 20;
        }
    }
}
