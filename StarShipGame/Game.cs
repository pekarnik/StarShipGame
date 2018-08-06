using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

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
        private static Timer timer;
        public static Random rand = new Random();
        /// <summary>
        /// Проигрыш
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            buffer.Render();
            Form.ActiveForm.KeyDown -= Form_KeyDown;
            Form.ActiveForm.Close();
            using (StreamWriter record = new StreamWriter("record.txt",true))
            {
                record.WriteLine(monstercounter);
            }
                filew?.Close();
            Application.Restart();
        }
      
        /// <summary>
        /// Инициализация графики
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {            
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;            
            monstercounter = 0;
            buls = new List<Bullet>();
            FileStream file = new FileStream("log.txt", FileMode.Create);
            file?.Close();
            filew = new StreamWriter("log.txt");
            if (!File.Exists("records.txt")) File.Create("records.txt");

            timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            form.SizeChanged += ActiveForm_SizeChanged;
        }
        /// <summary>
        /// Журнал с логами в файле
        /// </summary>
        public static StreamWriter filew;
        /// <summary>
        /// Действия по нажатиям кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (buls.Count > 10)
                {
                    buls.RemoveAt(0);
                }
                else
                {
                    buls.Add(new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(12, 0), new Size(20, 20)));
                }
            }
            if (e.KeyCode == Keys.Up) ship.Up(delegate
            {
            Console.WriteLine("Корабль взлетел выше");
            
                filew?.WriteLine("Корабль взлетел выше");
            });
            if (e.KeyCode == Keys.Down) ship.Down(delegate { Console.WriteLine("Корабль опустился");
                filew?.WriteLine("Корабль опустился");
            });
        }

        /// <summary>
        /// Проверка на размеры игрового
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ActiveForm_SizeChanged(object sender, EventArgs e)
        {
            if (Form.ActiveForm.Size.Height > 1000 || Form.ActiveForm.Size.Height < 0 || Form.ActiveForm.Size.Width > 1000 || Form.ActiveForm.Size.Width < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Выполняемые во время игры действия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();            
        }
        /// <summary>
        /// рисование объектов
        /// </summary>
        public static void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (ImageObject monster in monsters)
                monster?.Draw();            
            foreach (Bullet bul in buls)
            {
                bul?.Draw();
            }
            heal?.Draw();
            ship?.Draw();
            if (ship!=null)
            {
                buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                buffer.Graphics.DrawString("Score:" + monstercounter, SystemFonts.DefaultFont, Brushes.White, Game.Width-100, 0);
            }
            buffer.Render();
        }
        /// <summary>
        /// обновление позиций объектов
        /// </summary>
		public static void Update()
        {            
            foreach (BaseObject obj in objs)
                obj.Update();

            foreach (Bullet bul in buls)
            {
                bul?.Update();                
            }
            heal?.Update();
            for(var i=0;i<monsters.Length;i++)
            {
                if (monsters[i] == null) continue;
                monsters[i].Update();                
                for (int j=0;j<buls.Count;j++)
                {

                    if (buls[j]?.Pos.X >= Game.Width)
                    {
                        buls[j] = null;
                        continue;
                    }
                    if (buls[j] != null && monsters[i] != null )
                    {
                        if (buls[j].Collision(monsters[i]))
                        {
                            System.Media.SystemSounds.Hand.Play();                            
                            monstercounter++;
                            heal = new ImageObject(monsters[i].Pos, monsters[i].Dir, new Size(40,40), "heal.jpg");
                            monsters[i] = null;
                            buls[j] = null;
                            continue;
                        }
                    }                    
                }
                if(monsters[i]==null)
                {
                    continue;
                }
                if (!ship.Collision(monsters[i])) continue;
                var rnd = new Random();
                ship?.EnergyLow(rnd.Next(10, 40),delegate { Console.WriteLine("Корабль поврежден");                    
                    filew?.WriteLine("Корабль поврежден");
                });
                monsters[i] = null;
                System.Media.SystemSounds.Asterisk.Play();
                monstercounter++;
                if (ship.Energy <= 0) ship?.Die();                 
            }
            if (heal != null)
            {
                if (heal.Collision(ship)) ship.EnergyUp(rand.Next(10, 40), delegate {
                    Console.WriteLine("Корабль отремонтирован");
                    filew?.WriteLine("Корабль отремонтирован");
                    heal = null;
                });
            }
            ///
            /// Победа
            ///
            if(monstercounter==monsters.Length)
            {
                timer.Stop();
                buffer.Graphics.DrawString("You win!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
                buffer.Render();
                filew?.Close();
                using (StreamWriter record = new StreamWriter("record.txt",true))
                {
                    record.WriteLine(monstercounter);
                }
                Form.ActiveForm.KeyDown -= Form_KeyDown;
                Form.ActiveForm.KeyDown += Form_PressAnyKey;
                Application.Restart();
            }
           
        }
        /// <summary>
        /// Закрытие формы после конца игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_PressAnyKey(object sender, KeyEventArgs e)
        {
            Form.ActiveForm.Close();
        }

        public static BaseObject[] objs;
        public static BaseObject[] monsters;
        public static List<Bullet> buls;
        public static BaseObject heal;
        /// <summary>
        /// инициализация объектов
        /// </summary>
        public static void Load()
        {
            Random rnd = new Random();
            objs = new BaseObject[30];
            for (int i = 0; i < objs.Length-1; i++)
            {
                int tmp = rnd.Next(5, 25);
                objs[i] = new Star(new Point(i*50, i * 35), new Point(50 - i, 50 - i), new Size(tmp, tmp));
            }         
            objs[objs.Length-1] = new CoolComet(new Point(29 * 12, 400), new Point(10, 15));
            monsters = new ImageObject[10];
            for(int i=0;i<monsters.Length;i++)
            {
                int tmp = rnd.Next(5, 25);
                monsters[i] = new ImageObject(new Point((i+40) * 12, i * 30), new Point(5 - i, 15 - i), new Size((int)(tmp * 2.2), (int)(tmp * 2.2)),"image.png");
            }
            //bul = new Bullet(new Point(0,rnd.Next(0,500)),new Point(10,0),new Size(49,100));
        }        
        private static Ship ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(100, 100));
        private static int monstercounter;
    }
}