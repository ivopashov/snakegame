using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    public class Snake
    {
        public List<BodyPart> SnakeBody { get; set; }
        public int BodyPartSize { get; set; }
        public Directions CurrentDirection { get; set; }

        public Snake(int size, int startX, int startY, int bodyPartSize)
        {
            this.SnakeBody = new List<BodyPart>();
            this.BodyPartSize = bodyPartSize;
            CurrentDirection = Directions.Right;

            for (int i = 0; i < size; i++)
            {
                SnakeBody.Add(new BodyPart() { X = (startX - bodyPartSize * i), Y = startY });
            }
        }

        public void Move(Directions direction)
        {
            if (IsDirectionAllowed(direction))
            {
                switch (direction)
                {
                    case Directions.Up:
                        if (!DetectSelfCollision(0, -1))
                        {
                            Move(0, -1);
                            this.CurrentDirection = direction;
                        }
                        break;
                    case Directions.Down:
                        if (!DetectSelfCollision(0, 1))
                        {
                            Move(0, 1);
                            this.CurrentDirection = direction;
                        }
                        break;
                    case Directions.Right:
                        if (!DetectSelfCollision(1,0))
                        {
                            Move(1, 0);
                            this.CurrentDirection = direction;
                        }
                        break;
                    case Directions.Left:
                        if (!DetectSelfCollision(-1, 0))
                        {
                            Move(-1, 0);
                            this.CurrentDirection = direction;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Move(int xDiff, int yDiff)
        {
            for (int i = SnakeBody.Count - 1; i >= 1; i--)
            {
                SnakeBody[i].X = SnakeBody[i - 1].X;
                SnakeBody[i].Y = SnakeBody[i - 1].Y;
            }

            SnakeBody[0].X += this.BodyPartSize * xDiff;
            SnakeBody[0].Y += this.BodyPartSize * yDiff;
        }

        private bool IsDirectionAllowed(Directions direction)
        {
            if (((direction == Directions.Up && CurrentDirection == Directions.Down) || (direction == Directions.Down && CurrentDirection == Directions.Up))
                || ((direction == Directions.Right && CurrentDirection == Directions.Left) || (direction == Directions.Left && CurrentDirection == Directions.Right))
                )
            {
                return false;
            }
            return true;
        }

        private bool DetectSelfCollision(int xDiff, int yDiff)
        {
            for (int i = 1; i < SnakeBody.Count; i++)
            {
                if(((SnakeBody[0].X + this.BodyPartSize * xDiff)==SnakeBody[i].X && (SnakeBody[0].Y + this.BodyPartSize * yDiff)==SnakeBody[i].Y)){
                    return false;
                }
            }

            return true;
        }
    }

    public class BodyPart
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }
}
