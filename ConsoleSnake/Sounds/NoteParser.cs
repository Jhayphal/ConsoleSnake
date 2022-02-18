using System;
using System.Collections.Generic;

namespace ConsoleSnake.Sounds
{
    internal static class NoteParser
    {
        public static IEnumerable<Note> Parse(string melody)
        {
            var commands = melody.Split(
                new char[] { ' ' }, 
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var command in commands)
            {
                if (char.IsDigit(command[0]))
                    yield return Note.Pause(int.Parse(command));
                else
                    yield return ParseNote(command);
            }
        }

        private static Note ParseNote(string note)
        {
            var parts = note.Split(
                new char[] { ':' }, 
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                throw new ArgumentException(note);

            var duration = int.Parse(parts[1]);
            var octave = parts[0][1] - '0';
            var tone = parts[0][0];

            switch (tone)
            {
                case 'C':
                    return Note.C(duration, octave);

                case 'D':
                    return Note.D(duration, octave);

                case 'E':
                    return Note.E(duration, octave);

                case 'F':
                    return Note.F(duration, octave);

                case 'G':
                    return Note.G(duration, octave);

                case 'A':
                    return Note.A(duration, octave);

                case 'B':
                    return Note.B(duration, octave);

                default:
                    throw new NotImplementedException(tone.ToString());
            }
        }
    }
}
