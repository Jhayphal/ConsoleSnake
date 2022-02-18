using ConsoleSnake.Common;
using ConsoleSnake.Game;
using System;
using System.Drawing;

namespace ConsoleSnake
{
    internal class Program
	{
		private const int UpToSpeed = 99;

		static void Main()
		{
			while (true)
				GameLoop();
		}

		static void GameLoop()
        {
			var speed = GetSpeed();

			new GameProcess(speed).Run();
		}

		private static int GetSpeed()
        {
			while (true)
			{
				Console.Clear();

				Painter.DrawTextCentered($"Укажите скорость игры [1 - {UpToSpeed}]: ");

				var lastLeftCursorPosition = Console.CursorLeft;

				Painter.DrawText(
					"(где 1 - медленно)",
					new Point(
						lastLeftCursorPosition + 3,
						Console.CursorTop),
					ConsoleColor.DarkGray);

				Console.CursorLeft = lastLeftCursorPosition;

				Console.CursorVisible = true;

				if (TryRequestSpeed(out int speed))
					return speed;

				Console.Clear();

				Painter.DrawTextCentered($"Нужно ввести число от 1 до {UpToSpeed} включительно!", ConsoleColor.Red);

				System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
			}
		}

		private static bool TryRequestSpeed(out int speed)
		{
			speed = RequestSpeed();

			return speed > 0 && speed <= UpToSpeed;
		}

		private static int RequestSpeed()
        {
			Console.ForegroundColor = ConsoleColor.Yellow;

			int first;

			while (!TryReadInt(out first)) ;

			if (TryReadInt(out int second))
				return first * 10 + second;

			return first;
        }

		private static bool TryReadInt(out int value)
        {
			while (true)
			{
				var key = Console.ReadKey(true).Key;

				if (key == ConsoleKey.Enter)
				{
					value = 0;

					return false;
				}

				if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
				{
					value = key - ConsoleKey.D0;

					Console.Write(value);

					return true;
				}

				if (key >= ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9)
				{
					value = key - ConsoleKey.NumPad0;

					Console.Write(value);

					return true;
				}
			}
		}
	}
}
