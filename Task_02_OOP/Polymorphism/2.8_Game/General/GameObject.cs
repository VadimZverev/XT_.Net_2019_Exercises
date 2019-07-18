namespace _28_Game.General
{
    /// <summary>
    /// Абстрактный класс объекта на поле. Содержит координаты местоположения объекта на игровом поле.
    /// </summary>
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

        /// <summary>
        /// Позиция на игровом поле.
        /// </summary>
        public Point Position { get; protected set; }
    }
}
