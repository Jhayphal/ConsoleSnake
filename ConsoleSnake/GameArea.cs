using System;
using System.Drawing;
using System.Timers;

namespace ConsoleSnake
{
    internal sealed class GameArea
	{
		private readonly Timer Clock = new Timer();
		
		private readonly int SpeedInFramesPerSecond;
		private int ToNextMoveFramesCount;

		private volatile SnakeHeadDirection Direction = SnakeHeadDirection.Right;

		public event EventHandler<SnakeHeadDirection> DrawFrame;

		public GameArea(int frameRate, int speedInFramesPerSecond)
        {
			SpeedInFramesPerSecond = speedInFramesPerSecond;

			ToNextMoveFramesCount = SpeedInFramesPerSecond;
			
			Clock.Interval = 1000d / frameRate;
			Clock.Elapsed += Clock_Elapsed;
			Clock.Start();
		}

		public void MainLoop()
        {
			try
			{
				while (Clock.Enabled)
				{
					var key = Console.ReadKey(true).Key;

					if (TryGetNewDirection(key, out var newDirection))
						if (newDirection != Direction)
							Direction = newDirection;
				}
			}
			catch { }

			Clock.Stop();
			Clock.Dispose();
		}

		public void GameOver(TimeSpan await)
		{
			Clock.Enabled = false;

			Painter.DrawTextCentered("Неудачник!", ConsoleColor.Red);

            System.Threading.Thread.Sleep(await);
		}

		private void Clock_Elapsed(object sender, ElapsedEventArgs e)
		{
			Console.CursorVisible = false;

			if (SpeedInFramesPerSecond == 0)
				throw new InvalidProgramException(nameof(SpeedInFramesPerSecond));

			if (--ToNextMoveFramesCount == 0)
			{
				DrawFrame?.Invoke(this, Direction);

				ToNextMoveFramesCount = SpeedInFramesPerSecond;
			}
		}

		public static Rectangle GetCurrentArea()
        {
			return new Rectangle
			(
				Point.Empty,
				new Size(Console.WindowWidth, Console.WindowHeight)
			);
		}

		public static Point GetCenter()
        {
			return new Point
			(
				Console.WindowWidth >> 1,
				Console.WindowHeight >> 1
			);
		}

		public static bool IsOpposite(SnakeHeadDirection first, SnakeHeadDirection second)
        {
			return first == SnakeHeadDirection.Left && second == SnakeHeadDirection.Right
				|| first == SnakeHeadDirection.Up && second == SnakeHeadDirection.Down
				|| first == SnakeHeadDirection.Right && second == SnakeHeadDirection.Left
				|| first == SnakeHeadDirection.Down && second == SnakeHeadDirection.Up;
		}

		private static bool TryGetNewDirection(ConsoleKey key, out SnakeHeadDirection headDirection)
		{
			switch (key)
			{
				case ConsoleKey.A:
				case ConsoleKey.LeftArrow:
					headDirection = SnakeHeadDirection.Left;

					return true;

				case ConsoleKey.D:
				case ConsoleKey.RightArrow:
					headDirection = SnakeHeadDirection.Right;

					return true;

				case ConsoleKey.W:
				case ConsoleKey.UpArrow:
					headDirection = SnakeHeadDirection.Up;

					return true;

				case ConsoleKey.S:
				case ConsoleKey.DownArrow:
					headDirection = SnakeHeadDirection.Down;

					return true;

				default:
					headDirection = default;

					return false;
			}
		}
	}
}
