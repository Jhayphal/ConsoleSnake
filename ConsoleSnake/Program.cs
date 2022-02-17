using System;

namespace ConsoleSnake
{
    internal class Program
	{
		private static volatile bool _isRunning = false; // по большей мере нужно только для отладки

		private static readonly ScoresBar scores 
			= new ScoresBar();

		private static readonly Snake snake 
			= SnakeBuilder.Create(surface: '#', color: ConsoleColor.Cyan, length: 3);

		private static readonly SnakeFood food 
			= new SnakeFood(surface: 'A', color: ConsoleColor.Yellow);

		private static readonly GameArea area
			= new GameArea(frameRate: 60, speedInFramesPerSecond: 5);

		static void Main()
		{
			CreateFood();

            area.DrawFrame += Area_DrawFrame;
			area.MainLoop();
		}

		private static void CreateFood()
        {
			do
			{
				food.CreateNew();
			}
			while (snake.InsideMe(food.Position));
		}

        private static void Area_DrawFrame(object sender, SnakeHeadDirection e)
        {
			if (_isRunning)
				return;

			_isRunning = true;

			var gameArea = GameArea.GetCurrentArea();

			if (snake.HasCollisions(gameArea))
			{
				(sender as GameArea)?
					.GameOver(TimeSpan.FromSeconds(1));

				return;
			}

			if (snake.InMyHead(food.Position))
            {
				snake.Eat();
				scores.Add();

				CreateFood();
            }

			food.Draw();
			snake.DrawAndMove(e);

			_isRunning = false;
		}
	}
}
