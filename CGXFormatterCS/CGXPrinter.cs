using System;
using System.Collections.Generic;
using System.IO;

namespace CGXFormatterCS
{
    public enum State { Idle, StartQuotes, InQuotes, EndQuotes, WritePrimitive, WriteKeyValue, StartBlock, EndBlock, EndElement }

    public class CGXPrinter
    {
        private State state;
        private TextWriter writer;
        private List<IFormatter> formatters;

        public CGXPrinter(params IFormatter[] inputFormatters)
        {
            state = State.Idle;
            writer = Console.Out;
            if (inputFormatters.Length != 0)
            {
                formatters = new List<IFormatter>();
                foreach (var formatter in inputFormatters)
                {
                    formatters.Add(formatter);
                }
            }
        }

        public void SetOutput(TextWriter outStream)
        {
            writer = outStream;
        }

        public void Print(char c)
        {
            if (IsTrash(c))
                return;

            state = Parse(c);
            foreach (var formatter in formatters)
            {
                Format(formatter, c);
            }
        }

        private void Format(IFormatter formatter, char c)
        {
            switch (state)
            {
                case State.StartQuotes:
                    formatter.FormatStartQuotes(c, writer);
                    break;
                case State.InQuotes:
                    formatter.FormatInQuotes(c, writer);
                    break;
                case State.EndQuotes:
                    formatter.FormatEndQuotes(c, writer);
                    break;
                case State.WritePrimitive:
                    formatter.FormatWritePrimitive(c, writer);
                    break;
                case State.WriteKeyValue:
                    formatter.FormatWriteKeyValue(c, writer);
                    break;
                case State.StartBlock:
                    formatter.FormatStartBlock(c, writer);
                    break;
                case State.EndBlock:
                    formatter.FormatEndBlock(c, writer);
                    break;
                case State.EndElement:
                    formatter.FormatEndElement(c, writer);
                    break;
                default:
                    return;
            }
        }

        private State Parse(char c)
        {
            if ((state != State.InQuotes && state != State.StartQuotes) && c == '\'')
                return State.StartQuotes;

            if ((state == State.InQuotes || state == State.StartQuotes) && c != '\'')
                return State.InQuotes;

            if ((state == State.InQuotes || state == State.StartQuotes) && c == '\'')
                return State.EndQuotes;

            if (c == '=')
                return State.WriteKeyValue;

            if (c == '(')
                return State.StartBlock;

            if (c == ')')
                return State.EndBlock;

            if (c == ';')
                return State.EndElement;

            if (IsCorrectSymbol(c))
                return State.WritePrimitive;

            return State.Idle;
        }

        private bool IsCorrectSymbol(char c)
        {
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
            {
                return true;
            }
            return false;
        }

        private bool IsTrash(char c)
        {
            if (IsCorrectSymbol(c) || c == '(' || c == ')' || c == '\'' ||
                c == ';' || c == '=' || state == State.InQuotes || state == State.StartQuotes)
            {
                return false;
            }
            return true;
        }
    }
}
