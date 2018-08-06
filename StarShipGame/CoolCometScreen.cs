using System;
using System.Drawing;
namespace StarShipGame
{
	class CoolCometScreen:BaseObjectScreen
    {
        /// <summary>
        /// делегат для рисования частей кометы
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private delegate double Fun(double x);
        /// <summary>
        /// Вызываем базовый конструктор
        /// </summary>
        /// <param name="_pos"></param>
        /// <param name="_dir"></param>
        /// <param name="_size"></param>
		public CoolCometScreen(Point _pos,Point _dir,Size _size):base(_pos,_dir,_size)
        {

        }
        /// <summary>
        /// Рисуем комету
        /// </summary>
        public override void Draw()
        {
            Random rnd = new Random();
            SplashScreen.buffer.Graphics.DrawLines(Pens.OrangeRed, FuncDraw(delegate (double x) { return Math.Sin(x); },pos.X+100,pos.Y+10));
            SplashScreen.buffer.Graphics.DrawLines(Pens.OrangeRed, FuncDraw(delegate (double x) { return Math.Cos(x); }, pos.X+100, pos.Y+25));
            SplashScreen.buffer.Graphics.DrawLines(Pens.OrangeRed, FuncDraw(delegate (double x) { return x=1; }, pos.X + 100, pos.Y + 17));
            SplashScreen.buffer.Graphics.FillEllipse(Brushes.Yellow, pos.X, pos.Y,rnd.Next(10,20) ,rnd.Next(10,20));
        }
        /// <summary>
        /// функция-это часть кометы
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
            pos.X -= dir.X;
            pos.Y -= dir.Y;
            if (pos.X < 0||pos.X>SplashScreen.Width) { pos.X = SplashScreen.Width + size.Width; pos.Y = rnd.Next(0, 500); }
            if (pos.Y < 0 || pos.Y > SplashScreen.Height) { pos.Y = SplashScreen.Height + size.Height; }
        }
    }
}