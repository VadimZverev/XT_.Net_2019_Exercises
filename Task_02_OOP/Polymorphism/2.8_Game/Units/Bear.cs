using _28_Game.General;

namespace _28_Game.Units
{
    class Bear : Unit
    {
        public Bear(int x, int y)
            : base(x, y, health: 300, attack: 30) { }

        public Bear(int x, int y, int health, int attack)
            : base(x, y, health, attack) { }
    }
}
