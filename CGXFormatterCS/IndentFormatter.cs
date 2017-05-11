using System.IO;

namespace CGXFormatterCS
{
    public class IndentFormatter : IFormatter
    {
        private static IndentFormatter instance;
        private State prevState;
        private int indentCounter;

        private IndentFormatter()
        {
            indentCounter = 0;
        }

        public static IndentFormatter GetInstance()
        {
            if (instance == null)
            {
                instance = new IndentFormatter();
            }
            return instance;
        }

        public void FormatStartQuotes(char c, TextWriter tw)
        {
            if (prevState != State.WriteKeyValue)
            {
                PrintChar(c, tw);
            }
            else
            {
                tw.Write(c);
            }
            prevState = State.StartQuotes;
        }

        public void FormatInQuotes(char c, TextWriter tw)
        {
            tw.Write(c);
            prevState = State.InQuotes;
        }

        public void FormatEndQuotes(char c, TextWriter tw)
        {
            tw.Write(c);
            prevState = State.EndQuotes;
        }

        public void FormatWritePrimitive(char c, TextWriter tw)
        {
            if (prevState != State.WriteKeyValue && prevState != State.WritePrimitive)
            {
                PrintChar(c, tw);
            }
            else
            {
                tw.Write(c);
            }
            prevState = State.WritePrimitive;
        }

        public void FormatWriteKeyValue(char c, TextWriter tw)
        {
            tw.Write(c);
            prevState = State.WriteKeyValue;
        }

        public void FormatStartBlock(char c, TextWriter tw)
        {
            PrintChar(c, tw);
            indentCounter++;
            prevState = State.StartBlock;
        }

        public void FormatEndBlock(char c, TextWriter tw)
        {
            if (indentCounter != 0) //Пока только такая простая проверка.
                indentCounter--;
            PrintChar(c, tw);
            prevState = State.EndBlock;
        }

        public void FormatEndElement(char c, TextWriter tw)
        {
            tw.Write(c);
            prevState = State.EndElement;
        }

        private void PrintChar(char c, TextWriter tw)
        {
            if (prevState != State.Idle)
                tw.WriteLine();
            Indent(tw);
            tw.Write(c);
        }

        private void Indent(TextWriter tw)
        {
            for (int i = 0; i < indentCounter; i++)
            {
                tw.Write("    ");
            }
        }
    }
}
