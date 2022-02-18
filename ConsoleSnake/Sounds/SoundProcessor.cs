using System.Collections.Generic;
using System.Threading;

namespace ConsoleSnake.Sounds
{
    internal class SoundProcessor
    {
        public ISoundInterface Player { get; set; } = new ConsoleSoundInterface();

        public void Play(IEnumerable<Note> melody)
        {
            foreach(var note in melody)
                if (note.IsPause)
                    Thread.Sleep(note.Duration);
                else
                    Player.Beep(note);
        }
    }
}
