namespace _28_Game.Bonuses
{
    /// <summary>
    /// Меч, повышающий атаку на 100 пунктов.
    /// </summary>
    class Sword : Bonus
    {
        public Sword(int x, int y) : base(x, y)
        {
            Ability = 100;
        }
    }
}
