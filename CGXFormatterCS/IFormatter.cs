using System.IO;

namespace CGXFormatterCS
{
    public interface IFormatter
    {
        void FormatStartQuotes(char c, TextWriter tw);
        void FormatInQuotes(char c, TextWriter tw);
        void FormatEndQuotes(char c, TextWriter tw);
        void FormatWritePrimitive(char c, TextWriter tw);
        void FormatWriteKeyValue(char c, TextWriter tw);
        void FormatStartBlock(char c, TextWriter tw);
        void FormatEndBlock(char c, TextWriter tw);
        void FormatEndElement(char c, TextWriter tw);
    }
}
