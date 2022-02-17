using System;
using System.Linq;

namespace ConsoleSnake
{
    internal static class SnakeBuilder
    {
		public static Snake Create(char surface, ConsoleColor color, int length)
        {
			if (length <= 0)
				throw new ArgumentOutOfRangeException(nameof(length));

			var position = GameArea.GetCenter();

			var snake = new Snake(surface, color, position);

			Snake.HeadDirection = SnakeHeadDirection.Right;

			foreach (var _ in Enumerable.Range(0, length))
			{
				snake.Eat();
				snake.DrawAndMove();
			}

			return snake;
		}
    }
}
