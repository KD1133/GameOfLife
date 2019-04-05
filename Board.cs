using GameOfLife.Services;
using System;

namespace GameOfLife
{
    class Board
    {
        static private Random _Rnd = new Random();
        private CellIterator _cellIterator;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Iteration { get; private set; }
        public int LiveCells { get; private set; }
        public bool[,] Cells { get; set; }
        public bool[,] PreviousCells { get; set; }

        public Board(int width, int height, CellIterator cellIterator)
        {
            Width = width;
            Height = height;
            Cells = new bool[width, height];
            PreviousCells = new bool[width, height];
            _cellIterator = cellIterator;
        }

        public bool[,] GetCells()
        {
            return Cells;
        }

        public bool[,] GetPreviousCells()
        {
            return PreviousCells;
        }

        public void RandomizeCells()
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {

                    Cells[x, y] = _Rnd.Next(2) < 1 ? true : false;
                }
            }
        }

        public void ClearCells()
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    PreviousCells[x, y] = true;
                    Cells[x, y] = false;
                }
            }
        }

        public void InvertCell(int x, int y)
        {
            Cells[x, y] = !Cells[x, y];
        }

        public void UpdateCell(int x, int y, bool state)
        {
            Cells[x, y] = state;
        }

        public void Iterate()
        {
            PreviousCells = Cells.Clone() as bool[,];
            LiveCells = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int livingNeighbours = _cellIterator.CountLiveNeigbours(x, y, Cells, PreviousCells);
                    
                    if(_cellIterator.WillSurvive(x, y, livingNeighbours, PreviousCells[x, y]))
                    {
                        UpdateCell(x, y, true);
                        LiveCells++;
                    }
                    else
                    {
                        UpdateCell(x, y, false);
                    }
                }
            }
        }
    }
}
