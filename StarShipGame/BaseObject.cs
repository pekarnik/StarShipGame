using System;
using System.Drawing;
namespace StarShipGame
{
    abstract class BaseObject:ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        /// <summary>
        /// Создание базового объекта
        /// </summary>
        /// <param name="_pos"></param>
        /// <param name="_dir"></param>
        /// <param name="_size"></param>
        public BaseObject(Point _pos, Point _dir, Size _size)
        {   
            if (size.Height > Game.Height / 2 || size.Width > Game.Width / 2)
            {
                throw new GameObjectException();
            }
            pos = _pos;
            dir = _dir;
            size = _size;
        }
        /// <summary>
        /// Начальная позиция объекта
        /// </summary>
        public Point Pos
        {
            get=>pos;
            set => pos = value;
        }
        /// <summary>
        /// Скорость перемещения объекта в пространстве
        /// </summary>
        public Point Dir
        {
            get => dir;
            set => dir = value;
        }
        /// <summary>
        /// Размер объекта
        /// </summary>
        public Size Size
        {
            get => size;
            set => size = value;
        }
        /// <summary>
        /// конструктор для загрузки изображения
        /// </summary>
        /// <param name="_pos"></param>
        /// <param name="_dir"></param>
        /// <param name="_size"></param>
        /// <param name="filename"></param>
        public BaseObject(Point _pos, Point _dir, Size _size,string filename):this(_pos,_dir,_size)
        {
            image = Image.FromFile(filename);
        }
        private Image image;
        /// <summary>
        /// рисование объекта
        /// </summary>
        public virtual void Draw()
        {            
            Game.buffer.Graphics.DrawImage(image, pos.X, pos.Y, size.Width, size.Height);
        }
        /// <summary>
        /// Действия, связанные с объектом
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// Столкновение с другим объектом
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        /// <summary>
        /// границы объекта
        /// </summary>
        public Rectangle Rect => new Rectangle(pos, size);
        public delegate void Message();
        /// <summary>
        /// Собственное исключение
        /// </summary>
        internal class GameObjectException : Exception
        {
            public GameObjectException(string msg)
            {
                Message = msg;
            }
            public GameObjectException():base()
            {
                Message = "GameObjectException";
            }            
            public string Message { get; }
        }
    }
}