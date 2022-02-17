using System;

namespace ConsoleSnake
{
    internal class Program
	{
		private static volatile bool _isRunning; // по большей мере нужно только для отладки

		private static ScoresBar scores 
			= new ScoresBar();

		private static Snake snake 
			= SnakeBuilder.Create(
				new SnakeBuilderSettings 
				{ 
					Length = 1, 
					Surface = '#', 
					Color = ConsoleColor.Green
				});

		private static SnakeFood food 
			= new SnakeFood(surface: 'A', color: ConsoleColor.Yellow);

		private static GameArea area
			= new GameArea(frameRate: 60, speedInFramesPerSecond: 5);

		private static void InitGame()
        {
			Console.Clear();

			_isRunning = false;

			scores
				= new ScoresBar();

			snake
				= SnakeBuilder.Create(
					new SnakeBuilderSettings
					{
						Length = 3,
						Surface = '#',
						Color = ConsoleColor.Green
					});

			food
				= new SnakeFood(surface: 'A', color: ConsoleColor.Yellow);

			area
				= new GameArea(frameRate: 60, speedInFramesPerSecond: 5);
		}

		static void Main()
		{
			do
			{
				InitGame();
				CreateFood();

				area.DrawFrame += Area_DrawFrame;
				area.MainLoop();
			}
			while (true);
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
				area.GameOver(TimeSpan.FromSeconds(1));

				return;
			}

			if (snake.InMyHead(food.Position))
            {
				snake.Eat();
				scores.Add();

				CreateFood();
            }

			food.Draw();

			if (!GameArea.IsOpposite(Snake.HeadDirection, e))
				Snake.HeadDirection = e;

			snake.DrawAndMove();

			_isRunning = false;
		}
	}
}
