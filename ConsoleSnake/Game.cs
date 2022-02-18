using System;

namespace ConsoleSnake
{
    internal class Game
    {
        private volatile bool _isRunning; // по большей мере нужно только для отладки

        private ScoresBar scores;
        private Snake snake;
        private SnakeFood food;
        private GameArea area;

		private int Speed;

		public void Run(int speed)
        {
			Speed = speed;

			Initialize();
			CreateFood();

			area.DrawFrame += Area_DrawFrame;
			area.MainLoop();

			System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.5));
		}

		private void Initialize()
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
						Surface = 'o',
						Color = ConsoleColor.Green
					});

			food
				= new SnakeFood(surface: 'O', color: ConsoleColor.Yellow);

			area
				= new GameArea(frameRate: 60, speedInFramesPerSecond: Speed);
		}

		private void CreateFood()
		{
			do
			{
				food.CreateNew();
			}
			while (snake.InsideMe(food.Position));
		}

		private void Area_DrawFrame(object sender, SnakeHeadDirection e)
		{
			if (_isRunning)
				return;

			_isRunning = true;

			var gameArea = GameArea.GetCurrentArea();

			if (snake.HasCollisions(gameArea))
			{
				area.GameOver();

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
