using ConsoleSnake.Common;
using ConsoleSnake.Hero;
using ConsoleSnake.Sounds;
using System;

namespace ConsoleSnake.Game
{
    internal class GameProcess
    {
        private volatile bool _isRunning; // по большей мере нужно только для отладки

        private ScoresBar scores;
        private Snake snake;
        private SnakeFood food;
        private GameArea area;

		private bool useSpeedCompensation = false;
		private bool waitNextFrame;

		public readonly int Speed;

		public GameProcess(int speed)
        {
			Speed = speed;
        }

		public void Run()
        {
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
						Surface = '0',
						Color = ConsoleColor.Green
					});

			food
				= new SnakeFood(surface: 'O', color: ConsoleColor.Yellow);

			area
				= new GameArea(frameRate: Speed);
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

			try
			{
				if (useSpeedCompensation)
					if (Snake.HeadDirection == SnakeHeadDirection.Up 
						|| Snake.HeadDirection == SnakeHeadDirection.Down)
						if (waitNextFrame = !waitNextFrame)
							return; // пропускаем каждый второй кадр дабы выравнять скорость змейки при вертикальном движении

				var gameArea = GameArea.GetCurrentArea();

				if (snake.HasCollisions(gameArea))
				{
					area.GameOver();

					SoundsLibrary.GameOver();

					return;
				}

				if (snake.InMyHead(food.Position))
				{
					SoundsLibrary.Eat();

					snake.Eat();
					scores.Add();

					CreateFood();
				}

				food.Draw();

				if (!GameArea.IsOpposite(Snake.HeadDirection, e))
					Snake.HeadDirection = e;

				snake.DrawAndMove();
			}
			finally
			{
				_isRunning = false;
			}
		}
	}
}
