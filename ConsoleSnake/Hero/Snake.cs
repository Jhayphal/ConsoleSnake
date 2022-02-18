using ConsoleSnake.Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleSnake.Hero
{
    internal sealed class Snake
    {
		public static SnakeHeadDirection HeadDirection { get; set; } = SnakeHeadDirection.Right;
		public static char Surface { get; set; } = '#';
		public static ConsoleColor Color { get; set; } = ConsoleColor.Green;

		private Point Position;
		private Snake Next;

		public Snake(Point position)
        {
			Position = position;
        }

		public bool InMyHead(Point point)
        {
			return Position == point;
        }

		public bool InsideMe(Point point)
		{
			if (Position.X == point.X && Position.Y == point.Y)
				return true;

			if (Next == null)
				return false;

			return Next.InsideMe(point);
		}

		private void DrawSelf(bool erase)
        {
			if (erase)
				Painter.DrawChar(' ', Position);
			else
				Painter.DrawChar(Surface, Position, Color);
        }

		public bool HasCollisions(Rectangle world)
        {
			return HasCollisions(new HashSet<Point>(), ref world);
        }

		private bool HasCollisions(HashSet<Point> points, ref Rectangle world)
		{
			if (!points.Add(Position))
				return true;

			if (Position.X < 0 || Position.X >= world.Width)
				return true;

			if (Position.Y < 0 || Position.Y >= world.Height)
				return true;

			if (Next == null)
				return false;

			return Next.HasCollisions(points, ref world);
		}

		public void Eat()
        {
			if (Next == null)
            {
				Next = new Snake(Position);

				return;
            }

			Next.Eat();
        }

		public void DrawAndMove()
        {
			DrawSelf(erase: false);

			DrawAndMoveTail(ref Position);

			switch (HeadDirection)
            {
				case SnakeHeadDirection.Up:
					--Position.Y;

					break;

				case SnakeHeadDirection.Down:
					++Position.Y;

					break;

				case SnakeHeadDirection.Left:
					--Position.X;

					break;

				case SnakeHeadDirection.Right:
					++Position.X;

					break;

				default:
					throw new NotImplementedException(nameof(HeadDirection));
			}
        }

		private void DrawAndMoveTail(ref Point position)
        {
			if (Next == null)
				DrawSelf(erase: true);
			else
				Next.DrawAndMoveTail(ref Position);

			Position = position;
        }

        public override string ToString()
        {
            return $"{Position.X}:{Position.Y}";
        }
    }
}
