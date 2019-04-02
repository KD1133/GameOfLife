using GameOfLife.ConsoleAccesors;
using System;

namespace GameOfLife
{
    class DrawingService : IDrawingService
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void DispalyCursor(int x, int y)
        {
            Console.SetCursorPosition(x + 1, y + 4);
        }

        public void WriteLine(string strToWrite)
        {
            Console.WriteLine(strToWrite);
        }

        public void UpdateCell(bool CellState,int x,int y)
        {
            Console.SetCursorPosition(x + 1, y + 4);
            if (CellState == false)
            {
                Console.Write(" ");
            }
            else
            {
                Console.Write("█");
            }
        }

        public void DisplayHeader(string strToWrite)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("                                                                                         ");
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(strToWrite);
        }

        public void DispalyGlobalStats(int globalLiveCels, int gameCount, int iteration)
        {
            Console.SetCursorPosition(0, 1);
            Console.Write("Total live cells : ");
            Console.Write(globalLiveCels);
            Console.Write(" | Total game count : ");
            Console.Write(gameCount);
            Console.Write(" | Iteration : ");
            Console.Write(iteration);
            Console.Write("                                ");
        }

        public void DisplayStats(int liveCells, int boardNumber, int height, int displayPos) 
        {
            Console.SetCursorPosition(0, 2 + (height + 4) * displayPos);
            Console.Write("Board number : ");
            Console.Write(boardNumber);
            Console.Write(" | Live Cells Count : ");
            Console.Write(liveCells);
            Console.Write("                "); //bad hack to remove empty numbers
        }

        public void DisplayInitial(bool[,] cells, int height, int width, int displayPos)
        {
            Console.SetCursorPosition(0, 3 + (height + 4) * displayPos);
            Console.Write("╔");
            for (int i = 0; i < width; i++) Console.Write("═");
            Console.Write("╗");
            for (int j = 0; j < height; j++)
            {
                Console.SetCursorPosition(0, 4 + j + (height + 4) * displayPos);
                Console.Write("║");
                Console.SetCursorPosition(width + 1, 4 + j + (height + 4) * displayPos);
                Console.Write("║");
            }
            Console.SetCursorPosition(0, height + 4 + (height + 4) * displayPos);
            Console.Write("╚");
            for (int i = 0; i < width; i++) Console.Write("═");
            Console.Write("╝");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Console.SetCursorPosition(i + 1, (j + 4) + (height + 4) * displayPos);
                    if(cells[i, j] == true)
                    {
                        Console.Write("█");
                    }
                }
            }
        }

        public void Display(bool[,] cells, bool[,] previousCells, int height, int width, int displayPos)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (previousCells[i, j] != cells[i, j])
                    {
                        Console.SetCursorPosition(i + 1, (j + 4) + (height + 4) * displayPos);
                        if (cells[i, j] == false)
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("█");
                        }
                    }
                }
            }
        }
    }
}
