using System;
using System.Collections.Generic;

namespace ConsoleSnake.Sounds
{
    internal abstract class ArduinoСompatibleMelody
    {
        protected abstract int[] Tones { get; }

        protected abstract int[] Durations { get; }

        protected abstract int[] Delays { get; }

        public virtual IEnumerable<Note> GetMelody()
        {
            if (Tones == null)
                throw new InvalidOperationException(nameof(Tones));

            if (Durations == null)
                throw new InvalidOperationException(nameof(Durations));

            if (Delays == null)
                throw new InvalidOperationException(nameof(Delays));

            int count = Tones.Length;

            if (count != Durations.Length || count != Delays.Length)
                throw new InvalidOperationException();

            for (int i = 0; i < count; ++i)
            {
                yield return new Note(frequency: Tones[i], duration: Durations[i]);
                yield return Note.Pause(Delays[i]);
            }
        }
    }
}
