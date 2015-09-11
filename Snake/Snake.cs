using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    public class Snake
    {
        public List<BodyPart> SnakeBody { get; set; }
        public int BodyPartSize { get; set; }

        public Snake(int size, int startX, int startY, int bodyPartSize)
        {
            this.SnakeBody = new List<BodyPart>();
            this.BodyPartSize = bodyPartSize;

            for (int i = 0; i < size; i++)
            {
                SnakeBody.Add(new BodyPart() { X = (startX - bodyPartSize * i), Y = startY });
            }
        }

        public void Move(Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    if (!DetectSelfCollision(0, -1))
                    {
                        Move(XMultiplier(direction), YMultiplier(direction));
                    }
                    break;
                case Directions.Down:
                    if (!DetectSelfCollision(0, 1))
                    {
                        Move(XMultiplier(direction), YMultiplier(direction));
                    }
                    break;
                case Directions.Right:
                    if (!DetectSelfCollision(1, 0))
                    {
                        Move(XMultiplier(direction), YMultiplier(direction));
                    }
                    break;
                case Directions.Left:
                    if (!DetectSelfCollision(-1, 0))
                    {
                        Move(XMultiplier(direction), YMultiplier(direction));
                    }
                    break;
                default:
                    break;
            }
        }

        public int XMultiplier(Directions direction)
        {
            if (direction == Directions.Right)
                return 1;
            else if (direction == Directions.Left)
                return -1;
            else
                return 0;
        }

        public int YMultiplier(Directions direction)
        {
            if (direction == Directions.Up)
                return -1;
            else if (direction == Directions.Down)
                return 1;
            else
                return 0;
        }

        private void Move(int xDiff, int yDiff)
        {
            for (int i = SnakeBody.Count - 1; i >= 1; i--)
            {
                SnakeBody[i].X = SnakeBody[i - 1].X;
                SnakeBody[i].Y = SnakeBody[i - 1].Y;
            }

            SnakeBody[0] = GetHeadNextPosition(xDiff, yDiff);
        }

        public BodyPart GetHeadNextPosition(int xDiff, int yDiff)
        {
            return new BodyPart()
            {
                X = SnakeBody[0].X + this.BodyPartSize * xDiff,
                Y = SnakeBody[0].Y + this.BodyPartSize * yDiff
            };
        }

        internal void Eat(Food result)
        {
            var lastBodyPartDirectionX = (SnakeBody[SnakeBody.Count - 2].X - SnakeBody[SnakeBody.Count - 1].X) / BodyPartSize;
            var lastBodyPartDirectionY = (SnakeBody[SnakeBody.Count - 2].Y - SnakeBody[SnakeBody.Count - 1].Y) / BodyPartSize;

            for (int i = 0; i < result.Bounty; i++)
            {
                SnakeBody.Add(
                    new BodyPart()
                    {
                        X = SnakeBody[SnakeBody.Count - 1].X * BodyPartSize * (-1) * lastBodyPartDirectionX,
                        Y = SnakeBody[SnakeBody.Count - 1].Y * BodyPartSize * (-1) * lastBodyPartDirectionY
                    });
            }
        }

        private bool DetectSelfCollision(int xDiff, int yDiff)
        {
            var potentialXHeadPosition = SnakeBody[0].X + this.BodyPartSize * xDiff;
            var potentialYHeadPosition = SnakeBody[0].Y + this.BodyPartSize * yDiff;
            for (int i = 1; i < SnakeBody.Count; i++)
            {
                if (potentialXHeadPosition == SnakeBody[i].X && potentialYHeadPosition == SnakeBody[i].Y)
                {
                    return true;
                }
            }

            return false;
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
