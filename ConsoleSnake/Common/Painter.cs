using ConsoleSnake.Game;
using System;
using System.Drawing;

namespace ConsoleSnake.Common
{
    internal static class Painter
    {
		public static void DrawTextCentered(string text, ConsoleColor? color = null)
		{
			var center = GameArea.GetCenter();

			var x = center.X - (text.Length >> 1);
			var y = center.Y;

			var location = new Point(x, y);

			Draw(() => Console.Write(text), location, color);
		}

		public static void DrawText(string text, Point? location = null, ConsoleColor? color = null)
        {
			Draw(() => Console.Write(text), location, color);
		}

		public static void DrawChar(char symbol, Point? location = null, ConsoleColor? color = null)
		{
			Draw(() => Console.Write(symbol), location, color);
		}

		private static void Draw(Action action, Point? location = null, ConsoleColor? color = null)
		{
			if (location.HasValue)
				Console.SetCursorPosition(location.Value.X, location.Value.Y);

			var previouslyColor = Console.ForegroundColor;

			if (color.HasValue)
				Console.ForegroundColor = color.Value;

			action();

			if (color.HasValue)
				Console.ForegroundColor = previouslyColor;
		}
	}
}
