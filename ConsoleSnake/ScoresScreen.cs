using System;

namespace ConsoleSnake
{
    internal sealed class ScoresBar
    {
		public int ScoresCount { get; private set; }

        public ScoresBar()
        {
            Draw();
        }

		public void Add()
        {
			++ScoresCount;

            Draw();
        }

		private void Draw()
        {
            Console.Title = ToString();
        }

        public override string ToString()
        {
            return "Набрано очков: " + ScoresCount;
        }
    }
}
