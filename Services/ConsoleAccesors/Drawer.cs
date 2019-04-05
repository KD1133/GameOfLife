using GameOfLife.ConsoleAccesors;
using System;

namespace GameOfLife
{
    class Drawer : IDrawer
    {
        private ConsoleFacade _consoleFacade { get; set; }

        public Drawer(ConsoleFacade consoleFacade)
        {
            _consoleFacade = consoleFacade;
        }

        public void Clear()
        {
            _consoleFacade.Clear();
        }

        public void DispalyCursor(int x, int y)
        {
            _consoleFacade.SetCursorPosition(x + 1, y + 4);
        }

        public void WriteLine(string strToWrite)
        {
            _consoleFacade.WriteLine(strToWrite);
        }

        public void UpdateCell(bool CellState,int x,int y)
        {
            _consoleFacade.SetCursorPosition(x + 1, y + 4);
            if (CellState == false)
            {
                _consoleFacade.Write(" ");
            }
            else
            {
                _consoleFacade.Write("█");
            }
        }

        public void DisplayHeader(string strToWrite)
        {
            _consoleFacade.SetCursorPosition(0, 0);
            _consoleFacade.WriteLine("                                                                                                                             ");
            _consoleFacade.SetCursorPosition(0, 0);
            _consoleFacade.WriteLine(strToWrite);
        }

        public void DispalyGlobalStats(int globalLiveCels, int gameCount, int iteration)
        {
            _consoleFacade.SetCursorPosition(0, 1);
            _consoleFacade.Write("Total live cells : ");
            _consoleFacade.Write(globalLiveCels.ToString());
            _consoleFacade.Write(" | Total game count : ");
            _consoleFacade.Write(gameCount.ToString());
            _consoleFacade.Write(" | Iteration : ");
            _consoleFacade.Write(iteration.ToString());
            _consoleFacade.Write("                                ");
        }

        public void DisplayStats(int liveCells, int boardNumber, int height, int displayPos) 
        {
            _consoleFacade.SetCursorPosition(0, 2 + (height + 4) * displayPos);
            _consoleFacade.Write("Board number : ");
            _consoleFacade.Write(boardNumber.ToString());
            _consoleFacade.Write(" | Live Cells Count : ");
            _consoleFacade.Write(liveCells.ToString());
            _consoleFacade.Write("                                                   "); //bad hack to remove empty numbers
        }

        public void DisplayInitial(bool[,] cells, int height, int width, int displayPos)
        {
            _consoleFacade.SetCursorPosition(0, 3 + (height + 4) * displayPos);
            _consoleFacade.Write("╔");
            for (int i = 0; i < width; i++) _consoleFacade.Write("═");
            _consoleFacade.Write("╗");
            for (int j = 0; j < height; j++)
            {
                _consoleFacade.SetCursorPosition(0, 4 + j + (height + 4) * displayPos);
                _consoleFacade.Write("║");
                _consoleFacade.SetCursorPosition(width + 1, 4 + j + (height + 4) * displayPos);
                _consoleFacade.Write("║");
            }
            _consoleFacade.SetCursorPosition(0, height + 4 + (height + 4) * displayPos);
            _consoleFacade.Write("╚");
            for (int i = 0; i < width; i++) _consoleFacade.Write("═");
            _consoleFacade.Write("╝");

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    _consoleFacade.SetCursorPosition(i + 1, (j + 4) + (height + 4) * displayPos);
                    if(cells[i, j] == true)
                    {
                        _consoleFacade.Write("█");
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
                        _consoleFacade.SetCursorPosition(i + 1, (j + 4) + (height + 4) * displayPos);
                        if (cells[i, j] == false)
                        {
                            _consoleFacade.Write(" ");
                        }
                        else
                        {
                            _consoleFacade.Write("█");
                        }
                    }
                }
            }
        }
    }
}
