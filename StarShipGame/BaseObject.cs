using System;
using System.Drawing;
namespace StarShipGame
{
    class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        public BaseObject(Point _pos, Point _dir, Size _size)
        {
            pos = _pos;
            dir = _dir;
            size = _size;
        }
        private static Image image = Image.FromFile("image.png");
        public virtual void Draw()
        {            
            Game.buffer.Graphics.DrawImage(image, pos.X, pos.Y, size.Width, size.Height);
        }
        public virtual void Update()
        {
            pos.X = pos.X - dir.X;
            pos.Y += dir.Y;
            if (pos.X < 0 || pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0 || pos.Y > Game.Height) dir.Y = -dir.Y;
        }
    }
}