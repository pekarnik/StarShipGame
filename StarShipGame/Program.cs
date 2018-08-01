﻿using System;
using System.Windows.Forms;
namespace StarShipGame
{
    class Program
    {
        static Button btnNewGame;
        static Button btnRecords;
        static Button btnExit;
        static Label lblMyName;
        static Form form;
        [STAThread]
        static void Main()
        {           
            form = new Form();
            form.Width = 800;
            form.Height = 600;
            btnNewGame = new Button();
            btnNewGame.Text = "Новая игра";
            btnNewGame.Height = 20;
            btnNewGame.Width = 200;
            btnNewGame.Top = 40;
            btnNewGame.Left = 300;
            btnNewGame.Click += BtnNewGame_Click;
            btnNewGame.Parent = form;
            btnRecords = new Button();
            btnRecords.Text = "Рекорды";
            btnRecords.Height = 20;
            btnRecords.Width = 200;
            btnRecords.Top = 240;
            btnRecords.Left = 300;
            btnRecords.Click += BtnRecords_Click;
            btnRecords.Parent = form;
            lblMyName = new Label();
            lblMyName.Text = "Кадомский Андрей";
            lblMyName.Height = 40;
            lblMyName.Width = 200;
            lblMyName.Top = 30;
            lblMyName.Left = 600;
            lblMyName.Parent = form;
            btnExit = new Button();
            btnExit.Text = "Выход";
            btnExit.Height = 20;
            btnExit.Width = 200;
            btnExit.Top = 440;
            btnExit.Left = 300;
            btnExit.Click += BtnExit_Click;
            btnExit.Parent = form;
            SplashScreen.Init(form);
            SplashScreen.Draw();
            form.FormClosed += Form_FormClosed;
            form.Visible = true;
            Application.Run(form);
        }

        private static void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private static void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void BtnRecords_Click(object sender, EventArgs e)
        {
        }

        private static void BtnNewGame_Click(object sender, EventArgs e)
        {
            form.Hide();
            Form form1 = new Form();
            form1.Width = 800;
            form1.Height = 600;
            Game.Init(form1);           
            form1.Show();
            Game.Draw();
            form1.FormClosed += Form1_FormClosed;
        }        

        private static void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}