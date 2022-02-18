using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSnake
{
    internal class SoundProcessor
    {

    }

    internal class Note
    {
        public int Frequency { get; set; }

        public int Duration { get; set; }

        public Note(int frequency, int duration)
        {
            if (frequency < 20)
                throw new ArgumentOutOfRangeException(nameof(frequency));

            if (duration < 20)
                throw new ArgumentOutOfRangeException(nameof(duration));

            Frequency = frequency;
            Duration = duration;
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

    internal static class Sounds
    {
        public static bool Enabled { get; set; }

        public static void Eat()
        {
            Run(() => Console.Beep());
        }

        private static void Run(Action action)
        {
            Task.Run(action);
        }
    }
}
