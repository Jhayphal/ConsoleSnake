using System;

namespace ConsoleSnake.Sounds
{
    internal class ConsoleSoundInterface : ISoundInterface
    {
        public void Beep(Note note)
        {
            Console.Beep(note.Frequency, note.Duration);
        }
    }
}
