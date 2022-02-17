using System;
using System.Linq;

namespace ConsoleSnake
{
    internal static class SnakeBuilder
    {
		public static Snake Create(SnakeBuilderSettings settings)
        {
			if (settings.Length <= 0)
				throw new ArgumentOutOfRangeException(nameof(settings.Length));

			var position = GameArea.GetCenter();

			Snake.Color = settings.Color;
			Snake.Surface = settings.Surface;

			var snake = new Snake(position);

			Snake.HeadDirection = SnakeHeadDirection.Right;

			foreach (var _ in Enumerable.Range(0, settings.Length))
			{
				snake.Eat();
				snake.DrawAndMove();
			}

			return snake;
		}
    }
}
