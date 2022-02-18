using System;
using System.Drawing;

namespace ConsoleSnake
{
    internal class Program
	{
		static void Main()
		{
			while (true)
				GameLoop();
		}

		static void GameLoop()
        {
			int speed;

			do
			{
				Console.Clear();

				Painter.DrawTextCentered("Укажите скорость игры [1 - 19]: ");

				var lastLeftCursorPosition = Console.CursorLeft;

				Painter.DrawText(
					"(где 1 - быстро)", 
					new Point(
						lastLeftCursorPosition + 3, 
						Console.CursorTop), 
					ConsoleColor.DarkGray);

				Console.CursorLeft = lastLeftCursorPosition;

				Console.CursorVisible = true;

				if (TryReadSpeed(out speed))
					break;

				Console.Clear();

				Painter.DrawTextCentered("Нужно ввести число от 1 до 19 включительно!", ConsoleColor.Red);

				System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
			}
			while (true);

			new Game().Run(speed);
		}

		public static bool TryReadSpeed(out int value)
        {
			var text = Console.ReadLine();

			return int.TryParse(text, out value) && value > 0 && value < 20;
        }
	}
}
