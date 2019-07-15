using _28_Game.General;

namespace _28_Game.Bonuses
{
    abstract class Bonus : GameObject
    {
        public Bonus(int x, int y) 
            : base(x, y) { }

        public int Ability { get; protected set; } = 0;
    }
}
