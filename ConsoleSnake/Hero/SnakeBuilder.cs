using ConsoleSnake.Game;
using System;
using System.Linq;

namespace ConsoleSnake.Hero
{
    internal static class SnakeBuilder
    {
		public static Snake Create(SnakeBuilderSettings settings)
		{
			if (settings.Length <= 0)
				throw new ArgumentOutOfRangeException(nameof(settings.Length));

			var position = GameArea.GetCenter();

            var snake = new Snake(position)
            {
                Color = settings.Color,
                Surface = settings.Surface,
				HeadDirection = SnakeHeadDirection.Right
            };

			foreach (var _ in Enumerable.Range(0, settings.Length))
			{
				snake.Eat();
				snake.DrawAndMove();
			}

			return snake;
		}
	}
}
