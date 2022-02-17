using System;
using System.Drawing;

namespace ConsoleSnake
{
    internal class SnakeFood
    {
		private readonly char Surface;
		private readonly ConsoleColor Color;

		private Point position;
		private readonly Random random = new Random(DateTime.Now.Millisecond);

		public Point Position => position;

		public SnakeFood(char surface, ConsoleColor color)
        {
			Surface = surface;
			Color = color;
        }

		public void CreateNew()
        {
			position.X = random.Next(Console.WindowWidth);
			position.Y = random.Next(Console.WindowHeight);
		}

		public void Draw()
        {
			Painter.DrawChar(Surface, position, Color);
		}
    }
}
