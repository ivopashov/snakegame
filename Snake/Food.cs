using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food
    {
        public Food(int x, int y, int bounty,int size)
        {
            this.X = x;
            this.Y = y;
            this.Bounty = bounty;
            this.Size = size;

            if (bounty == 1)
            {
                this.Color = Color.Brown;
            }
            else if(bounty==2)
            {
                this.Color = Color.Silver;
            }
            else
            {
                this.Color = Color.Gold;
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; }
        public int Bounty { get; set; }
    }
}
