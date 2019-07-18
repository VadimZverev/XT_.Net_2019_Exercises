namespace _28_Game.Bonuses
{
    /// <summary>
    /// Яблоко, поднимающее жизнь игрока на 10 пунктов.
    /// </summary>
    class Apple : Bonus
    {
        public Apple(int x, int y) : base(x, y)
        {
            Ability = 10;
        }
    }
}
