﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Game;

namespace moora_client
{
    public partial class MainForm : Form
    {
        private Random rnd = new Random((int)DateTime.Now.Ticks);
        private Pen myPen = new Pen(Color.Red);
        World world;
        double invFreq = 1.0 / (double)Stopwatch.Frequency;
        long time;

        public MainForm()
        {
            world = new World();

            InitializeComponent();

            time = Stopwatch.GetTimestamp();
            timer1.Start();
        }

        // free up resources on program exit
        private void Form1_Closed(object sender, System.EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                Close();
            }
        }

        private void splitContainer1_Panel1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MouseEventArgs args = (System.Windows.Forms.MouseEventArgs)e;
            world.Click(args.X, args.Y);
            this.Invalidate(true);
        }

        private void RedrawButton_Click(object sender, EventArgs e)
        {
            this.Invalidate(true);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            world.Draw(e.Graphics);

            float x = world.character.X;
            float y = world.character.Y;
            character_x.Text = ((int)x).ToString();
            character_y.Text = ((int)y).ToString();
            int fx = ((int)(100.0f * (x - (int)x)));
            int fy = ((int)(100.0f * (y - (int)y)));
            string sfx = fx.ToString();
            if (sfx.Length < 2)
            {
                sfx = "0" + sfx;
            }
            string sfy = fy.ToString();
            if (sfy.Length < 2)
            {
                sfy = "0" + sfy;
            }

            character_x_frac.Text = "." + sfx;
            character_y_frac.Text = "." + sfy;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            MainForm_KeyPress(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            long current = Stopwatch.GetTimestamp();
            long delta = current - time;
            float deltaTime = (float)(delta * invFreq);
            time = current;

            world.Update(deltaTime);
            this.Invalidate(true);
        }
    }
}
