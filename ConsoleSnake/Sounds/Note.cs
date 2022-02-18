using System;

namespace ConsoleSnake.Sounds
{
    internal class Note
    {
        public int Frequency { get; set; }

        public int Duration { get; set; }

        public bool IsPause => Frequency == 0;

        public Note(int frequency, int duration) 
            : this(duration)
        {
            if (frequency < 20)
                throw new ArgumentOutOfRangeException(nameof(frequency));

            Frequency = frequency;
        }

        public Note(int duration)
        {
            if (duration < 20)
                throw new ArgumentOutOfRangeException(nameof(duration));

            Duration = duration;
        }

        public static Note Pause(int duration)
        {
            return new Note(duration);
        }

        public static Note C(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 33, octave, duration);
        }

        public static Note D(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 37, octave, duration);
        }

        public static Note E(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 41, octave, duration);
        }

        public static Note F(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 43, octave, duration);
        }

        public static Note G(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 49, octave, duration);
        }

        public static Note A(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 55, octave, duration);
        }

        public static Note B(int duration = 500, int octave = 4)
        {
            return CreateNote(beginFrequency: 62, octave, duration);
        }

        private static Note CreateNote(int beginFrequency, int octave, int duration)
        {
            CheckArguments(octave, duration);

            var frequency = beginFrequency;

            while (--octave > 0)
                frequency *= 2;

            return new Note(frequency, duration);
        }

        private static void CheckArguments(int octave, int duration)
        {
            if (octave < 1 || octave > 10)
                throw new ArgumentOutOfRangeException(nameof(octave));

            if (duration < 1)
                throw new ArgumentOutOfRangeException(nameof(duration));
        }
    }
}
