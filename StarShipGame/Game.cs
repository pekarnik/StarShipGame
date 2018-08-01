using System;
using System.Windows.Forms;
using System.Drawing;
namespace StarShipGame
{
	static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;
		public static int Width { get; set; }
		public static int Height { get; set; }
		static Game()
        {

        }
		public static void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
			//Random rnd=new Random();
   //         buffer.Graphics.Clear(Color.Black);
   //         buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
   //         buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
   //         buffer.Render();

            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            buffer.Render();
        }
		public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }
        public static BaseObject[] objs;
		public static void Load()
        {
            Random rnd = new Random();
            objs = new BaseObject[30];
            for (int i = 0; i < objs.Length/2; i++)
            {
                int tmp = rnd.Next(5, 25);
                objs[i] = new Star(new Point(i*50, i * 35), new Point(50 - i, 50 - i), new Size(tmp, tmp));
            }
            for(int i=objs.Length/2;i<objs.Length-1;i++)
            {
                int tmp = rnd.Next(5, 25);
                objs[i]=new BaseObject(new Point(i*12,i*30),new Point(15-i,45-i),new Size((int)(tmp*2.2), (int)(tmp*2.2)));
            }
            int tmp1 = rnd.Next(5, 25);
            objs[objs.Length-1] = new CoolComet(new Point(29 * 12, 400), new Point(10, 15), new Size(tmp1, tmp1));
        }
    }
}