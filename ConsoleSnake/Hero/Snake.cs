using ConsoleSnake.Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleSnake.Hero
{
	internal sealed class Snake
    {
        public char Surface { get; set; } = '0';
        
        public ConsoleColor Color { get; set; } = ConsoleColor.Green;
		
        public SnakeHeadDirection HeadDirection { get; set; } = SnakeHeadDirection.Right;

        private Point HeadLocation;
        private readonly List<Point> Location = new List<Point>();
        
        public Snake(Point location)
        {
            HeadLocation = location;
        }

        private void Draw(Point point, char symbol)
        {
            Painter.DrawChar(symbol, point, Color);
        }

        public void DrawAndMove()
        {
            Draw(HeadLocation, Surface);

            DrawAndMoveTail();

            switch (HeadDirection)
            {
                case SnakeHeadDirection.Up:
                    --HeadLocation.Y;

                    break;

                case SnakeHeadDirection.Down:
                    ++HeadLocation.Y;

                    break;

                case SnakeHeadDirection.Left:
                    --HeadLocation.X;

                    break;

                case SnakeHeadDirection.Right:
                    ++HeadLocation.X;

                    break;

                default:
                    throw new NotImplementedException(nameof(HeadDirection));
            }
        }

        public void Eat()
        {
            if (Location.Count == 0)
                Location.Add(HeadLocation);
            else
                Location.Add(Location[Location.Count - 1]);
        }

        public bool HasCollisions(Rectangle world)
        {
            if (Inside(world, HeadLocation))
                return true;

            var points = new HashSet<Point> { HeadLocation };

            foreach (var point in Location)
            {
                if (!points.Add(point))
                    return true;

                if (Inside(world, point))
                    return true;
            }

            return false;
        }

        public bool InMyHead(Point point)
        {
            return HeadLocation == point;
        }

        public bool InsideMe(Point point)
        {
            return InMyHead(point)
                || Location.Contains(point);
        }

        private void DrawAndMoveTail()
        {
            var endOfTail = Location[Location.Count - 1];

            Draw(endOfTail, ' ');

            for (int i = Location.Count - 1; i > 0; --i)
                Location[i] = Location[i - 1];

            Location[0] = HeadLocation;
        }

        private bool Inside(Rectangle rectangle, Point point)
        {
            if (point.X < rectangle.X
                || point.X >= rectangle.Width)
                return true;

            return point.Y < rectangle.Y
                || point.Y >= rectangle.Height;
        }
    }
}
