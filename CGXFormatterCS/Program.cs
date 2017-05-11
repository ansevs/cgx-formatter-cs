using System;

namespace CGXFormatterCS
{
    public class Program
    {
        public static void Main()
        {
            ColorFormatter f1 = ColorFormatter.GetInstance();   // простейший форматер, просто задает цвет элементам.
            IndentFormatter f2 = IndentFormatter.GetInstance(); // форматер задания, проставляет отступы.
            CGXPrinter p = new CGXPrinter(f1, f2);              // объект, который использует объявленные выше форматеры.

            // Codingame case
            string cGXLine = @"'users'=(('id'=10;'name'='Serge';'roles'=('visitor';
'moderator'));('id'=11;'name'='Biales');true)";

            for (int j = 0; j < cGXLine.Length; j++)
            {
                p.Print(cGXLine[j]);
            }
            Console.ResetColor();
        }
    }
}
