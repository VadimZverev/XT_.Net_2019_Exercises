namespace _28_Game.General
{
    abstract class GameObject
    {
        public GameObject()
        {
            Position = new Point();
        }

        public GameObject(int x, int y)
        {
            Position = new Point(x, y);
        }

        public Point Position { get; protected set; }
    }
}
