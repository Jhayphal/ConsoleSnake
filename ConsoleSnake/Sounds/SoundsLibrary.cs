using System;
using System.Threading.Tasks;

namespace ConsoleSnake.Sounds
{
    internal static class SoundsLibrary
    {
        private static readonly SoundProcessor Processor = new SoundProcessor();

        public static bool Enabled { get; set; } = true;

        public static void Eat()
        {
            Run(() => Processor.Player.Beep(Note.C(duration: 350)));
        }

        public static void GameOver()
        {
            Run(() => Processor.Play(NoteParser.GetDefault()));
        }

        private static void Run(Action action)
        {
            if (Enabled)
                Task.Run(action);
        }
    }
}
