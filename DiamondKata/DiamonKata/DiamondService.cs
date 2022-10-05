using System.Text;

namespace DiamondKata
{
    public class DiamondService
    {
        public void Start()
        {
            Console.WriteLine("Welcome to Diamond Kata !");

            string input;

            while (true)
            {
                try
                {
                    WriteLine("Please insert one alphabet character (hit ENTER to quit): ", ConsoleColor.Cyan);

                    input = Console.ReadLine();

                    if (input.Equals(""))
                        break;

                    ValidateInput(input);

                    PrintDiamond(input[0]);
                }
                catch (ArgumentException ex)
                {
                    WriteLine(ex.Message, ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Validate input is a alphabet character
        /// </summary>
        /// <param name="input"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ValidateInput(string? input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("The input string is empty.");

            if (input.Length > 1)
                throw new ArgumentException("Please insert only one character.");

            var c = input[0];

            if (!(c >= 'A' && c <= 'Z') && !(c >= 'a' && c <= 'z'))
                throw new ArgumentException("The input character is not a alphabet letter.");
        }

        /// <summary>
        /// Method receives a character and prints the diamond string to console
        /// </summary>
        /// <param name="c"></param>
        public void PrintDiamond(char c)
        {
            int maxIdx = (int)c % 32;

            int i = maxIdx;

            var maxStringLength = GetLineLength(maxIdx);

            var finalString = new StringBuilder();

            do
            {
                var currentLine = GetDiamondLine(i, maxStringLength);

                if (i < maxIdx)
                {
                    finalString.Insert(0, $"{currentLine}{Environment.NewLine}");
                }

                finalString.AppendLine(currentLine);

                i--;
            }
            while (i > 0);

            WriteLine(finalString.ToString());
        }

        /// <summary>
        /// Generates the string for the current letter, based on the median line length
        /// </summary>
        /// <param name="currentIdx"></param>
        /// <param name="maxStringLength"></param>
        /// <returns></returns>
        public string GetDiamondLine(int currentIdx, int maxStringLength)
        {
            var lineLength = GetLineLength(currentIdx);

            var letter = GetCurrentLetter(currentIdx).ToString();

            if (lineLength == 1)
            {
                if (lineLength == maxStringLength)
                {
                    return letter;
                }
                else
                {
                    var beforeNoRepeats = (maxStringLength - lineLength) / 2;

                    var beforeSeparator = new string('_', beforeNoRepeats);

                    return string.Concat(beforeSeparator, letter, beforeSeparator);
                }
            }
            else
            {
                var beforeNoRepeats = (maxStringLength - lineLength) / 2;

                var beforeSeparator = new string('_', beforeNoRepeats);

                var btwSeparator = new string('_', (lineLength - 2));

                return string.Concat(beforeSeparator, letter, btwSeparator, letter, beforeSeparator);
            }
        }

        /// <summary>
        /// Method returns the upper care letter for the specified position in the latin alphabet
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public char GetCurrentLetter(int position)
        {
            return (char)(64 + position);
        }

        /// <summary>
        /// Method computes the length of the string between (and including) the limits of the diamond line
        /// i.e. for index 5 - means letter E - the string inside the diamond is E + 7 separators + E => 9
        /// the formula is the odd number matematical progression for 5 = (1 + (5-1)*2) = 9
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public int GetLineLength(int idx)
        {
            return (idx <= 1) ? 1 : (1 + ((idx - 1) * 2));
        }

        /// <summary>
        /// helper method for printing colored text in console
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="foreground"></param>
        /// <param name="backgroundColor"></param>
        public void WriteLine(string buffer, ConsoleColor foreground = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(buffer);
            Console.ResetColor();
        }
    }
}