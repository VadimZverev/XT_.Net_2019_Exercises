namespace _28_Game.Units
{
    class Wolf : Unit
    {
        public Wolf(int x, int y)
            : this(x, y, health: 150, attack: 10) { }

        public Wolf(int x, int y, int health, int attack)
            : base(x, y, health, attack) { }
    }
}
