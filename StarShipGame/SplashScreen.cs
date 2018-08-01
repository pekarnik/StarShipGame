using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarShipGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        static SplashScreen()
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
            buffer.Graphics.Clear(Color.Black);
            buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            buffer.Render();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        public static BaseObjectScreen[] objs;
        public static void Load()
        {
            Random rnd = new Random();
            objs = new BaseObjectScreen[30];
            for (int i = 0; i < objs.Length; i++)
            {
                int tmp = rnd.Next(5, 25);
                objs[i] = new CoolCometScreen(new Point(i*50,i*35),new Point(10,10), new Size(tmp, tmp));
            }
          
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public static void Update()
        {
            foreach (BaseObjectScreen obj in objs)
                obj.Update();
        }
        public static void Draw()
        {     

            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObjectScreen obj in objs)
                obj.Draw();
            buffer.Render();
        }
    }
}