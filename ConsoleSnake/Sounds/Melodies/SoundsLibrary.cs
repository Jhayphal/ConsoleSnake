using System;
using System.Threading.Tasks;

namespace ConsoleSnake.Sounds.Melodies
{
    internal static class SoundsLibrary
    {
        private static readonly SoundProcessor Processor = new SoundProcessor();

        public static bool Enabled { get; set; } = true;

        private const string DefaultMelody = 
            "A4:250 250 E3:250 250 A4:250 500 " +
            "A4:250 250 E3:250 250 A4:250 500 " +
            "E4:125 125 D4:125 125 C4:125 125 B4:125 125 A4:125 125 B4:125 125 C4:125 125 D4:125 125 " +
            "A4:250 250 E3:250 250 A4:250 500";

        private const string GameOverMelody =
            "A4:250 250 E3:250 250 A4:250 500";

        public static void Eat()
        {
            Run(() => Processor.Player.Beep(Note.E(duration: 250, octave: 5)));
        }

        public static void GameOver()
        {
            var melody = NoteParser.Parse(GameOverMelody);

            Run(() => Processor.Play(melody));
        }

        public static void SuperMario()
        {
            var melody = new SuperMarioMelody().GetMelody();

            Run(() => Processor.Play(melody));
        }

        private static void Run(Action action)
        {
            if (Enabled)
                Task.Run(action);
        }
    }
}
