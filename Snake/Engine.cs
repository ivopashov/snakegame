using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Engine
    {
        public Snake Snake { get; set; }
        public List<Food> Bites { get; set; }
        public Directions CurrentDirection { get; set; }

        public Engine(int snakeBodyParts, int snakeStartX, int snakeStartY, int snakeSize, int numberOfBites)
        {
            Snake = new Snake(snakeBodyParts, snakeStartX, snakeStartY, snakeSize);
            Bites = new List<Food>();
            CurrentDirection = Directions.Right;

            var random = new Random();

            for (int i = 0; i < numberOfBites; i++)
            {
                var bounty = random.Next(1, 4);
                var randomX = random.Next(1, 30);
                var randomY = random.Next(1, 30);
                Bites.Add(new Food(randomX * 10, randomY * 10, bounty, 10));
            }
        }

        internal bool DetectSelfCollision(Directions direction)
        {
            var potentialXHeadPosition = Snake.SnakeBody[0].X + Snake.BodyPartSize * Snake.XMultiplier(direction);
            var potentialYHeadPosition = Snake.SnakeBody[0].Y + Snake.BodyPartSize * Snake.YMultiplier(direction);
            for (int i = 1; i < Snake.SnakeBody.Count; i++)
            {
                if (potentialXHeadPosition == Snake.SnakeBody[i].X && potentialYHeadPosition == Snake.SnakeBody[i].Y)
                {
                    return true;
                }
            }

            return false;
        }

        public void Move(Directions direction)
        {
            if (IsDirectionAllowed(direction))
            {
                CurrentDirection = direction;
                DetectFoodCollision();

                switch (direction)
                {
                    case Directions.Up:
                        Snake.Move(direction);
                        break;
                    case Directions.Right:
                        Snake.Move(direction);
                        break;
                    case Directions.Left:
                        Snake.Move(direction);
                        break;
                    case Directions.Down:
                        Snake.Move(direction);
                        break;
                    default:
                        break;

                }
            }
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

        private void DetectFoodCollision()
        {
            var nextHeadPosition = Snake.GetHeadNextPosition(Snake.XMultiplier(CurrentDirection), Snake.YMultiplier(CurrentDirection));
            var result = Bites.SingleOrDefault(a => a.X == nextHeadPosition.X && a.Y == nextHeadPosition.Y);
            if (result!=null)
            {
                Snake.Eat(result);
                Bites.Remove(result);
            }
        }
    }
}
