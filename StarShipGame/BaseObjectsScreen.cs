using System;
using System.Drawing;
namespace StarShipGame
{
    abstract class BaseObjectScreen
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        /// <summary>
        /// Конструктор объекта для заставки
        /// </summary>
        /// <param name="_pos"></param>
        /// <param name="_dir"></param>
        /// <param name="_size"></param>
        public BaseObjectScreen(Point _pos, Point _dir, Size _size)
        {
            pos = _pos;
            dir = _dir;
            size = _size;
        }
        /// <summary>
        /// Рисование объекта
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Действия связанные с движением
        /// </summary>
        public virtual void Update()
        {
            pos.X = pos.X - dir.X;
            pos.Y += dir.Y;
            if (pos.X < 0 || pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0 || pos.Y > Game.Height) dir.Y = -dir.Y;
            
        }
    }
}