using System;
using System.Drawing;
namespace StarShipGame
{
	class CoolComet:BaseObject
    {
        /// <summary>
        /// Делегат для рисования
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private delegate double Fun(double x);
		public CoolComet(Point _pos,Point _dir):base(_pos,_dir,new Size(200,25))
        {

        }
        /// <summary>
        /// Рисование "Кометы"
        /// </summary>
        public override void Draw()
        {
            Game.buffer.Graphics.DrawLines(Pens.OrangeRed, FuncDraw(delegate (double x) { return Math.Sin(x); },pos.X+100,pos.Y+10));
            Game.buffer.Graphics.DrawLines(Pens.OrangeRed, FuncDraw(delegate (double x) { return Math.Cos(x); }, pos.X+100, pos.Y+25));
            Game.buffer.Graphics.DrawLines(Pens.OrangeRed, FuncDraw(delegate (double x) { return x=1; }, pos.X + 100, pos.Y + 17));
            Game.buffer.Graphics.FillEllipse(Brushes.Yellow, pos.X, pos.Y, 100, 35);
        }
        /// <summary>
        /// Рисование функции(части кометы)
        /// </summary>
        /// <param name="F"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
		private static PointF[] FuncDraw(Fun F,int x,int y)
        {
            PointF[] points = new PointF[100];
			for(int i=0;i<100;i++)
            {
                points[i]=new PointF(i+x,(float)F(i)+y);
            }
            return points;
        }
        /// <summary>
        /// Движение кометы
        /// </summary>
        public override void Update()
        {
            Random rnd = new Random();
            pos.X -= dir.X*2;
            if (pos.X < 0) { pos.X = Game.Width + size.Width; pos.Y = rnd.Next(0, 500); }
        }
    }
}