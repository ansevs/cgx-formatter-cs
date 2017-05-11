using System;
using System.IO;

namespace CGXFormatterCS
{
    public class ColorFormatter : IFormatter
    {
        private static ColorFormatter instance;

        private ColorFormatter()
        { }

        public static ColorFormatter GetInstance()
        {
            if (instance == null)
            {
                instance = new ColorFormatter();
            }
            return instance;
        }

        public void FormatStartQuotes(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void FormatInQuotes(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void FormatEndQuotes(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void FormatWritePrimitive(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        public void FormatWriteKeyValue(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        public void FormatStartBlock(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.Red;
        }

        public void FormatEndBlock(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.Red;
        }

        public void FormatEndElement(char c, TextWriter tw)
        {
            if (tw == Console.Out)
                Console.ForegroundColor = ConsoleColor.DarkGray;
        }
    }
}
