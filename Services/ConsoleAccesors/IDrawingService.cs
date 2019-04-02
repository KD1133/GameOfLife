using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.ConsoleAccesors
{
    interface IDrawingService
    {
        void Clear();
        void DispalyCursor(int x, int y);
        void WriteLine(string strToWrite);
        void UpdateCell(bool CellState, int x, int y);
        void DisplayHeader(string strToWrite);
        void DispalyGlobalStats(int globalLiveCels, int gameCount, int iteration);
        void DisplayStats(int liveCells, int boardNumber, int height, int displayPos);
        void DisplayInitial(bool[,] cells, int height, int width, int displayPos);
        void Display(bool[,] cells, bool[,] previousCells, int height, int width, int displayPos);
    }
}
