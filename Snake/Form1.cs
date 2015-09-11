using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Snake Snake { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.Snake = new Snake(3, 50, 50, 3);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Snake.Move(Directions.Up);
                    break;
                case Keys.Down:
                    Snake.Move(Directions.Down);
                    break;
                case Keys.Left:
                    Snake.Move(Directions.Left);
                    break;
                case Keys.Right:
                    Snake.Move(Directions.Right);
                    break;
            }

            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in Snake.SnakeBody)
            {
                var g = e.Graphics;
                g.DrawRectangle(new Pen(Color.Black), item.X, item.Y, Snake.BodyPartSize, Snake.BodyPartSize);
            }
        }
    }
}
