using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class ConsoleFacade
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void WriteLine(string strToWrite)
        {
            Console.WriteLine(strToWrite);
        }

        public void Write(string strToWrite)
        {
            Console.Write(strToWrite);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string ReadKey()
        {
            ConsoleKey input;
            input = Console.ReadKey(true).Key;
            return input.ToString();
        }
    }
}
