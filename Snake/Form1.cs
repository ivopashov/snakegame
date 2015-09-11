using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Engine Engine { get; set; }

        public Form1()
        {
            InitializeComponent();
            Engine = new Engine(3,50,50,10,10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Directions direction;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction=Directions.Up;
                    break;
                case Keys.Down:
                    direction = Directions.Down;
                    break;
                case Keys.Left:
                    direction = Directions.Left;
                    break;
                case Keys.Right:
                    direction = Directions.Right;
                    break;
                default:
                    //think of a better way.
                    direction = Directions.Up;
                    break;
            }

            if (Engine.DetectSelfCollision(direction))
            {
                this.Dispose();
            }

            Engine.Move(direction);

            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach (var item in Engine.Snake.SnakeBody)
            {
                g.DrawRectangle(new Pen(Color.Black), item.X, item.Y, Engine.Snake.BodyPartSize, Engine.Snake.BodyPartSize);
            }

            foreach (var item in Engine.Bites)
            {
                var myBrush = new SolidBrush(item.Color);
                g.FillRectangle(myBrush, item.X, item.Y, item.Size, item.Size);
            }
        }
    }
}
