using _28_Game.General;

namespace _28_Game.Obstacles
{
    class Tree : GameObject
    {
        public Point OptPosiotion { get; }

        public Tree(int x, int y) : base(x, y) 
            => OptPosiotion = new Point(Position.X, Position.Y + 1);
    }
}
