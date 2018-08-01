using System;
using System.Drawing;
namespace StarShipGame
{
    class Star:BaseObject
    {
        public Star(Point _pos,Point _dir, Size _size):base(_pos,_dir,_size)
        {

        }
        public override void Draw()
        {
            Game.buffer.Graphics.DrawLine(Pens.White, pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Height);
            Game.buffer.Graphics.DrawLine(Pens.White, pos.X + size.Width, pos.Y, pos.X , pos.Y + size.Height);
        }
        public override void Update()
        {
            pos.X -= dir.X;
            if (pos.X < 0) pos.X = Game.Width + size.Width;
        }
    }
}
