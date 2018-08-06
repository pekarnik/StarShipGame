using System;
using System.Drawing;

namespace StarShipGame
{
    class Bullet : BaseObject
    {
        /// <summary>
        /// Вызываем конструктор из BaseObject
        /// </summary>
        /// <param name="_pos"></param>
        /// <param name="_dir"></param>
        /// <param name="_size"></param>
        public Bullet(Point _pos, Point _dir, Size _size) : base(_pos, _dir,_size, "bullet.png")
        {

        }
        /// <summary>
        /// Движение объекта
        /// </summary>
        public override void Update()
        {
            Random rnd = new Random();
            pos.X+= dir.X;                   
        }        
    }
}
